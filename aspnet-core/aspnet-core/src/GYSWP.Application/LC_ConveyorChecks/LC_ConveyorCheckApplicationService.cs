
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


using GYSWP.LC_ConveyorChecks;
using GYSWP.LC_ConveyorChecks.Dtos;
using GYSWP.LC_ConveyorChecks.DomainService;
using GYSWP.Dtos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using GYSWP.DocAttachments;
using GYSWP.Employees;
using GYSWP.Employees.Dtos;

namespace GYSWP.LC_ConveyorChecks
{
    /// <summary>
    /// LC_ConveyorCheck应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_ConveyorCheckAppService : GYSWPAppServiceBase, ILC_ConveyorCheckAppService
    {
        private readonly IRepository<LC_ConveyorCheck, Guid> _entityRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_ConveyorCheckManager _entityManager;

        private readonly IRepository<LC_Attachment, Guid> _attachmentRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_ConveyorCheckAppService(
        IRepository<LC_ConveyorCheck, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_ConveyorCheckManager entityManager,
        IRepository<LC_Attachment, Guid> attachmentRepository,
        IRepository<Employee, string> employeeRepository
        )
        {
            _employeeRepository = employeeRepository;
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _hostingEnvironment = hostingEnvironment;
            _attachmentRepository = attachmentRepository;
        }


        /// <summary>
        /// 获取LC_ConveyorCheck的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<LC_ConveyorCheckListDto>> GetPaged(GetLC_ConveyorChecksInput input)
        {

            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_ConveyorCheckListDto>>();
            return new PagedResultDto<LC_ConveyorCheckListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取LC_ConveyorCheckListDto信息
        /// </summary>

        public async Task<LC_ConveyorCheckListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<LC_ConveyorCheckListDto>();
        }

        /// <summary>
        /// 获取编辑 LC_ConveyorCheck
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetLC_ConveyorCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetLC_ConveyorCheckForEditOutput();
            LC_ConveyorCheckEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<LC_ConveyorCheckEditDto>();

                //lC_ConveyorCheckEditDto = ObjectMapper.Map<List<lC_ConveyorCheckEditDto>>(entity);
            }
            else
            {
                editDto = new LC_ConveyorCheckEditDto();
            }

