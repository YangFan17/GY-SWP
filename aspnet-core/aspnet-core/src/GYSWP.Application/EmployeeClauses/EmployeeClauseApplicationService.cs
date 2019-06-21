
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
using GYSWP.ApplyInfos;
using GYSWP.ClauseRevisions;
using GYSWP.Organizations;
using GYSWP.Employees;

namespace GYSWP.EmployeeClauses
{
    /// <summary>
    /// EmployeeClause应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class EmployeeClauseAppService : GYSWPAppServiceBase, IEmployeeClauseAppService
    {
        private readonly IRepository<EmployeeClause, Guid> _entityRepository;
        private readonly IRepository<ApplyInfo, Guid> _applyInfoRepository;
        private readonly IEmployeeClauseManager _entityManager;
        private readonly IRepository<ClauseRevision, Guid> _clauseRevisionRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<Organization, long> _organizationRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public EmployeeClauseAppService(
        IRepository<EmployeeClause, Guid> entityRepository
        , IEmployeeClauseManager entityManager
        , IRepository<ApplyInfo, Guid> applyInfoRepository
        , IRepository<ClauseRevision, Guid> clauseRevisionRepository
        , IRepository<Employee, string> employeeRepository
        , IRepository<Organization, long> organizationRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _applyInfoRepository = applyInfoRepository;
            _clauseRevisionRepository = clauseRevisionRepository;
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
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
        /// 标准详情页获取用户操作权限相关信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> GetUserOperateAsync(ConfirmClauseInput input)
        {
            DocUserInfo resultData = new DocUserInfo();
            var user = await GetCurrentUserAsync();
            int count = await _entityRepository.CountAsync(v => v.DocumentId == input.DocId && v.EmployeeId == user.EmployeeId);
            var applyInfo = await _applyInfoRepository.GetAll().Where(v => v.EmployeeId == user.EmployeeId && v.DocumentId == input.DocId).OrderByDescending(v => v.CreationTime).FirstOrDefaultAsync();
            if (applyInfo != null)
            {
                //int revisionCount = await _clauseRevisionRepository.CountAsync(v => v.ApplyInfoId == applyInfo.Id);
                //是否申请制修订 审批等待阶段
                if (applyInfo.Status == GYEnums.ApplyStatus.待审批)
                {
                    resultData.IsApply = false;
                    resultData.EditModel = false;
                }
                else if (applyInfo.Status == GYEnums.ApplyStatus.审批通过 && applyInfo.ProcessingStatus == GYEnums.RevisionStatus.等待提交)
                {
                    resultData.IsRevision = true;
                    resultData.EditModel = true;
                    //resultData.IsApply = false;
                    resultData.ApplyId = applyInfo.Id;
                    //if (revisionCount != 0)
                    //{
                    //    resultData.IsSave = true;
                    //}
                }
                // 申请通过，审批通过or结束 （流程结束）
                else if (applyInfo.Status == GYEnums.ApplyStatus.审批通过 && (applyInfo.ProcessingStatus == GYEnums.RevisionStatus.审核拒绝 || applyInfo.ProcessingStatus == GYEnums.RevisionStatus.审核通过))
                {
                    resultData.IsApply = true;
                    resultData.IsRevisionOver = true;
                    //resultData.EditModel = false;
                    //resultData.IsRevision = false;
                }
                // 申请通过，审批等待阶段
                else if (applyInfo.Status == GYEnums.ApplyStatus.审批通过 && applyInfo.ProcessingStatus == GYEnums.RevisionStatus.待审核)
                {
                    resultData.IsRevisionWaitTime = true;
                    //resultData.EditModel = false;
                    //resultData.IsRevisionOver = false;
                    //resultData.IsApply = false;
                    //resultData.IsRevision = false;
                    //resultData.EditModel = false;
                }
                else
                {
                    resultData.IsApply = true;
                    resultData.EditModel = false;
                }
            }
            else
            {
                resultData.IsApply = true;
                //resultData.IsRevision = false;
                //resultData.EditModel = false;
            }

            //是否确认条款
            if (count != 0)
            {
                resultData.IsConfirm = true;
            }
            //else
            //{
            //    resultData.IsConfirm = false;
            //}
            return new APIResultDto() { Code = 0, Msg = "ok", Data = resultData };
        }

        /// <summary>
        /// 获取用户制定操作权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> GetUserOperateDraftAsync()
        {
            DocUserInfo resultData = new DocUserInfo();
            var user = await GetCurrentUserAsync();
            var applyInfo = await _applyInfoRepository.GetAll().Where(v => v.EmployeeId == user.EmployeeId && v.OperateType == GYEnums.OperateType.制定标准).OrderByDescending(v => v.CreationTime).FirstOrDefaultAsync();
            if (applyInfo != null)
            {
                //是否申请制修订 审批等待阶段
                if (applyInfo.Status == GYEnums.ApplyStatus.待审批)
                {
                    resultData.IsApply = false;
                    resultData.EditModel = false;
                }
                else if (applyInfo.Status == GYEnums.ApplyStatus.审批通过 && applyInfo.ProcessingStatus == GYEnums.RevisionStatus.等待提交)
                {
                    resultData.IsRevision = true;
                    resultData.EditModel = true;
                    resultData.ApplyId = applyInfo.Id;
                }
                // 申请通过，审批通过or结束 （流程结束）
                else if (applyInfo.Status == GYEnums.ApplyStatus.审批通过 && (applyInfo.ProcessingStatus == GYEnums.RevisionStatus.审核拒绝 || applyInfo.ProcessingStatus == GYEnums.RevisionStatus.审核通过))
                {
                    resultData.IsApply = true;
                    resultData.IsRevisionOver = true;
                }
                // 申请通过，审批等待阶段
                else if (applyInfo.Status == GYEnums.ApplyStatus.审批通过 && applyInfo.ProcessingStatus == GYEnums.RevisionStatus.待审核)
                {
                    resultData.IsRevisionWaitTime = true;
                    resultData.ApplyId = applyInfo.Id;
                }
                else
                {
                    resultData.IsApply = true;
                    resultData.EditModel = false;
                }
            }
            else
            {
                resultData.IsApply = true;
            }
            return new APIResultDto() { Code = 0, Msg = "ok", Data = resultData };
        }

        /// <summary>
        /// 判断用户是否为制定等待阶段（防止改参数跳过验证）
        /// </summary>
        /// <returns></returns>
        public async Task<APIResultDto> GetDraftOperateDraftAsync()
        {
            var user = await GetCurrentUserAsync();
            string deptId = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            string deptName = await _organizationRepository.GetAll().Where(v => "["+ v.Id + "]" == deptId).Select(v => v.DepartmentName).FirstOrDefaultAsync();
            var applyInfo = await _applyInfoRepository.GetAll().Where(v => v.EmployeeId == user.EmployeeId && v.OperateType == GYEnums.OperateType.制定标准).OrderByDescending(v => v.CreationTime).FirstOrDefaultAsync();
            bool IsRevisionWaitTime = false;
            if (applyInfo != null)
            {
                if (applyInfo.Status == GYEnums.ApplyStatus.审批通过 && applyInfo.ProcessingStatus == GYEnums.RevisionStatus.待审核)
                {
                    IsRevisionWaitTime = true;
                }
            }
            return new APIResultDto() { Code = 0, Msg = "ok", Data = new { IsRevisionWaitTime , deptName } };
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
