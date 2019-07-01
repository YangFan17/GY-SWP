using Abp.Authorization;
using Abp.Domain.Repositories;
using GYSWP.EmployeeClauses;
using GYSWP.Employees;
using GYSWP.InspectReports.Dtos;
using GYSWP.Organizations;
using GYSWP.SelfChekRecords;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GYSWP.InspectReports
{
    [AbpAuthorize]
    public class InspectAppService : GYSWPAppServiceBase, IInspectAppService
    {
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<Organization, long> _organizationRepository;
        private readonly IRepository<SelfChekRecord, Guid> _selfChekRecordRepository;
        private readonly IRepository<EmployeeClause, Guid> _employeeClausesRepository;

        public InspectAppService(IRepository<Employee, string> employeeRepository
            , IRepository<Organization, long> organizationRepository
            , IRepository<SelfChekRecord, Guid> selfChekRecordRepository
            , IRepository<EmployeeClause, Guid> employeeClausesRepository)
        {
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _selfChekRecordRepository = selfChekRecordRepository;
            _employeeClausesRepository = employeeClausesRepository;
        }

        public async Task<List<InspectDto>> GetSearchInspectReports(InspectInputDto input)
        {
            //var query

            return new List<InspectDto>();
        }
    }
}
