
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


using GYSWP.EntryExitRegistrations;
using GYSWP.EntryExitRegistrations.Dtos;
using GYSWP.EntryExitRegistrations.DomainService;
using Abp.Auditing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using GYSWP.Dtos;
using GYSWP.Employees;

namespace GYSWP.EntryExitRegistrations
{
    /// <summary>
    /// EntryExitRegistration应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class EntryExitRegistrationAppService : GYSWPAppServiceBase, IEntryExitRegistrationAppService
    {
        private readonly IRepository<EntryExitRegistration, long> _entityRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEntryExitRegistrationManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public EntryExitRegistrationAppService(
        IRepository<EntryExitRegistration, long> entityRepository
        , IHostingEnvironment hostingEnvironment
        , IEntryExitRegistrationManager entityManager,
        IRepository<Employee, string> employeeRepository
        )
        {
            _employeeRepository = employeeRepository;
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取EntryExitRegistration的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<EntryExitRegistrationListDto>> GetPagedAsync(GetEntryExitRegistrationsInput input)
		{

		    var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<EntryExitRegistrationListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<EntryExitRegistrationListDto>>();

			return new PagedResultDto<EntryExitRegistrationListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取EntryExitRegistrationListDto信息
		/// </summary>
		 
		public async Task<EntryExitRegistrationListDto> GetByIdAsync(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<EntryExitRegistrationListDto>();
		}

		/// <summary>
		/// 获取编辑 EntryExitRegistration
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetEntryExitRegistrationForEditOutput> GetForEditAsync(NullableIdDto<long> input)
		{
			var output = new GetEntryExitRegistrationForEditOutput();
EntryExitRegistrationEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<EntryExitRegistrationEditDto>();

				//entryExitRegistrationEditDto = ObjectMapper.Map<List<entryExitRegistrationEditDto>>(entity);
			}
			else
			{
				editDto = new EntryExitRegistrationEditDto();
			}

			output.EntryExitRegistration = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改EntryExitRegistration的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdateAsync(CreateOrUpdateEntryExitRegistrationInput input)
		{

			if (input.EntryExitRegistration.Id.HasValue)
			{
				await Update(input.EntryExitRegistration);
			}
			else
			{
				await Create(input.EntryExitRegistration);
			}
		}


		/// <summary>
		/// 新增EntryExitRegistration
		/// </summary>
		
		protected virtual async Task<EntryExitRegistrationEditDto> Create(EntryExitRegistrationEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <EntryExitRegistration>(input);
            var entity=input.MapTo<EntryExitRegistration>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<EntryExitRegistrationEditDto>();
		}

		/// <summary>
		/// 编辑EntryExitRegistration
		/// </summary>
		
		protected virtual async Task Update(EntryExitRegistrationEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除EntryExitRegistration信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteAsync(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除EntryExitRegistration的方法
		/// </summary>
		
		public async Task BatchDeleteAsync(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出EntryExitRegistration为excel表,等待开发。
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
        /// 导出卷烟仓库人员出入登记表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportEntryExitRegistratione(GetEntryExitRegistrationsInput input)
        {
            try
            {
                var exportData = await GetEntryExitRegistrationeForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateEntryExitRegistrationeExcel("卷烟仓库人员出入登记表.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportEntryExitRegistratione errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<EntryExitRegistrationListDto>> GetEntryExitRegistrationeForExcel(GetEntryExitRegistrationsInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<EntryExitRegistrationListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建卷烟仓库人员出入登记表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateEntryExitRegistrationeExcel(string fileName, List<EntryExitRegistrationListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("EntryExitRegistratione");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "入库时间", "出库时间", "入库事由", "库内有无异常", "备注", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.EntryTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.ExitTime?.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.ReasonsForWarehousing);
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.IsAbnormal == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.Remarks);
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }


        /// <summary>
        /// 导入需求预测数据
        /// </summary>
        /// <returns></returns>
        public async Task<APIResultDto> ImportEntryExitRegistrationExcelAsync()
        {
            //获取Excel数据
            var excelList = await GetEntryExitRegistrationDataAsync();
            //循环批量更新
            await UpdateAsyncEntryExitRegistrationData(excelList);
            return new APIResultDto() { Code = 0, Msg = "导入数据成功" };
        }
        /// <summary>
        /// 从上传的Excel读出数据
        /// </summary>
        private async Task<List<EntryExitRegistrationEditDto>> GetEntryExitRegistrationDataAsync()
        {
            string fileName = _hostingEnvironment.WebRootPath + "/files/upload/卷烟仓库人员出入登记表.xlsx";
            var EntryExitRegistrationList = new List<EntryExitRegistrationEditDto>();
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheet("EntryExitRegistration");
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

                        var EntryExitRegistration = new EntryExitRegistrationEditDto();
                        if (row.GetCell(0) != null)
                        {
                            EntryExitRegistration.ReasonsForWarehousing = row.GetCell(2).ToString();
                            EntryExitRegistration.Remarks = row.GetCell(4).ToString();

                            if (!string.IsNullOrEmpty(row.GetCell(0).ToString()))
                            {
                                EntryExitRegistration.EntryTime = Convert.ToDateTime(row.GetCell(0).ToString());
                            }

                            if (!string.IsNullOrEmpty(row.GetCell(1).ToString()))
                            {
                                EntryExitRegistration.ExitTime = Convert.ToDateTime(row.GetCell(1).ToString());
                            }

                            if (row.GetCell(3).ToString() == "是" || row.GetCell(3).ToString() == "有")
                            {
                                EntryExitRegistration.IsAbnormal = true;
                            }
                            else if (row.GetCell(3).ToString() == "否" || row.GetCell(3).ToString() == "无")
                            {
                                EntryExitRegistration.IsAbnormal = false;
                            }
  

                            EntryExitRegistration.EmployeeId = await _employeeRepository.GetAll().Where(aa => aa.Name == row.GetCell(5).ToString()).Select(v => v.Id).FirstOrDefaultAsync();

                            EntryExitRegistration.EmployeeName = row.GetCell(5).ToString();
                            EntryExitRegistration.CreationTime = Convert.ToDateTime(row.GetCell(6).ToString());
                            EntryExitRegistrationList.Add(EntryExitRegistration);
                        }
                    }
                }
                return await Task.FromResult(EntryExitRegistrationList);
            }
        }

        /// <summary>
        /// 更新到数据库
        /// </summary>
        private async Task UpdateAsyncEntryExitRegistrationData(List<EntryExitRegistrationEditDto> excelList)
        {
            foreach (var item in excelList)
            {
                var entity = new EntryExitRegistration();
                entity.CreationTime = item.CreationTime;
                entity.EmployeeId = item.EmployeeId;
                entity.EmployeeName = item.EmployeeName;
                entity.ExitTime = item.ExitTime;
                entity.EntryTime = item.EntryTime;
                entity.IsAbnormal = item.IsAbnormal;
                entity.ReasonsForWarehousing = item.ReasonsForWarehousing;
                entity.Remarks = item.Remarks;
                await _entityRepository.InsertAsync(entity);
                //}
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}


