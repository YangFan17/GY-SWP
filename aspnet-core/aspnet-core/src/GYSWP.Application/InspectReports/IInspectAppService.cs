using Abp.Application.Services;
using GYSWP.SelfChekRecords.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GYSWP.InspectReports
{
    public interface IInspectAppService : IApplicationService
    {
        Task<List<InspectDto>> GetSearchInspectReports(InspectInputDto input);
    }
}
