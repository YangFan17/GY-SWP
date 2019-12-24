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
using GYSWP.Dtos;
using GYSWP.Helpers;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Hosting;

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
        private readonly IHostingEnvironment _hostingEnvironment;


        public SuperviseAppService(IRepository<CriterionExamine, Guid> criterionExaminesRepository,
            IRepository<ExamineDetail, Guid> examineDetailsRepository,
            IRepository<Employee, string> employeeRepository,
            IRepository<Indicator, Guid> indicatorRepository,
            IRepository<IndicatorsDetail, Guid> indicatorsDetailRepository,
            IHostingEnvironment hostingEnvironment,
            IRepository<Organization, long> organizationRepository)
        {
            _criterionExaminesRepository = criterionExaminesRepository;
            _examineDetailsRepository = examineDetailsRepository;
            _employeeRepository = employeeRepository;
            _indicatorRepository = indicatorRepository;
            _indicatorsDetailRepository = indicatorsDetailRepository;
            _organizationRepository = organizationRepository;
            _hostingEnvironment = hostingEnvironment;
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
            if (input.DeptId.HasValue)
            {
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

                var dataList = await query.WhereIf(!string.IsNullOrEmpty(input.UserName), q => q.UserName.Contains(input.UserName)).ToListAsync();
                if (dataList.Count > 0)
                {
                    var sum = new SuperviseDto();
                    sum.UserName = "汇总";
                    sum.Position = "/";
                    sum.ExamineTitle = "/";
                    sum.ExamineTime = DateTime.Today;
                    sum.ExamineNum = dataList.Sum(v => v.ExamineNum);
                    sum.NotUpNum = dataList.Sum(v => v.NotUpNum);
                    sum.OkNum = dataList.Sum(v => v.OkNum);
                    sum.NotFinished = dataList.Sum(v => v.NotFinished);
                    dataList.Add(sum);
                }
                return dataList;
            }
            else //顶级部门
            {
                List<SuperviseDto> dataList = new List<SuperviseDto>();
                var criterion = _criterionExaminesRepository.GetAll().Where(c => c.IsPublish == true && c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime);
                var detail = _examineDetailsRepository.GetAll();
                var query = from c in criterion
                            join d in detail on c.Id equals d.CriterionExamineId
                            select new
                            {
                                c.Id,
                                d.Result
                            };
                var sum = new SuperviseDto();
                sum.UserName = "四川省烟草公司广元市公司";
                sum.Position = "/";
                sum.ExamineTitle = "/";
                sum.ExamineTime = DateTime.Today;
                sum.ExamineNum = await query.CountAsync();
                sum.NotUpNum = await query.CountAsync(v => v.Result == ExamineStatus.不合格);
                sum.OkNum = await query.CountAsync(v => v.Result == ExamineStatus.合格);
                sum.NotFinished = await query.CountAsync(v => v.Result == ExamineStatus.未检查);
                dataList.Add(sum);
                return dataList;
            }
        }

        /// <summary>
        /// 目标指标检查统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<IndicatorSuperviseDto>> GetIndicatorSuperviseReportDataAsync(IndicatorSuperviseInputDto input)
        {
            input.EndTime = input.EndTime.AddDays(1);
            if (input.DeptId == 1)
            {
                var dept = await _organizationRepository.FirstOrDefaultAsync(input.DeptId);
                var baseQuery = _indicatorsDetailRepository.GetAll().Where(c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime);
                int totalQuery = await baseQuery.CountAsync();
                int notFillNum = await baseQuery.Where(i => i.Status == IndicatorStatus.未填写).CountAsync();
                int okNum = await baseQuery.Where(i => i.Status == IndicatorStatus.已达成).CountAsync();
                int notFinishedNum = await baseQuery.Where(i => i.Status == IndicatorStatus.未达成).CountAsync();
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
            else
            {
                var dept = await _organizationRepository.FirstOrDefaultAsync(input.DeptId);
                var deptIds = GetDeptIds(dept.Id);
                deptIds.Add(dept.Id);
                var baseQuery = _indicatorsDetailRepository.GetAll().Where(c => deptIds.Contains(c.DeptId) && c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime);

                int totalQuery = await baseQuery.CountAsync();

                int notFillNum = await baseQuery.Where(i => i.Status == IndicatorStatus.未填写).CountAsync();

                int okNum = await baseQuery.Where(i => i.Status == IndicatorStatus.已达成).CountAsync();

                int notFinishedNum = await baseQuery.Where(i => i.Status == IndicatorStatus.未达成).CountAsync();

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
        }

        private List<long> GetDeptIds(long id)
        {
            var deptIds = _organizationRepository.GetAll().Where(i => i.ParentId == id).Select(i => i.Id).ToList();
            foreach (var item in deptIds)
            {
                GetDeptIds(item);
            }
            return deptIds;
        }


        /// <summary>
        /// 标办检查汇总
        /// </summary>
        /// <returns></returns>
        public async Task<List<SuperviseSummaryDto>> GetQGSuperviseSummaryAsync(SuperviseInputDto input)
        {
            List<SuperviseSummaryDto> list = new List<SuperviseSummaryDto>();
            var adminList = await GetUsersInRoleAsync("QiGuanAdmin");
            string[] adminIds = adminList.Select(v => v.EmployeeId).ToArray();
            var orgList = await _organizationRepository.GetAll().Where(v => v.ParentId == 1).OrderBy(v => v.Order).Select(v => new { v.Id, v.DepartmentName }).ToListAsync();

            foreach (var item in orgList)
            {
                SuperviseSummaryDto entity = new SuperviseSummaryDto();
                entity.DeptName = item.DepartmentName;
                Guid[] examineId = await _criterionExaminesRepository.GetAll().Where(v => v.Type == CriterionExamineType.标办考核 && v.IsPublish == true && adminIds.Contains(v.CreatorEmpeeId) && v.DeptId == item.Id && v.CreationTime >= input.BeginTime && v.CreationTime < input.EndTime).Select(v => v.Id).ToArrayAsync();
                entity.ExamineNum = await _examineDetailsRepository.CountAsync(v => examineId.Contains(v.CriterionExamineId) && v.CreationTime >= input.BeginTime && v.CreationTime < input.EndTime);
                entity.NotUpNum = await _examineDetailsRepository.CountAsync(v => examineId.Contains(v.CriterionExamineId) && v.Result == ExamineStatus.不合格 && v.CreationTime >= input.BeginTime && v.CreationTime < input.EndTime);
                entity.OkNum = await _examineDetailsRepository.CountAsync(v => examineId.Contains(v.CriterionExamineId) && v.Result == ExamineStatus.合格 && v.CreationTime >= input.BeginTime && v.CreationTime < input.EndTime);
                entity.NotFinished = await _examineDetailsRepository.CountAsync(v => examineId.Contains(v.CriterionExamineId) && v.Result == ExamineStatus.未检查 && v.CreationTime >= input.BeginTime && v.CreationTime < input.EndTime);
                list.Add(entity);
            }
            return list;
        }


        public async Task<APIResultDto> ExportQGSuperviseSummary(SuperviseInputDto input)
        {
            try
            {
                var exportData = await GetQGSuperviseSummaryAsync(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateQGSuperviseSummaryExcel("标办检查汇总.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportQGSuperviseSummary errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }


        /// <summary>
        /// 创建标办检查汇总表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateQGSuperviseSummaryExcel(string fileName, List<SuperviseSummaryDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("InspectionRecorde");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "部门", "抽查条数", "合格", "不合格", "未完成", "执行率", "达成率"};
                var fontTitle = workbook.CreateFont();
                fontTitle.IsBold = true;
                for (int i = 0; i < titles.Length; i++)
                {
                    var cell = titleRow.CreateCell(i);
                    cell.CellStyle.SetFont(fontTitle);
                    cell.SetCellValue(titles[i]);
                }
                var font = workbook.CreateFont();
                foreach (var item in data)
                {
                    rowIndex++;
                    IRow row = sheet.CreateRow(rowIndex);
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.DeptName);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.ExamineNum);
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.OkNum);
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.NotUpNum);
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.NotFinished);
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.ImplementRate);
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.ReachRate);
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}
