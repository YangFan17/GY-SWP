
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


using GYSWP.EmployeeClauses;
using GYSWP.EmployeeClauses.Dtos;
using GYSWP.EmployeeClauses.DomainService;
using GYSWP.Dtos;

namespace GYSWP.EmployeeClauses
{
    /// <summary>
    /// EmployeeClause应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class EmployeeClauseAppService : GYSWPAppServiceBase, IEmployeeClauseAppService
    {
        private readonly IRepository<EmployeeClause, Guid> _entityRepository;

        private readonly IEmployeeClauseManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public EmployeeClauseAppService(
        IRepository<EmployeeClause, Guid> entityRepository
        , IEmployeeClauseManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取EmployeeClause的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<EmployeeClauseListDto>> GetPaged(GetEmployeeClausesInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<EmployeeClauseListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<EmployeeClauseListDto>>();

            return new PagedResultDto<EmployeeClauseListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取EmployeeClauseListDto信息
        /// </summary>

        public async Task<EmployeeClauseListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<EmployeeClauseListDto>();
        }

        /// <summary>
        /// 获取编辑 EmployeeClause
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetEmployeeClauseForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetEmployeeClauseForEditOutput();
            EmployeeClauseEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<EmployeeClauseEditDto>();

                //employeeClauseEditDto = ObjectMapper.Map<List<employeeClauseEditDto>>(entity);
            }
            else
            {
                editDto = new EmployeeClauseEditDto();
            }

            output.EmployeeClause = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改EmployeeClause的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateEmployeeClauseInput input)
        {

            if (input.EmployeeClause.Id.HasValue)
            {
                await Update(input.EmployeeClause);
            }
            else
            {
                await Create(input.EmployeeClause);
            }
        }


        /// <summary>
        /// 新增EmployeeClause
        /// </summary>

        protected virtual async Task<EmployeeClauseEditDto> Create(EmployeeClauseEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <EmployeeClause>(input);
            var entity = input.MapTo<EmployeeClause>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<EmployeeClauseEditDto>();
        }

        /// <summary>
        /// 编辑EmployeeClause
        /// </summary>

        protected virtual async Task Update(EmployeeClauseEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除EmployeeClause信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除EmployeeClause的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> GetIsConfirmAsync(ConfirmClauseInput input)
        {
            var user = await GetCurrentUserAsync();
            int count = await _entityRepository.GetAll().Where(v => v.DocumentId == input.DocId && v.EmployeeId == user.EmployeeId).CountAsync();
            if (count != 0)
            {
                return new APIResultDto() { Code = 0, Msg = "ok", Data = true };
            }
            else
            {
                return new APIResultDto() { Code = 0, Msg = "ok", Data = false };
            }
        }


        /// <summary>
        /// 确认条款
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ConfirmClauseAsync(ConfirmClauseInput input)
        {
            var user = await GetCurrentUserAsync();
            await DeleteAll(user.EmployeeId, input.DocId);
            foreach (var item in input.ClauseIds)
            {
                //bool isDelete = await NotExistClauseAsync(item, user.EmployeeId, input.DocId);
                //if (isDelete == false)
                //{
                var entity = new EmployeeClause();
                entity.ClauseId = item;
                entity.EmployeeId = user.EmployeeId;
                entity.EmployeeName = user.EmployeeName;
                entity.DocumentId = input.DocId;
                await _entityRepository.InsertAsync(entity);
                //}
            }
            return new APIResultDto() { Code = 0, Msg = "保存成功" };
        }

        /// <summary>
        /// 删除取消的条款
        /// </summary>
        /// <param name="clauseId"></param>
        /// <param name="userId"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        //private async Task<bool> NotExistClauseAsync(Guid clauseId, string userId, Guid docId)
        //{
        //    var id = await _entityRepository.GetAll().Where(v => v.EmployeeId == userId && v.DocumentId == docId && v.ClauseId == clauseId).Select(v => v.Id).FirstOrDefaultAsync();
        //    if (id != Guid.Empty)
        //    {
        //        await _entityRepository.DeleteAsync(id);
        //        await CurrentUnitOfWork.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// 全部删除
        /// </summary>
        /// <param name="clauseId"></param>
        /// <param name="userId"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        private async Task DeleteAll(string userId, Guid docId)
        {
            var ids = await _entityRepository.GetAll().Where(v => v.EmployeeId == userId && v.DocumentId == docId).Select(v => v.Id).ToListAsync();
            await BatchDelete(ids);
        }
    }
}
