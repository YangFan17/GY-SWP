using Abp.Application.Services;
using GYSWP.DingDingApproval.Dtos;
using GYSWP.Dtos;
using GYSWP.GYEnums;
using System;
using System.Threading.Tasks;

namespace GYSWP.DingDingApproval
{
    public interface IApprovalAppService : IApplicationService
    {
        Task<APIResultDto> SubmitDocApproval(string Reason, string Content, DateTime CreationTime, OperateType OperateType,string DocName);
        Task<APIResultDto> SubmitRevisionApproval(Guid ApplyInfoId, Guid DocumentId);
        Task<APIResultDto> SubmitDraftDocApproval(Guid ApplyInfoId, Guid DocumentId);
        APIResultDto SendMessageToQGAdminAsync(string docName, Guid docId);
        APIResultDto SendMessageToStandardAdminAsync(string docName, string empId);
        APIResultDto SendIndicatorMessageAsync(string empId);
        APIResultDto SendCriterionExamineMessageAsync(string empId);
        APIResultDto SendIndicatorResultAsync(IndicatorStatus status,string empList);
        Task<APIResultDto> SubmitAdviceApproval(Guid id);
        APIResultDto GetProcessinstanceSpace(string accessToken, string empId);
    }
}
