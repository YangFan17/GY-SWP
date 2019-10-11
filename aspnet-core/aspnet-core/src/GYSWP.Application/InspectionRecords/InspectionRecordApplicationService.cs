
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


using GYSWP.InspectionRecords;
using GYSWP.InspectionRecords.Dtos;
using GYSWP.InspectionRecords.DomainService;
using Abp.Auditing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using GYSWP.Dtos;

namespace GYSWP.InspectionRecords
{
    /// <summary>
    /// InspectionRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class InspectionRecordAppService : GYSWPAppServiceBase, IInspectionRecordAppService
    {
        private readonly IRepository<InspectionRecord, long> _entityRepository;

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IInspectionRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public InspectionRecordAppService(
        IRepository<InspectionRecord, long> entityRepository
        , IHostingEnvironment hostingEnvironment
        , IInspectionRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取InspectionRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<InspectionRecordListDto>> GetPagedAsync(GetInspectionRecordsInput input)
		{

		    var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

			var entityList = await query
					.OrderByDescending(v=>v.CreationTime).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<InspectionRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<InspectionRecordListDto>>();

			return new PagedResultDto<InspectionRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取InspectionRecordListDto信息
		/// </summary>
		 
		public async Task<InspectionRecordListDto> GetByIdAsync(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<InspectionRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 InspectionRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetInspectionRecordForEditOutput> GetForEditAsync(NullableIdDto<long> input)
		{
			var output = new GetInspectionRecordForEditOutput();
InspectionRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<InspectionRecordEditDto>();

				//inspectionRecordEditDto = ObjectMapper.Map<List<inspectionRecordEditDto>>(entity);
			}
			else
			{
				editDto = new InspectionRecordEditDto();
			}

			output.InspectionRecord = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改InspectionRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdateAsync(CreateOrUpdateInspectionRecordInput input)
		{

			if (input.InspectionRecord.Id.HasValue)
			{
				await Update(input.InspectionRecord);
			}
			else
			{
				await Create(input.InspectionRecord);
			}
		}


		/// <summary>
		/// 新增InspectionRecord
		/// </summary>
		
		protected virtual async Task<InspectionRecordEditDto> Create(InspectionRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <InspectionRecord>(input);
            var entity=input.MapTo<InspectionRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<InspectionRecordEditDto>();
		}

		/// <summary>
		/// 编辑InspectionRecord
		/// </summary>
		
		protected virtual async Task Update(InspectionRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除InspectionRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteAsync(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除InspectionRecord的方法
		/// </summary>
		
		public async Task BatchDeleteAsync(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出InspectionRecord为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}
        /// <summary>
        /// 导出巡查记录表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportInspectionRecord(GetInspectionRecordsInput input)
        {
            try
            {
                var exportData = await GetInspectionRecordeForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateInspectionRecordeExcel("巡查记录表.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportInspectionRecorde errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<InspectionRecordListDto>> GetInspectionRecordeForExcel(GetInspectionRecordsInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<InspectionRecordListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建巡查记录表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateInspectionRecordeExcel(string fileName, List<InspectionRecordListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("InspectionRecorde");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "门窗、锁有无异常", "墙体有无破坏", "屋顶、墙面是否渗水", "温湿度是否超标", "消防控制系统工作是否正常", "防盗报警器工作是否正常", "防盗报警设密是否灵敏有效", "监控摄像头有无遮挡", "消防设施有无阻拦", "安全出口有无阻拦", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.IsDWLAbnormal == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.IsWallDestruction == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.IsRoofWallSeepage == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.IsHumitureExceeding == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.IsFASNormal == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.IsBurglarAlarmNormal == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.IsSASSValid == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.IsCameraShelter == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.IsFPDStop == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.IsEXITStop == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}


