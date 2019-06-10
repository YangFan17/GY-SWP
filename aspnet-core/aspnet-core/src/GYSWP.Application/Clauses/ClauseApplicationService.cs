
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


using GYSWP.Clauses;
using GYSWP.Clauses.Dtos;
using GYSWP.Clauses.DomainService;
using GYSWP.Dtos;
using GYSWP.EmployeeClauses;

namespace GYSWP.Clauses
{
    /// <summary>
    /// Clause应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ClauseAppService : GYSWPAppServiceBase, IClauseAppService
    {
        private readonly IRepository<Clause, Guid> _entityRepository;
        private readonly IRepository<EmployeeClause, Guid> _employeeClauseRepository;
        private readonly IClauseManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ClauseAppService(
        IRepository<Clause, Guid> entityRepository
        , IClauseManager entityManager
        , IRepository<EmployeeClause, Guid> employeeClauseRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _employeeClauseRepository = employeeClauseRepository;
        }


        /// <summary>
        /// 获取Clause的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<ClauseListDto>> GetPaged(GetClausesInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ClauseListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ClauseListDto>>();

            return new PagedResultDto<ClauseListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 获取子条款
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clauseList"></param>
        /// <returns></returns>
        private List<ClauseTreeNodeDto> GetChildren(Guid? id, List<ClauseTreeListDto> clauseList)
        {
            var list = clauseList.Where(c => c.ParentId == id).Select(c => new ClauseTreeNodeDto()
            {
                Id = c.Id,
                ClauseNo = c.ClauseNo,
                Title = c.Title,
                Content = c.Content,
                ParentId = c.ParentId,
                Children = GetChildren(c.Id, clauseList)
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取条款树形表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ClauseTreeNodeDto>> GetClauseTreeAsync(GetClausesInput input)
        {
            var clause = await _entityRepository.GetAll().Where(v => v.DocumentId == input.DocumentId).Select(v=>new ClauseTreeListDto()
            {
                Id = v.Id,
                ClauseNo =v.ClauseNo,
                Title =v.Title,
                Content =v.Content,
                ParentId =v.ParentId
            }).OrderBy(v=>v.ClauseNo).ToListAsync();
            return GetChildren(null, clause);
        }


        /// <summary>
        /// 通过指定id获取ClauseListDto信息
        /// </summary>

        public async Task<ClauseListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);
            return entity.MapTo<ClauseListDto>();
        }

        /// <summary>
        /// 获取编辑 Clause
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetClauseForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetClauseForEditOutput();
            ClauseEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<ClauseEditDto>();

                //clauseEditDto = ObjectMapper.Map<List<clauseEditDto>>(entity);
            }
            else
            {
                editDto = new ClauseEditDto();
            }

            output.Clause = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Clause的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        //public async Task CreateOrUpdate(CreateOrUpdateClauseInput input)
        //{

        //	if (input.Clause.Id.HasValue)
        //	{
        //		await Update(input.Clause);
        //	}
        //	else
        //	{
        //		await Create(input.Clause);
        //	}
        //}
        public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateClauseInput input)
        {
            if (string.IsNullOrEmpty(input.Clause.Content))
            {
                input.Clause.Content = null;
            }
            if (string.IsNullOrEmpty(input.Clause.Title))
            {
                input.Clause.Title = null;
            }
            if (input.Clause.Id.HasValue)
            {
                await Update(input.Clause);
                return new APIResultDto() { Code = 0, Msg = "保存成功" };
            }
            else
            {
                var entity = await Create(input.Clause);
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
            }
        }

        /// <summary>
        /// 新增Clause
        /// </summary>

        protected virtual async Task<ClauseEditDto> Create(ClauseEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Clause>(input);
            var entity = input.MapTo<Clause>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<ClauseEditDto>();
        }

        /// <summary>
        /// 编辑Clause
        /// </summary>

        protected virtual async Task Update(ClauseEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Clause信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Clause的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task<APIResultDto> ClauseRemoveById(EntityDto<Guid> id)
        {
            int childCount = await _entityRepository.GetAll().Where(v => v.ParentId == id.Id).CountAsync();
            if (childCount != 0)
            {
                return new APIResultDto() { Code = 1, Msg = "存在子条款" };
            }
            else
            {
                await _entityRepository.DeleteAsync(id.Id);
                return new APIResultDto() { Code = 0, Msg = "删除成功" };
            }
        }


        /// <summary>
        /// 获取子条款
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clauseList"></param>
        /// <returns></returns>
        private List<ClauseTreeNodeDto> GetChildrenWithChecked(Guid? id, List<ClauseTreeListDto> clauseList)
        {
            var list = clauseList.Where(c => c.ParentId == id).Select(c => new ClauseTreeNodeDto()
            {
                Id = c.Id,
                ClauseNo = c.ClauseNo,
                Title = c.Title,
                Content = c.Content,
                ParentId = c.ParentId,
                Checked = c.Checked,
                Children = GetChildrenWithChecked(c.Id, clauseList)
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取确认后的条款树形表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ClauseTreeNodeDto>> GetClauseTreeWithCheckedAsync(GetClausesInput input)
        {
            var user = await GetCurrentUserAsync();
            var confirmIds = await _employeeClauseRepository.GetAll().Where(v => v.DocumentId == input.DocumentId && v.EmployeeId == user.EmployeeId).Select(v => v.ClauseId).ToListAsync();

            var clause = await _entityRepository.GetAll().Where(v => v.DocumentId == input.DocumentId).Select(v => new ClauseTreeListDto()
            {
                Id = v.Id,
                ClauseNo = v.ClauseNo,
                Title = v.Title,
                Content = v.Content,
                ParentId = v.ParentId
            }).OrderBy(v => v.ClauseNo).ToListAsync();
            foreach (var item in clause)
            {
                foreach (var cnfmId in confirmIds)
                {
                    if(item.Id == cnfmId)
                    {
                        item.Checked = true;
                        break;
                    }
                }
            }

            return GetChildrenWithChecked(null, clause);
        }
    }
}


