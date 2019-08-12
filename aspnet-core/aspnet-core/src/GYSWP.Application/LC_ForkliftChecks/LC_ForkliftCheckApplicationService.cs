
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


using GYSWP.LC_ForkliftChecks;
using GYSWP.LC_ForkliftChecks.Dtos;
using GYSWP.LC_ForkliftChecks.DomainService;
using GYSWP.Dtos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GYSWP.LC_ForkliftChecks
{
    /// <summary>
    /// LC_ForkliftCheck应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_ForkliftCheckAppService : GYSWPAppServiceBase, ILC_ForkliftCheckAppService
    {
        private readonly IRepository<LC_ForkliftCheck, Guid> _entityRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_ForkliftCheckManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_ForkliftCheckAppService(
        IRepository<LC_ForkliftCheck, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_ForkliftCheckManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取LC_ForkliftCheck的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<LC_ForkliftCheckListDto>> GetPaged(GetLC_ForkliftChecksInput input)
		{
		    var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var count = await query.CountAsync();
			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();
			var entityListDtos =entityList.MapTo<List<LC_ForkliftCheckListDto>>();
			return new PagedResultDto<LC_ForkliftCheckListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_ForkliftCheckListDto信息
		/// </summary>
		 
		public async Task<LC_ForkliftCheckListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_ForkliftCheckListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_ForkliftCheck
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_ForkliftCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_ForkliftCheckForEditOutput();
LC_ForkliftCheckEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_ForkliftCheckEditDto>();

				//lC_ForkliftCheckEditDto = ObjectMapper.Map<List<lC_ForkliftCheckEditDto>>(entity);
			}
			else
			{
				editDto = new LC_ForkliftCheckEditDto();
			}

			output.LC_ForkliftCheck = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_ForkliftCheck的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_ForkliftCheckInput input)
		{

			if (input.LC_ForkliftCheck.Id.HasValue)
			{
				await Update(input.LC_ForkliftCheck);
			}
			else
			{
				await Create(input.LC_ForkliftCheck);
			}
		}


		/// <summary>
		/// 新增LC_ForkliftCheck
		/// </summary>
		
		protected virtual async Task<LC_ForkliftCheckEditDto> Create(LC_ForkliftCheckEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_ForkliftCheck>(input);
            var entity=input.MapTo<LC_ForkliftCheck>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_ForkliftCheckEditDto>();
		}

		/// <summary>
		/// 编辑LC_ForkliftCheck
		/// </summary>
		
		protected virtual async Task Update(LC_ForkliftCheckEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_ForkliftCheck信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_ForkliftCheck的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出LC_ForkliftCheck为excel表,等待开发。
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
        public async Task<APIResultDto> CreateForkliftCheckRecordAsync(LC_ForkliftCheckEditDto input)
        {
            var entity = input.MapTo<LC_ForkliftCheck>();

            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }

        /// <summary>
        /// 导出叉车运行记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportForkliftCheckRecord(GetLC_ForkliftChecksInput input)
        {
            try
            {
                var exportData = await GetForkliftCheckForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateForkliftCheckExcel("叉车运行记录.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportForkliftCheckRecord errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_ForkliftCheckListDto>> GetForkliftCheckForExcel(GetLC_ForkliftChecksInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_ForkliftCheckListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建叉车运行记录
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateForkliftCheckExcel(string fileName, List<LC_ForkliftCheckListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("ForkliftCheck");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "设备编号", "责任人", "监管人", "运行时间", "开机时间", "停机时间", "各部位润滑是否正常", "蓄电池接线有无腐蚀、松动", "转向、制动是否灵活", "车灯、喇叭是否正常", "电量是否充足", "货叉升降是否正常", "电量是否满足", "刹车、喇叭有无异常", "货叉升降有无异常", "运行声音有无异响", "停放是否规范到位", "制动是否拉紧、电源是否关闭", "是否需要补充电量", "各部位是否进行清洁", "故障描述和处理","创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.RunTime.HasValue == true ? (item.RunTime.Value.ToString("yyyy-MM-dd hh:mm:ss")) : "");
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.BeginTime.HasValue == true ? (item.BeginTime.Value.ToString("yyyy-MM-dd hh:mm:ss")) : "");
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.EndTime.HasValue == true ? (item.EndTime.Value.ToString("yyyy-MM-dd hh:mm:ss")) : "");
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.IslubricatingOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.IsBatteryBad == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.IsTurnOrBreakOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.IsLightOrHornOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.IsFullCharged == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.IsForkLifhOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(12), font, item.IsRunFullCharged == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(13), font, item.IsRunTurnOrBreakOk == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(14), font, item.IsRunLightOrHornOk == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(15), font, item.IsRunSoundOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(16), font, item.IsParkStandard == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(17), font, item.IsShutPower == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(18), font, item.IsNeedCharge == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(19), font, item.IsClean == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(20), font, item.Troubleshooting);
                    ExcelHelper.SetCell(row.CreateCell(21), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(22), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}


