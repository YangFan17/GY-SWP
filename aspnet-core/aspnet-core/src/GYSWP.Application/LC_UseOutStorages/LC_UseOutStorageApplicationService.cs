
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


using GYSWP.LC_UseOutStorages;
using GYSWP.LC_UseOutStorages.Dtos;
using GYSWP.LC_UseOutStorages.DomainService;
using Abp.Auditing;
using GYSWP.Dtos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GYSWP.LC_UseOutStorages
{
    /// <summary>
    /// LC_UseOutStorage应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_UseOutStorageAppService : GYSWPAppServiceBase, ILC_UseOutStorageAppService
    {
        private readonly IRepository<LC_UseOutStorage, Guid> _entityRepository;

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_UseOutStorageManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_UseOutStorageAppService(
        IRepository<LC_UseOutStorage, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_UseOutStorageManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取LC_UseOutStorage的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_UseOutStorageListDto>> GetPaged(GetLC_UseOutStoragesInput input)
		{

		    var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

			var entityList = await query
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_UseOutStorageListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_UseOutStorageListDto>>();

			return new PagedResultDto<LC_UseOutStorageListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_UseOutStorageListDto信息
		/// </summary>
		 
		public async Task<LC_UseOutStorageListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_UseOutStorageListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_UseOutStorage
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_UseOutStorageForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_UseOutStorageForEditOutput();
LC_UseOutStorageEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_UseOutStorageEditDto>();

				//lC_UseOutStorageEditDto = ObjectMapper.Map<List<lC_UseOutStorageEditDto>>(entity);
			}
			else
			{
				editDto = new LC_UseOutStorageEditDto();
			}

			output.LC_UseOutStorage = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改LC_UseOutStorage的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdate(CreateOrUpdateLC_UseOutStorageInput input)
		{

			if (input.LC_UseOutStorage.Id.HasValue)
			{
				await Update(input.LC_UseOutStorage);
			}
			else
			{
				await Create(input.LC_UseOutStorage);
			}
		}


		/// <summary>
		/// 新增LC_UseOutStorage
		/// </summary>
		
		protected virtual async Task<LC_UseOutStorageEditDto> Create(LC_UseOutStorageEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_UseOutStorage>(input);
            var entity=input.MapTo<LC_UseOutStorage>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_UseOutStorageEditDto>();
		}

		/// <summary>
		/// 编辑LC_UseOutStorage
		/// </summary>
		
		protected virtual async Task Update(LC_UseOutStorageEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_UseOutStorage信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_UseOutStorage的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出LC_UseOutStorage为excel表,等待开发。
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
        /// 导出卷烟分拣领用出库单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportUseOutStorage(GetLC_UseOutStoragesInput input)
        {
            try
            {
                var exportData = await GetUseOutStorageForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateUseOutStorageExcel("卷烟分拣领用出库单.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportQualityRecord errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_UseOutStorageListDto>> GetUseOutStorageForExcel(GetLC_UseOutStoragesInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_UseOutStorageListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建卷烟分拣领用出库表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateUseOutStorageExcel(string fileName, List<LC_UseOutStorageListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("UseOutStorage");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "分拣线名称", "商品名称", "差异(预出库数量)", "零条(预出库数量)", "整盘(补库量)", "总件(补库量)", "总条(补库量)", "实际订单量", "盘点(库存盘点)", "零条(库存盘点)", "保管员", "分拣员", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.SortLineName);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.ProductName);
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.PreDiffNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.PreAloneNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.SupWholeNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.SupAllPieceNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.SupAllItemNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.AcutalOrderNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.CheckNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.CheckAloneNum?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.ClerkName);
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.SortorName);
                    ExcelHelper.SetCell(row.CreateCell(12), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(13), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}


