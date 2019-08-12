
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


using GYSWP.LC_InStorageRecords;
using GYSWP.LC_InStorageRecords.Dtos;
using GYSWP.LC_InStorageRecords.DomainService;
using GYSWP.Dtos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GYSWP.LC_InStorageRecords
{
    /// <summary>
    /// LC_InStorageRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_InStorageRecordAppService : GYSWPAppServiceBase, ILC_InStorageRecordAppService
    {
        private readonly IRepository<LC_InStorageRecord, Guid> _entityRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_InStorageRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_InStorageRecordAppService(
        IRepository<LC_InStorageRecord, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_InStorageRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取LC_InStorageRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<LC_InStorageRecordListDto>> GetPaged(GetLC_InStorageRecordsInput input)
		{
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

			var entityList = await query
					.OrderByDescending(v=>v.CreationTime).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_InStorageRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_InStorageRecordListDto>>();

			return new PagedResultDto<LC_InStorageRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_InStorageRecordListDto信息
		/// </summary>
		 
		public async Task<LC_InStorageRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_InStorageRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_InStorageRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_InStorageRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_InStorageRecordForEditOutput();
LC_InStorageRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_InStorageRecordEditDto>();

				//lC_InStorageRecordEditDto = ObjectMapper.Map<List<lC_InStorageRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_InStorageRecordEditDto();
			}

			output.LC_InStorageRecord = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改LC_InStorageRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task CreateOrUpdate(CreateOrUpdateLC_InStorageRecordInput input)
		{

			if (input.LC_InStorageRecord.Id.HasValue)
			{
				await Update(input.LC_InStorageRecord);
			}
			else
			{
				await Create(input.LC_InStorageRecord);
			}
		}


		/// <summary>
		/// 新增LC_InStorageRecord
		/// </summary>
		
		protected virtual async Task<LC_InStorageRecordEditDto> Create(LC_InStorageRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_InStorageRecord>(input);
            var entity=input.MapTo<LC_InStorageRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_InStorageRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_InStorageRecord
		/// </summary>
		
		protected virtual async Task Update(LC_InStorageRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_InStorageRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_InStorageRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出入库记录明细表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportInStorageRecord(GetLC_InStorageRecordsInput input)
        {
            try
            {
                var exportData = await GetInStorageRecordForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateInStorageRecordExcel("卷烟入库记录.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportInStorageRecord errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_InStorageRecordListDto>> GetInStorageRecordForExcel(GetLC_InStorageRecordsInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_InStorageRecordListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建入库记录表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateInStorageRecordExcel(string fileName, List<LC_InStorageRecordListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("InStorageRecord");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "品名规格", "车号", "发货单位", "单据号", "应收数量", "实收数量", "差损情况", "质量", "收货人", "备注", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.Name);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.CarNo );
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.DeliveryUnit);
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.BillNo);
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.ReceivableAmount.ToString());
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.ActualAmount.ToString());
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.DiffContent);
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.Quality);
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.ReceiverName);
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.Remark);
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}


