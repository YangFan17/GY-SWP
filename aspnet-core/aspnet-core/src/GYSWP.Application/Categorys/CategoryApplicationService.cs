
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


using GYSWP.Categorys;
using GYSWP.Categorys.Dtos;
using GYSWP.Categorys.DomainService;
using GYSWP.Dtos;
using GYSWP.Documents;
using GYSWP.Employees;

namespace GYSWP.Categorys
{
    /// <summary>
    /// Category应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class CategoryAppService : GYSWPAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category, int> _entityRepository;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly ICategoryManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public CategoryAppService(
        IRepository<Category, int> entityRepository
        , IRepository<Document, Guid> documentRepository
        , ICategoryManager entityManager
        , IRepository<Employee, string> employeeRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _documentRepository = documentRepository;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// 获取Category的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<CategoryListDto>> GetPaged(GetCategorysInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<CategoryListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<CategoryListDto>>();

            return new PagedResultDto<CategoryListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取CategoryListDto信息
        /// </summary>

        public async Task<CategoryListDto> GetById(EntityDto<int> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<CategoryListDto>();
        }

        /// <summary>
        /// 获取编辑 Category
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetCategoryForEditOutput> GetForEdit(NullableIdDto<int> input)
        {
            var output = new GetCategoryForEditOutput();
            CategoryEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<CategoryEditDto>();

                //categoryEditDto = ObjectMapper.Map<List<categoryEditDto>>(entity);
            }
            else
            {
                editDto = new CategoryEditDto();
            }

            output.Category = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Category的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateCategoryInput input)
        {
            input.Category.ParentId = input.Category.ParentId ?? 0;
            if (input.Category.Id != 0 && input.Category.Id != null)
            {
                await Update(input.Category);
            }
            else
            {
                await Create(input.Category);
            }
        }


        /// <summary>
        /// 新增Category
        /// </summary>

        protected virtual async Task<CategoryEditDto> Create(CategoryEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Category>(input);
            var entity = input.MapTo<Category>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<CategoryEditDto>();
        }

        /// <summary>
        /// 编辑Category
        /// </summary>

        protected virtual async Task Update(CategoryEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Category信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Category的方法
        /// </summary>

        public async Task BatchDelete(List<int> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<APIResultDto> CategoryRemoveById(EntityDto<int> id)
        {
            int childCount = await _entityRepository.GetAll().Where(v => v.ParentId == id.Id).CountAsync();
            int docCount = await _documentRepository.GetAll().Where(v => v.CategoryId == id.Id).CountAsync();
            if (childCount != 0)
            {
                return new APIResultDto() { Code = 1, Msg = "存在子分类" };
            }
            else if (docCount != 0)
            {
                return new APIResultDto() { Code = 2, Msg = "存在文档" };
            }
            else
            {
                await _entityRepository.DeleteAsync(id.Id);
                return new APIResultDto() { Code = 0, Msg = "删除成功" };
            }
        }

        private List<CategoryTreeNode> GetTrees(int pid, List<Category> categoryList)
        {
            var catQuery = categoryList.Where(c => c.ParentId == pid)
                            .Select(c => new CategoryTreeNode()
                            {
                                key = c.Id.ToString(),
                                title = c.Name,
                                ParentId = c.ParentId,
                                children = GetTrees(c.Id, categoryList)
                            });
            return catQuery.ToList();
        }

        public async Task<List<CategoryTreeNode>> GetTreeAsync(long? deptId)
        {
            if (!deptId.HasValue)
            {
                return new List<CategoryTreeNode>();
            }
            var categoryList = await _entityRepository.GetAll().WhereIf(deptId.HasValue, e => e.DeptId == deptId).ToListAsync();
            return GetTrees(0, categoryList);
        }

        /// <summary>
        /// 递归获取父级信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private void GetCurrentName(int id, ref string result)
        {
            var entity = _entityRepository.GetAll().Where(v => v.Id == id).AsNoTracking().FirstOrDefault();
            result = $"{entity.Name} / " + result;
            if (entity.ParentId.Value != 0)
            {
                GetCurrentName(entity.ParentId.Value, ref result);
            }
        }

        /// <summary>
        /// 获取层级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetParentName(int id)
        {
            string result = "";
            var doc = await _entityRepository.GetAsync(id);
            result = doc.Name;
            if (doc.ParentId != 0)
            {
                GetCurrentName(doc.ParentId.Value, ref result);
            }
            return result;
        }

        /// <summary>
        /// 按照部门id获取标准分类
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectGroups>> GetCategoryTypeByDeptAsync()
        {
            var curUser = await GetCurrentUserAsync();
            var deptId = await _employeeRepository.GetAll().Where(v => v.Id == curUser.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var entity = await (from c in _entityRepository.GetAll().Where(v => "[" + v.DeptId + "]" == deptId)
                                select new
                                {
                                    text = c.Name,
                                    value = c.Id,
                                }).OrderBy(v => v.value).ToListAsync();
            var result = entity.MapTo<List<SelectGroups>>();
            return result;
        }
    }
}


