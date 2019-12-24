using Abp.Application.Services;
using GYSWP.Dtos;
using GYSWP.SuperviseReports.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GYSWP.SuperviseReports
{
    public interface ISuperviseAppService : IApplicationService
    {
        Task<Dictionary<Guid, string>> GetSupervisesAsync(DateTime month, long deptId);

        Task<List<SuperviseDto>> GetSuperviseReportDataAsync(SuperviseInputDto input);

        Task<List<IndicatorSuperviseDto>> GetIndicatorSuperviseReportDataAsync(IndicatorSuperviseInputDto input);
        Task<List<SuperviseSummaryDto>> GetQGSuperviseSummaryAsync(SuperviseInputDto input);
        Task<APIResultDto> ExportQGSuperviseSummary(SuperviseInputDto input);
    }
}
