
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using GYSWP.Documents;
using GYSWP.Documents.Dtos;
using GYSWP.Documents.DomainService;
using GYSWP.Employees.Dtos;
using GYSWP.Employees;
using GYSWP.Organizations;
using GYSWP.Dtos;
using GYSWP.Categorys;

namespace GYSWP.Documents
{
    /// <summary>
    /// Document应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class DocumentAppService : GYSWPAppServiceBase, IDocumentAppService
    {
        private readonly IRepository<Document, Guid> _entityRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<Organization, long> _organizationRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IDocumentManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public DocumentAppService(
        IRepository<Document, Guid> entityRepository
        , IRepository<Employee, string> employeeRepository
        , IRepository<Organization, long> organizationRepository
        , IRepository<Category> categoryRepository
        , IDocumentManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _categoryRepository = categoryRepository;
        }


        /// <summary>
        /// 获取Document的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<DocumentListDto>> GetPaged(GetDocumentsInput input)
        {
            //var categories = await _categoryRepository.GetAll().Where(d => d.DeptId == input.DeptId).Select(c => c.Id).ToArrayAsync();
            //var carr = Array.ConvertAll(categories, c => "," + c + ",");
            var query = _entityRepository.GetAll().Where(v => v.DeptIds == input.DeptId)
                .WhereIf(input.CategoryId.HasValue, v => v.CategoryId == input.CategoryId)
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), e => e.Name.Contains(input.KeyWord) || e.DocNo.Contains(input.KeyWord));
            //var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderByDescending(v => v.PublishTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<DocumentListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<DocumentListDto>>();

            return new PagedResultDto<DocumentListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取DocumentListDto信息
        /// </summary>

        public async Task<DocumentListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<DocumentListDto>();
        }

        /// <summary>
        /// 获取编辑 Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetDocumentForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetDocumentForEditOutput();
            DocumentEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<DocumentEditDto>();

                //documentEditDto = ObjectMapper.Map<List<documentEditDto>>(entity);
            }
            else
            {
                editDto = new DocumentEditDto();
            }

            output.Document = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Document的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateDocumentInput input)
        {

            //if (input.Document.Id.HasValue)
            //{
            //	await Update(input.Document);
            //}
            //else
            //{
            //	await Create(input.Document);
            //}
            if (input.Document.Id.HasValue)
            {
                await Update(input.Document);
                return new APIResultDto() { Code = 0, Msg = "保存成功" };
            }
            else
            {
                var entity = await Create(input.Document);
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
            }
        }


        /// <summary>
        /// 新增Document
        /// </summary>

        protected virtual async Task<DocumentEditDto> Create(DocumentEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Document>(input);
            var entity = input.MapTo<Document>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<DocumentEditDto>();
        }

        /// <summary>
        /// 编辑Document
        /// </summary>

        protected virtual async Task Update(DocumentEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Document信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Document的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 构建子部门树
        /// </summary>
        private List<DocNzTreeNode> getDeptChildTree(long pid, List<Organization> depts)
        {
            var trees = depts.Where(d => d.ParentId == pid).Select(d => new DocNzTreeNode()
            {
                key = d.Id.ToString(),
                title = d.DepartmentName,
                children = getDeptChildTree(d.Id, depts)
            });

            return trees.ToList();
        }

        /// <summary>
        /// 构建部门树
        /// </summary>
        private async Task<List<DocNzTreeNode>> getDeptTreeAsync(long[] deptids)
        {
            var trees = new List<DocNzTreeNode>();
            var depts = await _organizationRepository.GetAll().AsNoTracking().ToListAsync();
            foreach (var id in deptids)
            {
                if (id == 1)//顶级市
                {
                    trees.AddRange(getDeptChildTree(id, depts));
                }
                else
                {
                    var dept = depts.Where(d => d.Id == id).First();
                    trees.Add(new DocNzTreeNode()
                    {
                        key = dept.Id.ToString(),
                        title = dept.DepartmentName,
                        children = getDeptChildTree(id, depts)
                    });
                }
            }
            return trees;
        }

        public async Task<List<DocNzTreeNode>> GetDeptDocNzTreeNodesAsync()
        {
            var docDeptList = new List<DocNzTreeNode>();
            var root = new DocNzTreeNode()
            {
                key = "0",
                title = "标准归口部门"
            };

            //当前用户角色
            var roles = await GetUserRolesAsync();
            //如果包含市级管理员 和 系统管理员 全部架构
            if (roles.Contains(RoleCodes.Admin))
            {
                root.children = await getDeptTreeAsync(new long[] { 1 });//顶级部门
            }
            //else if (roles.Contains(RoleCodes.EnterpriseAdmin))//本部门架构
            //{
            //    var user = await GetCurrentUserAsync();
            //    if (!string.IsNullOrEmpty(user.EmployeeId))
            //    {
            //        var employee = await _employeeRepository.GetAsync(user.EmployeeId);
            //        var depts = employee.Department.Substring(1, employee.Department.Length - 2).Split("][");//多部门拆分
            //        root.children = await getDeptTreeAsync(Array.ConvertAll(depts, d => long.Parse(d)));
            //    }
            //}
            if (root.children.Count == 0)
            {
                root.children.Add(new DocNzTreeNode()
                {
                    key = "-1",
                    title = "没有任何部门权限"
                });
            }
            else
            {
                root.children[0].selected = true;
            }
            docDeptList.Add(root);
            return docDeptList;
        }

        /// <summary>
        /// 获取当前用户所属标准
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DocumentListDto>> GetPagedWithPermission(GetDocumentsInput input)
        {
            var curUser = await GetCurrentUserAsync();
            var query = _entityRepository.GetAll().Where(v => (v.PublishTime.HasValue ? v.PublishTime <= DateTime.Today : false) && (v.IsAllUser == true || v.EmployeeIds.Contains(curUser.EmployeeId)))
                .WhereIf(input.CategoryId.HasValue, v => v.CategoryId == input.CategoryId)
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), e => e.Name.Contains(input.KeyWord) || e.DocNo.Contains(input.KeyWord));
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(v=>v.CategoryId)
                    .ThenByDescending(v => v.PublishTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<DocumentListDto>>();
            return new PagedResultDto<DocumentListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 自查学习标题相关信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DocumentTitleDto> GetDocumentTitleAsync(Guid id)
        {
            var query = await _entityRepository.GetAsync(id);
            string deptName = await _organizationRepository.GetAll().Where(v => v.Id.ToString() == query.DeptIds).Select(v => v.DepartmentName).FirstOrDefaultAsync();
            var result = query.MapTo<DocumentTitleDto>();
            result.DeptName = deptName;
            return result;
        }
    }
}


