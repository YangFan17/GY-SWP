
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


using GYSWP.LC_MildewSummers;
using GYSWP.LC_MildewSummers.Dtos;
using GYSWP.LC_MildewSummers.DomainService;
using Abp.Auditing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using GYSWP.Dtos;

namespace GYSWP.LC_MildewSummers
{
    /// <summary>
    /// LC_MildewSummer应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_MildewSummerAppService : GYSWPAppServiceBase, ILC_MildewSummerAppService
    {
        private readonly IRepository<LC_MildewSummer, Guid> _entityRepository;

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_MildewSummerManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_MildewSummerAppService(
        IRepository<LC_MildewSummer, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_MildewSummerManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取LC_MildewSummer的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_MildewSummerListDto>> GetPaged(GetLC_MildewSummersInput input)
		{

		    var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_MildewSummerListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_MildewSummerListDto>>();

			return new PagedResultDto<LC_MildewSummerListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_MildewSummerListDto信息
		/// </summary>
		 
		public async Task<LC_MildewSummerListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_MildewSummerListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_MildewSummer
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_MildewSummerForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_MildewSummerForEditOutput();
LC_MildewSummerEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_MildewSummerEditDto>();

				//lC_MildewSummerEditDto = ObjectMapper.Map<List<lC_MildewSummerEditDto>>(entity);
			}
			else
			{
				editDto = new LC_MildewSummerEditDto();
			}

			output.LC_MildewSummer = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_MildewSummer的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdate(CreateOrUpdateLC_MildewSummerInput input)
		{

			if (input.LC_MildewSummer.Id.HasValue)
			{
				await Update(input.LC_MildewSummer);
			}
			else
			{
				await Create(input.LC_MildewSummer);
			}
		}


		/// <summary>
		/// 新增LC_MildewSummer
		/// </summary>
		
		protected virtual async Task<LC_MildewSummerEditDto> Create(LC_MildewSummerEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_MildewSummer>(input);
            var entity=input.MapTo<LC_MildewSummer>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_MildewSummerEditDto>();
		}

		/// <summary>
		/// 编辑LC_MildewSummer
		/// </summary>
		
		protected virtual async Task Update(LC_MildewSummerEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_MildewSummer信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_MildewSummer的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出LC_MildewSummer为excel表,等待开发。
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
        /// 导出卷烟仓库温湿度记录表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportLC_MildewSummere(GetLC_MildewSummersInput input)
        {
            try
            {
                var exportData = await GetLC_MildewSummereForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateLC_MildewSummereExcel("卷烟仓库温湿度记录表.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportLC_MildewSummere errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_MildewSummerListDto>> GetLC_MildewSummereForExcel(GetLC_MildewSummersInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_MildewSummerListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建卷烟仓库温湿度记录表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateLC_MildewSummereExcel(string fileName, List<LC_MildewSummerListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("LC_MildewSummere");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "上午开机时间", "上午开机前温度", "上午开机前湿度", "上午观察时间", "上午开机中温度", "上午开机中湿度", "上午停机时间", "上午停机后温度", "上午停机后湿度", "上午开机时间", "下午开机前温度", "下午开机前湿度", "下午观察时间", "下午开机中温度", "下午开机中湿度", "下午停机时间", "下午停机后温度", "下午停机后湿度", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.AMBootTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.AMBootBeforeTmp?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.AMBootBeforeHum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.AMObservedTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.AMBootingTmp?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.AMBootingHum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.AMBootAfterTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.AMBootAfterTmp?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.AMBootAfterHum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.PMBootingTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.PMBootBeforeTmp?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.PMBootBeforeHum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(12), font, item.PMObservedTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(13), font, item.PMBootingTmp?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(14), font, item.PMBootingHum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(15), font, item.PMBootAfterTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(16), font, item.PMBootAfterTmp?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(17), font, item.PMBootAfterHum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(18), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(19), font, item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}


