using Abp.Application.Services;
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
    }
}
