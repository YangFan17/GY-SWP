using Abp.Application.Services;
using GYSWP.StandardRevisionReports.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GYSWP.StandardRevisionReports
{
    public interface IStandardRevisionAppService : IApplicationService
    {
        Task<List<StandardRevisionDto>> GetSearchStandardRevisions(StandardRevisionInputDto input);
    }
}
