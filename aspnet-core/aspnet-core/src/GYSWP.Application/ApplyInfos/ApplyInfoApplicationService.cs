
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


using GYSWP.ApplyInfos;
using GYSWP.ApplyInfos.Dtos;
using GYSWP.ApplyInfos.DomainService;
using GYSWP.DingDingApproval;
using GYSWP.Dtos;

namespace GYSWP.ApplyInfos
{
    /// <summary>
    /// ApplyInfo应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ApplyInfoAppService : GYSWPAppServiceBase, IApplyInfoAppService
    {
        private readonly IRepository<ApplyInfo, Guid> _entityRepository;
        private readonly IApplyInfoManager _entityManager;
        private readonly IApprovalAppService _approvalAppService;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ApplyInfoAppService(
        IRepository<ApplyInfo, Guid> entityRepository
        , IApplyInfoManager entityManager
        , IApprovalAppService approvalAppService
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _approvalAppService = approvalAppService;
        }


        /// <summary>
        /// 获取ApplyInfo的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<ApplyInfoListDto>> GetPaged(GetApplyInfosInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ApplyInfoListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ApplyInfoListDto>>();

            return new PagedResultDto<ApplyInfoListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取ApplyInfoListDto信息
        /// </summary>

        public async Task<ApplyInfoListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<ApplyInfoListDto>();
        }

        /// <summary>
        /// 获取编辑 ApplyInfo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetApplyInfoForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetApplyInfoForEditOutput();
            ApplyInfoEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<ApplyInfoEditDto>();

                //applyInfoEditDto = ObjectMapper.Map<List<applyInfoEditDto>>(entity);
            }
            else
            {
                editDto = new ApplyInfoEditDto();
            }

            output.ApplyInfo = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改ApplyInfo的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateApplyInfoInput input)
        {

            if (input.ApplyInfo.Id.HasValue)
            {
                await Update(input.ApplyInfo);
            }
            else
            {
                await Create(input.ApplyInfo);
            }
        }


        /// <summary>
        /// 新增ApplyInfo
        /// </summary>

        protected virtual async Task<ApplyInfoEditDto> Create(ApplyInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <ApplyInfo>(input);
            var entity = input.MapTo<ApplyInfo>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<ApplyInfoEditDto>();
        }

        /// <summary>
        /// 编辑ApplyInfo
        /// </summary>

        protected virtual async Task Update(ApplyInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除ApplyInfo信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除ApplyInfo的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 标准修订申请并提交审核
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ApplyDocAsync(ApplyInfoEditDto input)
        {
            var user = await GetCurrentUserAsync();
            input.EmployeeId = user.EmployeeId;
            input.EmployeeName = user.EmployeeName;
            input.Type = GYEnums.ApplyType.制修订申请;
            input.Status = GYEnums.ApplyStatus.待审批;
            input.CreationTime = DateTime.Now;
            try
            {
                var result = await _approvalAppService.SubmitDocApproval(input.Reason, input.Content, input.CreationTime,input.OperateType);
                if(result.Code != 0)
                {
                    Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0}", result.Data);
                    return new APIResultDto() { Code = 901, Msg = "标准制修订申请失败" };
                }
                input.ProcessInstanceId = result.Data.ToString();
                await Create(input);
                return new APIResultDto() { Code = 0, Msg = "标准制修订申请成功" };
            }
            catch (Exception ex)
            {

                Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "标准制修订申请失败" };
            }
        }

        /// <summary>
        /// 回调后修改状态
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task UpdateApplyInfoByPIIdAsync(string pIId,string result)
        {

            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.ProcessInstanceId == pIId);
            entity.HandleTime = DateTime.Now;
            if (result == "agree")
            {
                entity.Status = GYEnums.ApplyStatus.审批通过;
            }
            else
            {
                entity.Status = GYEnums.ApplyStatus.审批拒绝;
            }
            await _entityRepository.UpdateAsync(entity);
        }
    }
}