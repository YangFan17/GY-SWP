
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


using GYSWP.LC_QualityRecords;
using GYSWP.LC_QualityRecords.Dtos;
using GYSWP.LC_QualityRecords.DomainService;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using GYSWP.Dtos;

namespace GYSWP.LC_QualityRecords
{
    /// <summary>
    /// LC_QualityRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_QualityRecordAppService : GYSWPAppServiceBase, ILC_QualityRecordAppService
    {
        private readonly IRepository<LC_QualityRecord, Guid> _entityRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_QualityRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_QualityRecordAppService(
        IRepository<LC_QualityRecord, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_QualityRecordManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取LC_QualityRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<LC_QualityRecordListDto>> GetPaged(GetLC_QualityRecordsInput input)
        {

            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_QualityRecordListDto>>();
            return new PagedResultDto<LC_QualityRecordListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取LC_QualityRecordListDto信息
        /// </summary>

        public async Task<LC_QualityRecordListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<LC_QualityRecordListDto>();
        }

        /// <summary>
        /// 获取编辑 LC_QualityRecord
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetLC_QualityRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetLC_QualityRecordForEditOutput();
            LC_QualityRecordEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<LC_QualityRecordEditDto>();

                //lC_QualityRecordEditDto = ObjectMapper.Map<List<lC_QualityRecordEditDto>>(entity);
            }
            else
            {
                editDto = new LC_QualityRecordEditDto();
            }

            output.LC_QualityRecord = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改LC_QualityRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task CreateOrUpdate(CreateOrUpdateLC_QualityRecordInput input)
        {

            if (input.LC_QualityRecord.Id.HasValue)
            {
                await Update(input.LC_QualityRecord);
            }
            else
            {
                await Create(input.LC_QualityRecord);
            }
        }


        /// <summary>
        /// 新增LC_QualityRecord
        /// </summary>

        protected virtual async Task<LC_QualityRecordEditDto> Create(LC_QualityRecordEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_QualityRecord>(input);
            var entity = input.MapTo<LC_QualityRecord>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<LC_QualityRecordEditDto>();
        }

        /// <summary>
        /// 编辑LC_QualityRecord
        /// </summary>

        protected virtual async Task Update(LC_QualityRecordEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除LC_QualityRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除LC_QualityRecord的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 导出质量问题登记表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportQualityRecord(GetLC_QualityRecordsInput input)
        {
            try
            {
                var exportData = await GetQualityRecordForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateQualityRecordExcel("卷烟入库验收质量问题登记表.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportQualityRecord errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_QualityRecordListDto>> GetQualityRecordForExcel(GetLC_QualityRecordsInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_QualityRecordListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建质量问题登记表
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateQualityRecordExcel(string fileName, List<LC_QualityRecordListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("QualityRecord");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "卷烟名称", "批发价格", "出售数量", "车牌号码", "赔偿金额", "承运人", "保管员", "交接日期", "金额", "经手人", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.WholesaleAmount.ToString());
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.SaleQuantity.ToString());
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.CarNo);
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.CompensationAmount.ToString());
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.CarrierName);
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.ClerkName);
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.HandoverTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.Amount.ToString());
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.HandManName);
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}