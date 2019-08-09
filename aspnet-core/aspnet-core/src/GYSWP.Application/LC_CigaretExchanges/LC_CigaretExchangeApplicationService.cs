
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


using GYSWP.LC_CigaretExchanges;
using GYSWP.LC_CigaretExchanges.Dtos;
using GYSWP.LC_CigaretExchanges.DomainService;
using Abp.Auditing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using GYSWP.Dtos;

namespace GYSWP.LC_CigaretExchanges
{
    /// <summary>
    /// LC_CigaretExchange应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_CigaretExchangeAppService : GYSWPAppServiceBase, ILC_CigaretExchangeAppService
    {
        private readonly IRepository<LC_CigaretExchange, Guid> _entityRepository;

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_CigaretExchangeManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_CigaretExchangeAppService(
        IRepository<LC_CigaretExchange, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_CigaretExchangeManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取LC_CigaretExchange的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_CigaretExchangeListDto>> GetPaged(GetLC_CigaretExchangesInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_CigaretExchangeListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_CigaretExchangeListDto>>();

			return new PagedResultDto<LC_CigaretExchangeListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_CigaretExchangeListDto信息
		/// </summary>
		 
		public async Task<LC_CigaretExchangeListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_CigaretExchangeListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_CigaretExchange
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_CigaretExchangeForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_CigaretExchangeForEditOutput();
LC_CigaretExchangeEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_CigaretExchangeEditDto>();

				//lC_CigaretExchangeEditDto = ObjectMapper.Map<List<lC_CigaretExchangeEditDto>>(entity);
			}
			else
			{
				editDto = new LC_CigaretExchangeEditDto();
			}

			output.LC_CigaretExchange = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改LC_CigaretExchange的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdate(CreateOrUpdateLC_CigaretExchangeInput input)
		{

			if (input.LC_CigaretExchange.Id.HasValue)
			{
				await Update(input.LC_CigaretExchange);
			}
			else
			{
				await Create(input.LC_CigaretExchange);
			}
		}


		/// <summary>
		/// 新增LC_CigaretExchange
		/// </summary>
		
		protected virtual async Task<LC_CigaretExchangeEditDto> Create(LC_CigaretExchangeEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_CigaretExchange>(input);
            var entity=input.MapTo<LC_CigaretExchange>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_CigaretExchangeEditDto>();
		}

		/// <summary>
		/// 编辑LC_CigaretExchange
		/// </summary>
		
		protected virtual async Task Update(LC_CigaretExchangeEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_CigaretExchange信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_CigaretExchange的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出LC_CigaretExchange为excel表,等待开发。
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
        /// 导出残损卷烟调换表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportCigaretExchange(GetLC_CigaretExchangesInput input)
        {
            try
            {
                var exportData = await GetCigaretExchangeForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateCigaretExchangeExcel("残损卷烟调换表.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportCigaretExchange errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_CigaretExchangeListDto>> GetCigaretExchangeForExcel(GetLC_CigaretExchangesInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_CigaretExchangeListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建残损卷烟调换表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateCigaretExchangeExcel(string fileName, List<LC_CigaretExchangeListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("CigaretExchange");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "产地", "品名", "单位", "数量", "机损原因", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.OriginPlace);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.Name);
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.Unit);
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.Num?.ToString());
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.Reason);
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}


