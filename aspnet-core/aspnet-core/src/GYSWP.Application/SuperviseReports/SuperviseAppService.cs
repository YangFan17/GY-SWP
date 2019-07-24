using Abp.Domain.Repositories;
using GYSWP.CriterionExamines;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GYSWP.ExamineDetails;
using GYSWP.SuperviseReports.Dtos;
using GYSWP.Employees;
using GYSWP.GYEnums;
using Abp.Collections.Extensions;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using GYSWP.Indicators;
using GYSWP.IndicatorsDetails;
using GYSWP.Organizations;

namespace GYSWP.SuperviseReports
{
    public class SuperviseAppService : GYSWPAppServiceBase, ISuperviseAppService
    {
        private readonly IRepository<CriterionExamine, Guid> _criterionExaminesRepository;
        private readonly IRepository<ExamineDetail, Guid> _examineDetailsRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<Indicator, Guid> _indicatorRepository;
        private readonly IRepository<IndicatorsDetail, Guid> _indicatorsDetailRepository;
        private readonly IRepository<Organization, long> _organizationRepository;

        public SuperviseAppService(IRepository<CriterionExamine, Guid> criterionExaminesRepository, 
            IRepository<ExamineDetail, Guid> examineDetailsRepository,
            IRepository<Employee, string> employeeRepository,
            IRepository<Indicator, Guid> indicatorRepository,
            IRepository<IndicatorsDetail, Guid> indicatorsDetailRepository,
            IRepository<Organization, long> organizationRepository)
        {
            _criterionExaminesRepository = criterionExaminesRepository;
            _examineDetailsRepository = examineDetailsRepository;
            _employeeRepository = employeeRepository;
            _indicatorRepository = indicatorRepository;
            _indicatorsDetailRepository = indicatorsDetailRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<Dictionary<Guid, string>> GetSupervisesAsync(DateTime month, long deptId)
        {
            var beginTime = new DateTime(month.Year, month.Month, 1);
            var endTime = new DateTime(month.Year, month.Month + 1, 1);
            var query = _criterionExaminesRepository.GetAll().Where(c => c.CreationTime >= beginTime && c.CreationTime < endTime && c.DeptId == deptId);
            var reslist = await query.ToListAsync();
            return reslist.ToDictionary(key => key.Id, val => val.Type.ToString() + " - " + val.Title);
        }

        public async Task<List<SuperviseDto>> GetSuperviseReportDataAsync(SuperviseInputDto input)
        {
            input.EndTime = input.EndTime.AddDays(1);
            var baseQuery = _criterionExaminesRepository.GetAll().Where(c => c.DeptId == input.DeptId && c.IsPublish == true && c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime);

            var totalQuery = from cr in baseQuery
                             join ex in _examineDetailsRepository.GetAll()
                             on cr.Id equals ex.CriterionExamineId
                             //where ex.Result == ExamineStatus.不合格
                             group new { ex.EmployeeId, ex.CriterionExamineId, ex.Id } by new { ex.EmployeeId, ex.CriterionExamineId } into gtemp
                             select new
                             {
                                 gtemp.Key.EmployeeId,
                                 gtemp.Key.CriterionExamineId,
                                 TotalNum = gtemp.Count()
                             };

            var notUPQuery = from cr in baseQuery
                             join ex in _examineDetailsRepository.GetAll()
                             on cr.Id equals ex.CriterionExamineId
                             where ex.Result == ExamineStatus.不合格
                             group new { ex.EmployeeId, ex.CriterionExamineId, ex.Id } by new { ex.EmployeeId, ex.CriterionExamineId } into gtemp
                             select new
                             {
                                 gtemp.Key.EmployeeId,
                                 gtemp.Key.CriterionExamineId,
                                 NotUpNum = gtemp.Count()
                             };
           
            var okQuery = from cr in baseQuery
                             join ex in _examineDetailsRepository.GetAll()
                             on cr.Id equals ex.CriterionExamineId
                             where ex.Result == ExamineStatus.合格
                             group new { ex.EmployeeId, ex.CriterionExamineId, ex.Id } by new { ex.EmployeeId, ex.CriterionExamineId } into gtemp
                             select new
                             {
                                 gtemp.Key.EmployeeId,
                                 gtemp.Key.CriterionExamineId,
                                 OkNum = gtemp.Count()
                             };

            var notFinishedQuery = from cr in baseQuery
                             join ex in _examineDetailsRepository.GetAll()
                             on cr.Id equals ex.CriterionExamineId
                             where ex.Result == ExamineStatus.未检查
                             group new { ex.EmployeeId, ex.CriterionExamineId, ex.Id } by new { ex.EmployeeId, ex.CriterionExamineId } into gtemp
                             select new
                             {
                                 gtemp.Key.EmployeeId,
                                 gtemp.Key.CriterionExamineId,
                                 NotFinishedNum = gtemp.Count()
                             };

            var query = from t in totalQuery
                        join c in _criterionExaminesRepository.GetAll() on t.CriterionExamineId equals c.Id
                        join e in _employeeRepository.GetAll() on t.EmployeeId equals e.Id
                        join np in notUPQuery on new { t.EmployeeId, t.CriterionExamineId } equals new { np.EmployeeId, np.CriterionExamineId } into nptemp
                        from lnp in nptemp.DefaultIfEmpty()
                        join ok in okQuery on new { t.EmployeeId, t.CriterionExamineId } equals new { ok.EmployeeId, ok.CriterionExamineId } into oktemp
                        from lok in oktemp.DefaultIfEmpty()
                        join nf in notFinishedQuery on new { t.EmployeeId, t.CriterionExamineId } equals new { nf.EmployeeId, nf.CriterionExamineId } into nftemp
                        from lnf in nftemp.DefaultIfEmpty()
                        select new SuperviseDto()
                        {
                            UserName = e.Name,
                            Position = e.Position,
                            ExamineTitle = "[" + c.Type.ToString() + "]" + c.Title,
                            ExamineTime = c.CreationTime,
                            ExamineNum = t.TotalNum,
                            NotUpNum = lnp == null ? 0 : lnp.NotUpNum,
                            OkNum = lok == null ? 0 : lok.OkNum,
                            NotFinished = lnf == null ? 0 : lnf.NotFinishedNum
                        };

            var dataList = await query.WhereIf(!string.IsNullOrEmpty(input.UserName), q => q.UserName.Contains(input.UserName)).AsNoTracking().ToListAsync();
            return dataList;
        }

        public async Task<List<IndicatorSuperviseDto>> GetIndicatorSuperviseReportDataAsync(IndicatorSuperviseInputDto input)
        {
            input.EndTime = input.EndTime.AddDays(1);
            
            var dept = await _organizationRepository.FirstOrDefaultAsync(input.DeptId);

            var deptIds = GetDeptIds(dept.Id);
            deptIds.Add(dept.Id);

            var baseQuery = _indicatorsDetailRepository.GetAll().Where(c => deptIds.Contains(c.DeptId) && c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime);
            
            int totalQuery = baseQuery.Count();

            int notFillNum = baseQuery.Where(i => i.Status == IndicatorStatus.未填写).Count();

            int okNum = baseQuery.Where(i => i.Status == IndicatorStatus.已达成).Count();

            int notFinishedNum = baseQuery.Where(i => i.Status == IndicatorStatus.未达成).Count();

            return new List<IndicatorSuperviseDto>()
            {
                new IndicatorSuperviseDto
                {
                    DeptName = dept.DepartmentName,
                    IndicatorTotal = totalQuery,
                    NotFillNum = notFillNum,
                    OkNum = okNum,
                    NotFinishedNum = notFinishedNum
                }
            };
        }

        private List<long> GetDeptIds(long id)
        {
            var deptIds = _organizationRepository.GetAll().Where(i => i.ParentId == id).Select(i=>i.Id).ToList();
            foreach (var item in deptIds)
            {
                GetDeptIds(item);
            }
            return deptIds;
        } 
    }
}
