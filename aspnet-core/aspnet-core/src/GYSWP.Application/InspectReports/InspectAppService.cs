using Abp.Authorization;
using Abp.Domain.Repositories;
using GYSWP.EmployeeClauses;
using GYSWP.Employees;
using GYSWP.Organizations;
using GYSWP.SelfChekRecords;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using GYSWP.SelfChekRecords.Dtos;

namespace GYSWP.InspectReports
{
    [AbpAuthorize]
    public class InspectAppService : GYSWPAppServiceBase, IInspectAppService
    {
        private readonly ISelfChekRecordRepository _selfChekRecordRepository;

        public InspectAppService(ISelfChekRecordRepository selfChekRecordRepository)
        {
            _selfChekRecordRepository = selfChekRecordRepository;
        }

        //[AbpAllowAnonymous]
        public async Task<List<InspectDto>> GetSearchInspectReports(InspectInputDto input)
        {
            var res = await _selfChekRecordRepository.GetSearchInspectReports(input);
            return res;
        }
    }
}
