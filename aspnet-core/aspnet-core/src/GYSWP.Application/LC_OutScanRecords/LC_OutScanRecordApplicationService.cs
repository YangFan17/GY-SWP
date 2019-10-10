
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


using GYSWP.LC_OutScanRecords;
using GYSWP.LC_OutScanRecords.Dtos;
using GYSWP.LC_OutScanRecords.DomainService;
using GYSWP.LC_TimeLogs;
using GYSWP.LC_ScanRecords;
using GYSWP.Dtos;
using GYSWP.Helpers;
using Microsoft.AspNetCore.Hosting;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

namespace GYSWP.LC_OutScanRecords
{
    /// <summary>
    /// LC_OutScanRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_OutScanRecordAppService : GYSWPAppServiceBase, ILC_OutScanRecordAppService
    {
        private readonly IRepository<LC_OutScanRecord, Guid> _entityRepository;
        private readonly IRepository<LC_TimeLog, Guid> _timeLogRepository;
        private readonly IRepository<LC_ScanRecord, Guid> _scanRecordRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly ILC_OutScanRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_OutScanRecordAppService(
        IRepository<LC_OutScanRecord, Guid> entityRepository
            , IRepository<LC_TimeLog, Guid> timeLogRepository
            , IRepository<LC_ScanRecord, Guid> scanRecordRepository
            , IHostingEnvironment hostingEnvironment
        , ILC_OutScanRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _timeLogRepository = timeLogRepository;
            _scanRecordRepository = scanRecordRepository;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取LC_OutScanRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_OutScanRecordListDto>> GetPaged(GetLC_OutScanRecordsInput input)
		{

		    var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

			var entityList = await query
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_OutScanRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_OutScanRecordListDto>>();

			return new PagedResultDto<LC_OutScanRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_OutScanRecordListDto信息
		/// </summary>
		 
		public async Task<LC_OutScanRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_OutScanRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_OutScanRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_OutScanRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_OutScanRecordForEditOutput();
LC_OutScanRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_OutScanRecordEditDto>();

				//lC_OutScanRecordEditDto = ObjectMapper.Map<List<lC_OutScanRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_OutScanRecordEditDto();
			}

			output.LC_OutScanRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_OutScanRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAllowAnonymous]
		public async Task<Guid> CreateOrUpdate(CreateOrUpdateLC_OutScanRecordInput input)
		{

			if (input.LC_OutScanRecord.Id.HasValue)
			{
				await Update(input.LC_OutScanRecord);
			}
			else
			{
                //if (string.IsNullOrWhiteSpace(input.LC_OutScanRecord.TimeLogId.ToString()))
                //{
                //    await Create(input.LC_OutScanRecord);
                //    //LC_TimeLog timeLog = new LC_TimeLog();
                //    //timeLog.EmployeeId = input.LC_OutScanRecord.EmployeeId;
                //    //timeLog.EmployeeName = input.LC_OutScanRecord.EmployeeName;
                //    //timeLog.Status = GYEnums.LC_TimeStatus.开始;
                //    //timeLog.Type = GYEnums.LC_TimeType.零货出库;
                //    //var timeLogId = await _timeLogRepository.InsertAndGetIdAsync(timeLog);
                //    //await CurrentUnitOfWork.SaveChangesAsync();

                //    //保存结束后出库扫码结束
                //    //LC_ScanRecord scanRecord = new LC_ScanRecord();
                //    //scanRecord.TimeLogId = timeLog.Id;
                //    //scanRecord.EmployeeId = input.LC_OutScanRecord.EmployeeId;
                //    //scanRecord.EmployeeName = input.LC_OutScanRecord.EmployeeName;
                //    //scanRecord.Status = GYEnums.LC_TimeStatus.开始;
                //    //scanRecord.Type = GYEnums.LC_TimeType.零货出库;
                //    //await _scanRecordRepository.InsertAsync(scanRecord);
                //    //return timeLogId;
                //}
                //else
                //{
                //    LC_TimeLog timeLog = new LC_TimeLog();
                //    timeLog.EmployeeId = input.LC_OutScanRecord.EmployeeId;
                //    timeLog.EmployeeName = input.LC_OutScanRecord.EmployeeName;
                //    timeLog.Status = GYEnums.LC_TimeStatus.结束;
                //    timeLog.Type = GYEnums.LC_TimeType.零货出库;
                //    var timeLogId = await _timeLogRepository.InsertAndGetIdAsync(timeLog);
                //}

            }
            return Guid.Empty;
		}


		/// <summary>
		/// 新增LC_OutScanRecord
		/// </summary>
		[AbpAllowAnonymous]
		protected virtual async Task<LC_OutScanRecordEditDto> Create(LC_OutScanRecordEditDto input)
		{
            //TODO:新增前的逻辑判断，是否允许新增


            // var entity = ObjectMapper.Map <LC_OutScanRecord>(input);
            var entity=input.MapTo<LC_OutScanRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_OutScanRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_OutScanRecord
		/// </summary>
		
		protected virtual async Task Update(LC_OutScanRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_OutScanRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_OutScanRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        /// <summary>
        /// 保存出库扫码记录表及扫码结束信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateOutSacnRecordAsync(CreateOrUpdateLC_OutScanRecordInput input)
        {
            var entity = input.LC_OutScanRecord.MapTo<LC_OutScanRecord>();
            entity = await _entityRepository.InsertAsync(entity);
            LC_ScanRecord scanRecord = new LC_ScanRecord();
            //scanRecord.TimeLogId = input.LC_OutScanRecord.TimeLogId;
            scanRecord.Status = GYEnums.LC_TimeStatus.结束;
            scanRecord.Type = GYEnums.LC_ScanRecordType.出库扫码;
            scanRecord.EmployeeId = input.LC_OutScanRecord.EmployeeId;
            scanRecord.EmployeeName = input.LC_OutScanRecord.EmployeeName;
            await _scanRecordRepository.InsertAsync(scanRecord);
            return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
        }



        /// <summary>
        /// 导出卷烟出库扫码记录表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportOutScanRecord(GetLC_OutScanRecordsInput input)
        {
            try
            {
                var exportData = await GetOutScanRecordsForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateOutScanRecordsExcel("卷烟出库扫码记录表.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportOutScanRecord errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_OutScanRecordListDto>> GetOutScanRecordsForExcel(GetLC_OutScanRecordsInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_OutScanRecordListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 卷烟出库扫码记录表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateOutScanRecordsExcel(string fileName, List<LC_OutScanRecordListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("OutStorageScanRecord");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "出库订单数", "应扫码数", "实际扫码数", "零条未扫码数", "备注", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.OrderNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.ExpectedScanNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.AcutalScanNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.AloneNotScanNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.Remark);
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}


