
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


using GYSWP.LC_SortingEquipChecks;
using GYSWP.LC_SortingEquipChecks.Dtos;
using GYSWP.LC_SortingEquipChecks.DomainService;
using Abp.Auditing;
using GYSWP.Dtos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GYSWP.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GYSWP.LC_SortingEquipChecks
{
    /// <summary>
    /// LC_SortingEquipCheck应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_SortingEquipCheckAppService : GYSWPAppServiceBase, ILC_SortingEquipCheckAppService
    {
        private readonly IRepository<LC_SortingEquipCheck, Guid> _entityRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILC_SortingEquipCheckManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_SortingEquipCheckAppService(
        IRepository<LC_SortingEquipCheck, Guid> entityRepository
        , IHostingEnvironment hostingEnvironment
        , ILC_SortingEquipCheckManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// 获取LC_SortingEquipCheck的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<LC_SortingEquipCheckListDto>> GetPaged(GetLC_SortingEquipChecksInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_SortingEquipCheckListDto>>();
            return new PagedResultDto<LC_SortingEquipCheckListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取LC_SortingEquipCheckListDto信息
        /// </summary>

        public async Task<LC_SortingEquipCheckListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<LC_SortingEquipCheckListDto>();
        }

        /// <summary>
        /// 获取编辑 LC_SortingEquipCheck
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetLC_SortingEquipCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetLC_SortingEquipCheckForEditOutput();
            LC_SortingEquipCheckEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<LC_SortingEquipCheckEditDto>();

                //lC_SortingEquipCheckEditDto = ObjectMapper.Map<List<lC_SortingEquipCheckEditDto>>(entity);
            }
            else
            {
                editDto = new LC_SortingEquipCheckEditDto();
            }

            output.LC_SortingEquipCheck = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改LC_SortingEquipCheck的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdate(CreateOrUpdateLC_SortingEquipCheckInput input)
        {

            if (input.LC_SortingEquipCheck.Id.HasValue)
            {
                await Update(input.LC_SortingEquipCheck);
            }
            else
            {
                await Create(input.LC_SortingEquipCheck);
            }
        }


        /// <summary>
        /// 新增LC_SortingEquipCheck
        /// </summary>

        protected virtual async Task<LC_SortingEquipCheckEditDto> Create(LC_SortingEquipCheckEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_SortingEquipCheck>(input);
            var entity = input.MapTo<LC_SortingEquipCheck>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<LC_SortingEquipCheckEditDto>();
        }

        /// <summary>
        /// 编辑LC_SortingEquipCheck
        /// </summary>

        protected virtual async Task Update(LC_SortingEquipCheckEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除LC_SortingEquipCheck信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除LC_SortingEquipCheck的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出分拣设备运行记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ExportSortingEquipCheck(GetLC_SortingEquipChecksInput input)
        {
            try
            {
                var exportData = await GetSortingEquipCheckForExcel(input);
                var result = new APIResultDto();
                result.Code = 0;
                result.Data = CreateSortingEquipCheckExcel("分拣设备运行记录.xlsx", exportData);
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExportSortingEquipCheck errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "网络忙...请待会儿再试！" };

            }
        }

        private async Task<List<LC_SortingEquipCheckListDto>> GetSortingEquipCheckForExcel(GetLC_SortingEquipChecksInput input)
        {
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            var entityList = await query
                   .OrderByDescending(v => v.CreationTime).AsNoTracking()
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<LC_SortingEquipCheckListDto>>();
            return entityListDtos;
        }

        /// <summary>
        /// 创建分拣设备运行记录
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="data">表数据</param>
        /// <returns></returns>
        private string CreateSortingEquipCheckExcel(string fileName, List<LC_SortingEquipCheckListDto> data)
        {
            var fullPath = ExcelHelper.GetSavePath(_hostingEnvironment.WebRootPath) + fileName;
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("SortingEquipCheck");
                var rowIndex = 0;
                IRow titleRow = sheet.CreateRow(rowIndex);
                string[] titles = { "责任人", "监管人", "（分拣线）链板、推头是否正常", "（分拣线）控制开关是否灵敏有效", "（分拣线）电、气线路有无裸露、脱落", "（分拣线）检查所有的升降舌头处于上升状态", "（分拣线）分拣系统是否正常启动", "（分拣线）传动皮带、链板上有无异物", "（包装机）进料口、封切处有无异物", "（包装机）控制开关是否灵敏有效", "（包装机）电、气线路有无裸露、脱落", "（包装机）温控仪工作是否正常", "（包装机）系统是否正常启动", "（包装机）收缩炉、封切刀工作是否正常", "（自动贴标机）贴标系统能否正常工作", "（自动贴标机）电、气线路有无裸露、脱落", "（激光打码机）激光防护罩是否完好", "（激光打码机）检查电源线、激光机是否正常", "分拣设备烟仓下烟是否正常", "分拣设备合单机构工作是否正常", "主电源线是否发热", "打码机光学感应器有无偏移、有无残码现象", "包装机叠烟翻板工作是否正常", "分拣设备皮带是否跑偏，机械运转有无异响", "塑封包装是否完好", "贴标机工作是否正常", "故障描述和处理", "软件系统是否正常退出", "电、气是否关闭", "打码数据是否回传", "设备是否进行清洁保养", "创建人", "创建时间" };
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
                    ExcelHelper.SetCell(row.CreateCell(0), font, item.ResponsibleName);
                    ExcelHelper.SetCell(row.CreateCell(1), font, item.SupervisorName);
                    ExcelHelper.SetCell(row.CreateCell(2), font, item.IsChainPlateOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(3), font, item.IsControlSwitchOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(4), font, item.IsElcOrGasBad == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(5), font, item.IsLiftUp == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(6), font, item.IsSortSysOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(7), font, item.IsChanDirty == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(8), font, item.IsCutSealDirty == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(9), font, item.IsBZJControlSwitchOk == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(10), font, item.IsBZJElcOrGasBad == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(11), font, item.IsTempOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(12), font, item.IsBZJSysOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(13), font, item.IsStoveOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(14), font, item.IsLabelingOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(15), font, item.IsTBJElcOrGasBad == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(16), font, item.IsLaserShieldOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(17), font, item.IsLineOrMachineOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(18), font, item.IsCigaretteHouseOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(19), font, item.IsSingleOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(20), font, item.IsMainLineOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(21), font, item.IsCoderOk == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(22), font, item.IsBZJWorkOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(23), font, item.IsBeltDeviation == true ? "有" : "无");
                    ExcelHelper.SetCell(row.CreateCell(24), font, item.IsFBJOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(25), font, item.IsTBJOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(26), font, item.Troubleshooting);
                    ExcelHelper.SetCell(row.CreateCell(27), font, item.IsSysOutOk == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(28), font, item.IsShutElcOrGas == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(29), font, item.IsDataCallback == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(30), font, item.IsMachineClean == true ? "是" : "否");
                    ExcelHelper.SetCell(row.CreateCell(31), font, item.EmployeeName);
                    ExcelHelper.SetCell(row.CreateCell(32), font, item.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                workbook.Write(fs);
            }
            return "/files/downloadtemp/" + fileName;
        }
    }
}
