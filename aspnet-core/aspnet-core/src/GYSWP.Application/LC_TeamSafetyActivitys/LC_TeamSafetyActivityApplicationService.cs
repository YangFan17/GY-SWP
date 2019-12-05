
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using GYSWP.LC_TeamSafetyActivitys;
using GYSWP.LC_TeamSafetyActivitys.Dtos;
using GYSWP.LC_TeamSafetyActivitys.DomainService;
using Abp.Auditing;
using GYSWP.Dtos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using GYSWP.Employees;

namespace GYSWP.LC_TeamSafetyActivitys
{
    /// <summary>
    /// LC_TeamSafetyActivity应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_TeamSafetyActivityAppService : GYSWPAppServiceBase, ILC_TeamSafetyActivityAppService
    {
        private readonly IRepository<LC_TeamSafetyActivity, Guid> _entityRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_TeamSafetyActivityManager _entityManager;
        private readonly IRepository<Employee, string> _employeeRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_TeamSafetyActivityAppService(
        IRepository<LC_TeamSafetyActivity, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_TeamSafetyActivityManager entityManager,
        IRepository<Employee, string> employeeRepository
        )
        {
            _employeeRepository = employeeRepository;
            _entityRepository = entityRepository;
            _hostingEnvironment = hostingEnvironment;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取LC_TeamSafetyActivity的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<LC_TeamSafetyActivityListDto>> GetPaged(GetLC_TeamSafetyActivitysInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_TeamSafetyActivityListDto>>();
            return new PagedResultDto<LC_TeamSafetyActivityListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取LC_TeamSafetyActivityListDto信息
        /// </summary>

        public async Task<LC_TeamSafetyActivityListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<LC_TeamSafetyActivityListDto>();
        }

        /// <summary>
        /// 获取编辑 LC_TeamSafetyActivity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetLC_TeamSafetyActivityForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetLC_TeamSafetyActivityForEditOutput();
            LC_TeamSafetyActivityEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<LC_TeamSafetyActivityEditDto>();

                //lC_TeamSafetyActivityEditDto = ObjectMapper.Map<List<lC_TeamSafetyActivityEditDto>>(entity);
            }
            else
            {
                editDto = new LC_TeamSafetyActivityEditDto();
            }

            output.LC_TeamSafetyActivity = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改LC_TeamSafetyActivity的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdate(CreateOrUpdateLC_TeamSafetyActivityInput input)
        {

            if (input.LC_TeamSafetyActivity.Id.HasValue)
            {
                await Update(input.LC_TeamSafetyActivity);
            }
            else
            {
                await Create(input.LC_TeamSafetyActivity);
            }
        }


        /// <summary>
        /// 新增LC_TeamSafetyActivity
        /// </summary>

        protected virtual async Task<LC_TeamSafetyActivityEditDto> Create(LC_TeamSafetyActivityEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_TeamSafetyActivity>(input);
            var entity = input.MapTo<LC_TeamSafetyActivity>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<LC_TeamSafetyActivityEditDto>();
        }

        /// <summary>
        /// 编辑LC_TeamSafetyActivity
        /// </summary>

        protected virtual async Task Update(LC_TeamSafetyActivityEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除LC_TeamSafetyActivity信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除LC_TeamSafetyActivity的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 导出班组安全活动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportTeamSafetyActivity(GetLC_TeamSafetyActivitysInput input)
        {
            try
            {
                var exportData = await GetTeamSafetyActivityForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateTeamSafetyActivityExcel("班组安全活动.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportTeamSafetyActivity errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_TeamSafetyActivityListDto>> GetTeamSafetyActivityForExcel(GetLC_TeamSafetyActivitysInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_TeamSafetyActivityListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建班组安全活动
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateTeamSafetyActivityExcel(string fileName, List<LC_TeamSafetyActivityListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("TeamSafetyActivity");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "岗前安全例会", "劳动保护用品穿戴是否整齐", "人员身体状态有无异常", "通道机、立式机各烟仓条烟处于位置", "叉车通道是否畅通", "安全出口是否堵塞", "消防设施是否堵塞", "安全标识是否清晰", "安全标识有无脱落", "员工安全建议", "常规烟分拣量：条", "异型烟分拣量：条", "分拣开始时间", "分拣结束时间", "正常停机时间", "故障停机时间", "有无危险源、风险点", "有无非工作人员出入场", "有无违章作业现象", "分拣结束，电源、气源是否关闭", "车间门窗是否关闭", "现场安全监管", "岗位安全责任人", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.SafetyMeeting);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.IsSafeEquipOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.IsEmpHealth == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.IsTdjOrLsjOk == true ? "正常" : "异常");
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.IsAisleOk == true ? "是" : "否");           
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.IsExitBad == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.IsFireEquipBad == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.IsSafeMarkClean == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.IsSafeMarkFall == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.EmpSafeAdvice);
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.CommonCigaretNum.ToString());
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.ShapedCigaretNum.ToString());
                    ExcelHelper.SetCell(row.CreateCell(12), font, item.BeginSortTime?.ToString("yyyy-MM-dd hh:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(13), font, item.EndSortTime?.ToString("yyyy-MM-dd hh:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(14), font, item.NormalStopTime?.ToString("yyyy-MM-dd hh:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(15), font, item.AbnormalStopTime?.ToString("yyyy-MM-dd hh:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(16), font, item.IsNotDanger == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(17), font, item.IsOtherAdmittance == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(18), font, item.IsViolation == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(19), font, item.IsElcOrGasShut == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(20), font, item.IsCloseWindow == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(21), font, item.SafeSupervision);
                    ExcelHelper.SetCell(row.CreateCell(22), font, item.ResponsibleName);
                    ExcelHelper.SetCell(row.CreateCell(23), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(24), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }


        /// <summary>
        /// 导入需求预测数据
        /// </summary>
        /// <returns></returns>
        public async Task<APIResultDto> ImportTeamSafetyActivityExcelAsync()
        {
            //获取Excel数据
            var excelList = await GetTeamSafetyActivityDataAsync();
            //循环批量更新
            await UpdateAsyncTeamSafetyActivityData(excelList);
            return new APIResultDto() { Code = 0, Msg = "导入数据成功" };
        }
        /// <summary>
        /// 从上传的Excel读出数据
        /// </summary>
        private async Task<List<LC_TeamSafetyActivityEditDto>> GetTeamSafetyActivityDataAsync()
        {
            string fileName = _hostingEnvironment.WebRootPath + "/files/upload/班组安全活动.xlsx";
            var LC_TeamSafetyActivityList = new List<LC_TeamSafetyActivityEditDto>();
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheet("TeamSafetyActivity");
                if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                {
                    sheet = workbook.GetSheetAt(0);
                }

                if (sheet != null)
                {
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = 1; i <= rowCount; ++i)//排除首行标题
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        var TeamSafetyActivity = new LC_TeamSafetyActivityEditDto();
                        if (row.GetCell(0) != null)
                        {
                            TeamSafetyActivity.SafetyMeeting = row.GetCell(0).ToString();
                            TeamSafetyActivity.ResponsibleName = row.GetCell(1).ToString();
                            TeamSafetyActivity.EmpSafeAdvice = row.GetCell(9).ToString();
                            TeamSafetyActivity.CommonCigaretNum =int.Parse(row.GetCell(10).ToString());
                            TeamSafetyActivity.ShapedCigaretNum = int.Parse(row.GetCell(11).ToString());

                            if (!string.IsNullOrEmpty(row.GetCell(12).ToString()))
                            {
                                TeamSafetyActivity.BeginSortTime = Convert.ToDateTime(row.GetCell(12).ToString());
                            }
                            if (!string.IsNullOrEmpty(row.GetCell(13).ToString()))
                            {
                                TeamSafetyActivity.EndSortTime = Convert.ToDateTime(row.GetCell(13).ToString());
                            }
                            if (!string.IsNullOrEmpty(row.GetCell(14).ToString()))
                            {
                                TeamSafetyActivity.NormalStopTime = Convert.ToDateTime(row.GetCell(14).ToString());
                            }
                            if (!string.IsNullOrEmpty(row.GetCell(15).ToString()))
                            {
                                TeamSafetyActivity.AbnormalStopTime = Convert.ToDateTime(row.GetCell(15).ToString());
                            }

                            if (row.GetCell(1).ToString() == "是" || row.GetCell(1).ToString() == "有")
                            {
                                TeamSafetyActivity.IsSafeEquipOk = true;
                            }
                            else if (row.GetCell(1).ToString() == "否" || row.GetCell(1).ToString() == "无")
                            {
                                TeamSafetyActivity.IsSafeEquipOk = false;
                            }

                            if (row.GetCell(2).ToString() == "是" || row.GetCell(2).ToString() == "有")
                            {
                                TeamSafetyActivity.IsEmpHealth = true;
                            }
                            else if (row.GetCell(2).ToString() == "否" || row.GetCell(2).ToString() == "无")
                            {
                                TeamSafetyActivity.IsEmpHealth = false;
                            }

                            if (row.GetCell(3).ToString() == "是" || row.GetCell(3).ToString() == "有")
                            {
                                TeamSafetyActivity.IsTdjOrLsjOk = true;
                            }
                            else if (row.GetCell(3).ToString() == "否" || row.GetCell(3).ToString() == "无")
                            {
                                TeamSafetyActivity.IsTdjOrLsjOk = false;
                            }

                            if (row.GetCell(4).ToString() == "是" || row.GetCell(4).ToString() == "有")
                            {
                                TeamSafetyActivity.IsAisleOk = true;
                            }
                            else if (row.GetCell(4).ToString() == "否" || row.GetCell(4).ToString() == "无")
                            {
                                TeamSafetyActivity.IsAisleOk = false;
                            }

                            if (row.GetCell(5).ToString() == "是" || row.GetCell(5).ToString() == "有")
                            {
                                TeamSafetyActivity.IsExitBad = true;
                            }
                            else if (row.GetCell(5).ToString() == "否" || row.GetCell(5).ToString() == "无")
                            {
                                TeamSafetyActivity.IsExitBad = false;
                            }

                            if (row.GetCell(6).ToString() == "是" || row.GetCell(6).ToString() == "有")
                            {
                                TeamSafetyActivity.IsFireEquipBad = true;
                            }
                            else if (row.GetCell(6).ToString() == "否" || row.GetCell(6).ToString() == "无")
                            {
                                TeamSafetyActivity.IsFireEquipBad = false;
                            }

                            if (row.GetCell(7).ToString() == "是" || row.GetCell(7).ToString() == "有")
                            {
                                TeamSafetyActivity.IsSafeMarkClean = true;
                            }
                            else if (row.GetCell(7).ToString() == "否" || row.GetCell(7).ToString() == "无")
                            {
                                TeamSafetyActivity.IsSafeMarkClean = false;
                            }


                            if (row.GetCell(8).ToString() == "是" || row.GetCell(8).ToString() == "有")
                            {
                                TeamSafetyActivity.IsSafeMarkFall = true;
                            }
                            else if (row.GetCell(8).ToString() == "否" || row.GetCell(8).ToString() == "无")
                            {
                                TeamSafetyActivity.IsSafeMarkFall = false;
                            }


                            if (row.GetCell(16).ToString() == "是" || row.GetCell(16).ToString() == "有")
                            {
                                TeamSafetyActivity.IsNotDanger = true;
                            }
                            else if (row.GetCell(16).ToString() == "否" || row.GetCell(16).ToString() == "无")
                            {
                                TeamSafetyActivity.IsNotDanger = false;
                            }


                            if (row.GetCell(17).ToString() == "是" || row.GetCell(17).ToString() == "有")
                            {
                                TeamSafetyActivity.IsOtherAdmittance = true;
                            }
                            else if (row.GetCell(17).ToString() == "否" || row.GetCell(17).ToString() == "无")
                            {
                                TeamSafetyActivity.IsOtherAdmittance = false;
                            }

                            if (row.GetCell(18).ToString() == "是" || row.GetCell(18).ToString() == "有")
                            {
                                TeamSafetyActivity.IsViolation = true;
                            }
                            else if (row.GetCell(18).ToString() == "否" || row.GetCell(18).ToString() == "无")
                            {
                                TeamSafetyActivity.IsViolation = false;
                            }

                            if (row.GetCell(19).ToString() == "是" || row.GetCell(19).ToString() == "有")
                            {
                                TeamSafetyActivity.IsElcOrGasShut = true;
                            }
                            else if (row.GetCell(19).ToString() == "否" || row.GetCell(19).ToString() == "无")
                            {
                                TeamSafetyActivity.IsElcOrGasShut = false;
                            }

                            if (row.GetCell(20).ToString() == "是" || row.GetCell(20).ToString() == "有")
                            {
                                TeamSafetyActivity.IsCloseWindow = true;
                            }
                            else if (row.GetCell(20).ToString() == "否" || row.GetCell(20).ToString() == "无")
                            {
                                TeamSafetyActivity.IsCloseWindow = false;
                            }
                            TeamSafetyActivity.SafeSupervision = row.GetCell(21).ToString();
                            TeamSafetyActivity.ResponsibleName = row.GetCell(22).ToString();
                            TeamSafetyActivity.EmployeeId = await _employeeRepository.GetAll().Where(aa => aa.Name == row.GetCell(23).ToString()).Select(v => v.Id).FirstOrDefaultAsync();

                            TeamSafetyActivity.EmployeeName = row.GetCell(23).ToString();
                            TeamSafetyActivity.CreationTime = Convert.ToDateTime(row.GetCell(24).ToString());
                            LC_TeamSafetyActivityList.Add(TeamSafetyActivity);
                        }
                    }
                }
                return await Task.FromResult(LC_TeamSafetyActivityList);
            }
        }

        /// <summary>
        /// 更新到数据库
        /// </summary>
        private async Task UpdateAsyncTeamSafetyActivityData(List<LC_TeamSafetyActivityEditDto> excelList)
        {
            foreach (var item in excelList)
            {
                var entity = new LC_TeamSafetyActivity();
                entity.AbnormalStopTime = item.AbnormalStopTime;
                entity.CreationTime = item.CreationTime;
                entity.EmployeeId = item.EmployeeId;
                entity.EmployeeName = item.EmployeeName;
                entity.BeginSortTime = item.BeginSortTime;
                entity.CommonCigaretNum = item.CommonCigaretNum;
                entity.EmpSafeAdvice = item.EmpSafeAdvice;
                entity.EndSortTime = item.EndSortTime;
                entity.IsAisleOk = item.IsAisleOk;
                entity.IsCloseWindow = item.IsCloseWindow;
                entity.IsElcOrGasShut = item.IsElcOrGasShut;
                entity.IsEmpHealth = item.IsEmpHealth;
                entity.IsExitBad = item.IsExitBad;
                entity.IsFireEquipBad = item.IsFireEquipBad;
                entity.IsNotDanger = item.IsNotDanger;
                entity.IsOtherAdmittance = item.IsOtherAdmittance;
                entity.IsSafeEquipOk = item.IsSafeEquipOk;
                entity.IsSafeMarkClean = item.IsSafeMarkClean;
                entity.ResponsibleName = item.ResponsibleName;
                entity.IsSafeMarkFall = item.IsSafeMarkFall;
                entity.IsTdjOrLsjOk = item.IsTdjOrLsjOk;
                entity.IsViolation = item.IsViolation;
                entity.NormalStopTime = item.NormalStopTime;
                entity.SafeSupervision = item.SafeSupervision;
                entity.SafetyMeeting = item.SafetyMeeting;
                entity.ShapedCigaretNum = item.ShapedCigaretNum;
                await _entityRepository.InsertAsync(entity);
                //}
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

    }
}
