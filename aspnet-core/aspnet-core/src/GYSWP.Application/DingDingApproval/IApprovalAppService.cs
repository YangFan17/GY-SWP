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
        Task<APIResultDto> SendMessageToQGAdminAsync(string docName, Guid docId);
    }
}