            output.LC_ConveyorCheck = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改LC_ConveyorCheck的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateLC_ConveyorCheckInput input)
        {

            if (input.LC_ConveyorCheck.Id.HasValue)
            {
                await Update(input.LC_ConveyorCheck);
            }
            else
            {
                await Create(input.LC_ConveyorCheck);
            }
        }


        /// <summary>
        /// 新增LC_ConveyorCheck
        /// </summary>

        protected virtual async Task<LC_ConveyorCheckEditDto> Create(LC_ConveyorCheckEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_ConveyorCheck>(input);
            var entity = input.MapTo<LC_ConveyorCheck>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<LC_ConveyorCheckEditDto>();
        }

        /// <summary>
        /// 编辑LC_ConveyorCheck
        /// </summary>

        protected virtual async Task Update(LC_ConveyorCheckEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除LC_ConveyorCheck信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除LC_ConveyorCheck的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出LC_ConveyorCheck为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}


        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateConveyorCheckRecordAsync(LC_ConveyorCheckEditDto input)
        {
            var entity = input.MapTo<LC_ConveyorCheck>();

            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }


        /// <summary>
        /// 导出伸缩式输送机检查表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportConveyorChecksRecord(GetLC_ConveyorChecksInput input)
        {
            try
            {
                var exportData = await GetConveyorChecksForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateConveyorChecksExcel("伸缩式输送机运行记录.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportConveyorChecksRecord errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_ConveyorCheckListDto>> GetConveyorChecksForExcel(GetLC_ConveyorChecksInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_ConveyorCheckListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建入库记录表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateConveyorChecksExcel(string fileName, List<LC_ConveyorCheckListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("ConveyorCheck");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "设备编号", "责任人", "监管人", "运行时间", "开机时间", "停机时间", "设备表面有无异物", "固定支架是否完好", "设备螺栓是否紧固", "控制按钮是否正常", "电源线路是否老化、裸露", "皮带是否跑偏", "轴承运转是否正常", "运行声音有无异响", "电机运行是否正常", "电源是否断电", "传输皮带有无划伤、断裂", "设备是否进行清洁", "故障描述和处理", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.EquiNo);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.ResponsibleName);
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.SupervisorName);
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.RunTime.HasValue == true?( item.RunTime.Value.ToString("yyyy-MM-dd hh:mm:ss")):"");
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.BeginTime.HasValue == true ? (item.BeginTime.Value.ToString("yyyy-MM-dd hh:mm:ss")) : "");
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.EndTime.HasValue == true ? (item.EndTime.Value.ToString("yyyy-MM-dd hh:mm:ss")) : "");
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.IsEquiFaceClean == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.IsSteadyOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.IsScrewOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.IsButtonOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.IsElcLineBad == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.IsBeltSlant == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(12), font, item.IsBearingOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(13), font, item.IsSoundOk == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(14), font, item.IsMotor == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(15), font, item.IsShutPower == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(16), font, item.IsBeltBad == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(17), font, item.IsClean == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(18), font, item.Troubleshooting);
                    ExcelHelper.SetCell(row.CreateCell(19), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(20), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }

        /// <summary>
        /// 导入需求预测数据
        /// </summary>
        /// <returns></returns>
        public async Task<APIResultDto> ImportConveyorCheckExcelAsync()
        {
            //获取Excel数据
            var excelList = await GetConveyorCheckDataAsync();
            //循环批量更新
            await UpdateAsyncConveyorCheckData(excelList);
            return new APIResultDto() { Code = 0, Msg = "导入数据成功" };
        }
        /// <summary>
        /// 从上传的Excel读出数据
        /// </summary>
        private async Task<List<LC_ConveyorCheckEditDto>> GetConveyorCheckDataAsync()
        {
            string fileName = _hostingEnvironment.WebRootPath + "/files/upload/伸缩式输送机运行记录.xlsx";
            var LC_ConveyorCheckList = new List<LC_ConveyorCheckEditDto>();
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheet("ConveyorCheck");
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

                        var ConveyorCheck = new LC_ConveyorCheckEditDto();
                        if (row.GetCell(0) != null)
                        {
                            ConveyorCheck.EquiNo = row.GetCell(0).ToString();
                            ConveyorCheck.ResponsibleName = row.GetCell(1).ToString();
                            ConveyorCheck.SupervisorName = row.GetCell(2).ToString();
                            if (!string.IsNullOrEmpty(row.GetCell(3).ToString()))
                            {
                                ConveyorCheck.RunTime = Convert.ToDateTime(row.GetCell(2).ToString());
                            }
                            if (!string.IsNullOrEmpty(row.GetCell(4).ToString()))
                            {
                                ConveyorCheck.BeginTime = Convert.ToDateTime(row.GetCell(4).ToString());
                            }
                            if (!string.IsNullOrEmpty(row.GetCell(5).ToString()))
                            {
                                ConveyorCheck.EndTime = Convert.ToDateTime(row.GetCell(5).ToString());
                            }

                            if (row.GetCell(6).ToString() == "是" || row.GetCell(6).ToString() == "有")
                            {
                                ConveyorCheck.IsEquiFaceClean = true;
                            }
                            else if (row.GetCell(6).ToString() == "否" || row.GetCell(6).ToString() == "无")
                            {
                                ConveyorCheck.IsEquiFaceClean = false;
                            }

                            if (row.GetCell(7).ToString() == "是" || row.GetCell(7).ToString() == "有")
                            {
                                ConveyorCheck.IsSteadyOk = true;
                            }
                            else if (row.GetCell(7).ToString() == "否" || row.GetCell(7).ToString() == "无")
                            {
                                ConveyorCheck.IsSteadyOk = false;
                            }

                            if (row.GetCell(8).ToString() == "是" || row.GetCell(8).ToString() == "有")
                            {
                                ConveyorCheck.IsScrewOk = true;
                            }
                            else if (row.GetCell(8).ToString() == "否" || row.GetCell(8).ToString() == "无")
                            {
                                ConveyorCheck.IsScrewOk = false;
                            }

                            if (row.GetCell(9).ToString() == "是" || row.GetCell(9).ToString() == "有")
                            {
                                ConveyorCheck.IsButtonOk = true;
                            }
                            else if (row.GetCell(9).ToString() == "否" || row.GetCell(9).ToString() == "无")
                            {
                                ConveyorCheck.IsButtonOk = false;
                            }

                            if (row.GetCell(10).ToString() == "是" || row.GetCell(10).ToString() == "有")
                            {
                                ConveyorCheck.IsElcLineBad = true;
                            }
                            else if (row.GetCell(10).ToString() == "否" || row.GetCell(10).ToString() == "无")
                            {
                                ConveyorCheck.IsElcLineBad = false;
                            }

                            if (row.GetCell(11).ToString() == "是" || row.GetCell(11).ToString() == "有")
                            {
                                ConveyorCheck.IsBeltSlant = true;
                            }
                            else if (row.GetCell(11).ToString() == "否" || row.GetCell(11).ToString() == "无")
                            {
                                ConveyorCheck.IsBeltSlant = false;
                            }

                            if (row.GetCell(12).ToString() == "是" || row.GetCell(12).ToString() == "有")
                            {
                                ConveyorCheck.IsBearingOk = true;
                            }
                            else if (row.GetCell(12).ToString() == "否" || row.GetCell(12).ToString() == "无")
                            {
                                ConveyorCheck.IsBearingOk = false;
                            }


                            if (row.GetCell(13).ToString() == "是" || row.GetCell(13).ToString() == "有")
                            {
                                ConveyorCheck.IsSoundOk = true;
                            }
                            else if (row.GetCell(13).ToString() == "否" || row.GetCell(13).ToString() == "无")
                            {
                                ConveyorCheck.IsSoundOk = false;
                            }

                            if (row.GetCell(14).ToString() == "是" || row.GetCell(14).ToString() == "有")
                            {
                                ConveyorCheck.IsMotor = true;
                            }
                            else if (row.GetCell(14).ToString() == "否" || row.GetCell(14).ToString() == "无")
                            {
                                ConveyorCheck.IsMotor = false;
                            }

                            if (row.GetCell(15).ToString() == "是" || row.GetCell(15).ToString() == "有")
                            {
                                ConveyorCheck.IsShutPower = true;
                            }
                            else if (row.GetCell(15).ToString() == "否" || row.GetCell(15).ToString() == "无")
                            {
                                ConveyorCheck.IsShutPower = false;
                            }

                            if (row.GetCell(16).ToString() == "是" || row.GetCell(16).ToString() == "有")
                            {
                                ConveyorCheck.IsBeltBad = true;
                            }
                            else if (row.GetCell(16).ToString() == "否" || row.GetCell(16).ToString() == "无")
                            {
                                ConveyorCheck.IsBeltBad = false;
                            }

                            if (row.GetCell(17).ToString() == "是" || row.GetCell(17).ToString() == "有")
                            {
                                ConveyorCheck.IsClean = true;
                            }
                            else if (row.GetCell(17).ToString() == "否" || row.GetCell(17).ToString() == "无")
                            {
                                ConveyorCheck.IsClean = false;
                            }

                            if (!string.IsNullOrEmpty(row.GetCell(18).ToString()))
                            {
                                ConveyorCheck.Troubleshooting =row.GetCell(18).ToString();
                            }

                            ConveyorCheck.EmployeeId = await _employeeRepository.GetAll().Where(aa => aa.Name == row.GetCell(19).ToString()).Select(v => v.Id).FirstOrDefaultAsync();

                            ConveyorCheck.EmployeeName = row.GetCell(19).ToString();
                            ConveyorCheck.CreationTime=Convert.ToDateTime(row.GetCell(20).ToString());
                            LC_ConveyorCheckList.Add(ConveyorCheck);
                        }
                    }
                }
                return await Task.FromResult(LC_ConveyorCheckList);
            }
        }

        /// <summary>
        /// 更新到数据库
        /// </summary>
        private async Task UpdateAsyncConveyorCheckData(List<LC_ConveyorCheckEditDto> excelList)
        {
            foreach (var item in excelList)
            {
                var entity = new LC_ConveyorCheck();
                entity.BeginTime = item.BeginTime;
                entity.CreationTime = item.CreationTime;
                entity.EmployeeId = item.EmployeeId;
                entity.EmployeeName = item.EmployeeName;
                entity.EndTime = item.EndTime;
                entity.EquiNo = item.EquiNo;
                entity.IsBearingOk = item.IsBearingOk;
                entity.IsBeltBad = item.IsBeltBad;
                entity.IsBeltSlant = item.IsBeltSlant;
                entity.IsButtonOk = item.IsButtonOk;
                entity.IsClean = item.IsClean;
                entity.IsElcLineBad = item.IsElcLineBad;
                entity.IsEquiFaceClean = item.IsEquiFaceClean;
                entity.IsMotor = item.IsMotor;
                entity.IsScrewOk = item.IsScrewOk;
                entity.IsShutPower = item.IsShutPower;
                entity.IsSoundOk = item.IsSoundOk;
                entity.IsSteadyOk = item.IsSteadyOk;
                entity.ResponsibleName = item.ResponsibleName;
                entity.RunTime = item.RunTime;
                entity.SupervisorName = item.SupervisorName;
                entity.Troubleshooting = item.Troubleshooting;
                await _entityRepository.InsertAsync(entity);
                //}
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        /// <summary>
        /// 保养记录和照片拍照记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public virtual async Task RecordInsertOrUpdate(InsertLC_ConveyorCheckInput input)
        {
            if (input.LC_ConveyorCheck.Id.HasValue)//更新操作
            {
                var entity = input.LC_ConveyorCheck.MapTo<LC_ConveyorCheckEditDto>();
                await Update(entity);
                if (input.LC_ConveyorCheck.Path != null)
                {
                    var attachmentEntity = _attachmentRepository.GetAll();

                    foreach (var item in input.LC_ConveyorCheck.Path)
                    {
                        if (await attachmentEntity.CountAsync(aa => aa.Path == item) <= 0)
                        {
                            var AttachEntity = new LC_Attachment();
                            AttachEntity.Path = item;
                            AttachEntity.EmployeeId = input.LC_ConveyorCheck.EmployeeId;
                            AttachEntity.Type = input.LC_ConveyorCheck.Type;
                            AttachEntity.Remark = input.LC_ConveyorCheck.Remark;
                            AttachEntity.BLL = input.LC_ConveyorCheck.Id;
                            await _attachmentRepository.InsertAsync(AttachEntity);
                        }
                    }
                }
            }
            else//增加操作
            {
                var entity = input.LC_ConveyorCheck.MapTo<LC_ConveyorCheck>();
                var returnId = await _entityRepository.InsertAndGetIdAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();
                input.LC_ConveyorCheck.BLL = returnId;
                if (input.LC_ConveyorCheck.Path != null)
                {
                    foreach (var item in input.LC_ConveyorCheck.Path)
                    {
                        var AttachEntity = new LC_Attachment();
                        AttachEntity.Path = item;
                        AttachEntity.EmployeeId = input.LC_ConveyorCheck.EmployeeId;
                        AttachEntity.Type = input.LC_ConveyorCheck.Type;
                        AttachEntity.Remark = input.LC_ConveyorCheck.Remark;
                        AttachEntity.BLL = returnId;
                        await _attachmentRepository.InsertAsync(AttachEntity);
                    }
                }
            }
        }

        /// <summary>
        /// 钉钉通过指定条件获取LC_SortingEquipCheckListDto信息
        /// </summary>
        [AbpAllowAnonymous]
        public async Task<LC_ConveyorCheckDto> GetByDDWhereAsync(string employeeId)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(aa => aa.EmployeeId == employeeId && aa.CreationTime.ToString().Contains(DateTime.Now.ToShortDateString()));

            var item = entity.MapTo<LC_ConveyorCheckDto>();
            if (entity != null)
                item.Path = await _attachmentRepository.GetAll().Where(aa => aa.BLL == entity.Id).Select(aa => aa.Path).AsNoTracking().ToArrayAsync();
            if (item != null&& item.BeginTime.HasValue)
            { 
                item.StartTimeFormat = item.BeginTime.Value.ToString("yyyy-MM-dd HH:mm");
            }
            if (item != null && item.EndTime.HasValue)
            {
                item.EndTimeFormat = item.EndTime.Value.ToString("yyyy-MM-dd HH:mm");
            }
            return item;
        }
    }
}


