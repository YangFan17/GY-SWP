
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
using GYSWP.Organizations;

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
        private readonly IRepository<Organization, long> _organizationRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public CategoryAppService(
        IRepository<Category, int> entityRepository
        , IRepository<Document, Guid> documentRepository
        , ICategoryManager entityManager
        , IRepository<Employee, string> employeeRepository
        , IRepository<Organization, long> organizationRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _documentRepository = documentRepository;
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
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
            var zuofeiCategory = await _entityRepository.GetAll().Where(v => "[" + v.DeptId + "]" == deptId && v.Name == "作废标准库").Select(v => new { v.Id }).FirstOrDefaultAsync();
            var entity = await (from c in _entityRepository.GetAll().Where(v => "[" + v.DeptId + "]" == deptId && v.ParentId != 0 && v.ParentId!= zuofeiCategory.Id)
                                select new
                                {
                                    text = c.Name,
                                    value = c.Id,
                                }).OrderBy(v => v.value).ToListAsync();
            var result = entity.MapTo<List<SelectGroups>>();
            return result;
        }

        #region 一键生成默认标准库分类
        /// <summary>
        /// 一键生成默认标准库分类
        /// </summary>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> InitDeptmentCategory()
        {
            var list = await _organizationRepository.GetAll().Select(v => v.Id).ToListAsync();
            foreach (var item in list)
            {
                var dateNow = DateTime.Now;
                var userId = AbpSession.UserId;
                if (item != 1)
                {
                    #region 现行标准库
                    var category999 = new Category();
                    category999.Name = "现行标准库";
                    category999.DeptId = item;
                    category999.CreatorUserId = userId;
                    category999.CreationTime = dateNow;
                    category999.ParentId = 0;
                    category999.Desc = "[系统默认生成，无法修改]";
                    var id999 = await _entityRepository.InsertOrUpdateAndGetIdAsync(category999);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    var category0 = new Category();
                    category0.Name = "技术标准";
                    category0.DeptId = item;
                    category0.CreatorUserId = userId;
                    category0.CreationTime = dateNow;
                    category0.ParentId = id999;
                    category0.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category0);
                    var category1 = new Category();
                    category1.Name = "管理标准";
                    category1.DeptId = item;
                    category1.CreatorUserId = userId;
                    category1.CreationTime = dateNow;
                    category1.ParentId = id999;
                    category1.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category1);
                    var category2 = new Category();
                    category2.Name = "工作标准";
                    category2.DeptId = item;
                    category2.CreatorUserId = userId;
                    category2.CreationTime = dateNow;
                    category2.ParentId = id999;
                    category2.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category2);
                    var category3 = new Category();
                    category3.Name = "外来文件";
                    category3.DeptId = item;
                    category3.CreatorUserId = userId;
                    category3.CreationTime = dateNow;
                    category3.ParentId = id999;
                    category3.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category3);
                    var category4 = new Category();
                    category4.Name = "风险库";
                    category4.DeptId = item;
                    category4.CreatorUserId = userId;
                    category4.CreationTime = dateNow;
                    category4.ParentId = id999;
                    category4.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category4);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    #endregion
                    #region 作废标准库
                    var category000 = new Category();
                    category000.Name = "作废标准库";
                    category000.DeptId = item;
                    category000.CreatorUserId = userId;
                    category000.CreationTime = dateNow;
                    category000.ParentId = 0;
                    category000.Desc = "[系统默认生成，无法修改]";
                    var id000 = await _entityRepository.InsertAndGetIdAsync(category000);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    var category5 = new Category();
                    category5.Name = "技术标准";
                    category5.DeptId = item;
                    category5.CreatorUserId = userId;
                    category5.CreationTime = dateNow;
                    category5.ParentId = id000;
                    category5.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category5);
                    var category6 = new Category();
                    category6.Name = "管理标准";
                    category6.DeptId = item;
                    category6.CreatorUserId = userId;
                    category6.CreationTime = dateNow;
                    category6.ParentId = id000;
                    category6.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category6);
                    var category7 = new Category();
                    category7.Name = "工作标准";
                    category7.DeptId = item;
                    category7.CreatorUserId = userId;
                    category7.CreationTime = dateNow;
                    category7.ParentId = id000;
                    category7.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category7);
                    var category8 = new Category();
                    category8.Name = "外来文件";
                    category8.DeptId = item;
                    category8.CreatorUserId = userId;
                    category8.CreationTime = dateNow;
                    category8.ParentId = id000;
                    category8.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category8);
                    var category9 = new Category();
                    category9.Name = "风险库";
                    category9.DeptId = item;
                    category9.CreatorUserId = userId;
                    category9.CreationTime = dateNow;
                    category9.ParentId = id000;
                    category9.Desc = "[系统默认生成，无法修改]";
                    await _entityRepository.InsertAsync(category9);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    #endregion
                }
            }
            return new APIResultDto() { Code = 0, Msg = "生成成功" };
        }
        #endregion
    }
}