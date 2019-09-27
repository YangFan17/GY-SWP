
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
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using GYSWP.LC_ConveyorChecks.Dtos;
using GYSWP.LC_ConveyorChecks;
using GYSWP.Dtos;

namespace GYSWP.LC_ConveyorChecks
{
    /// <summary>
    /// LC_ConveyorCheck应用层服务的接口方法
    ///</summary>
    public interface ILC_ConveyorCheckAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_ConveyorCheck的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_ConveyorCheckListDto>> GetPaged(GetLC_ConveyorChecksInput input);


		/// <summary>
		/// 通过指定id获取LC_ConveyorCheckListDto信息
		/// </summary>
		Task<LC_ConveyorCheckListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_ConveyorCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_ConveyorCheck的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_ConveyorCheckInput input);


        /// <summary>
        /// 删除LC_ConveyorCheck信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_ConveyorCheck
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 导出LC_ConveyorCheck为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

        Task<APIResultDto> CreateConveyorCheckRecordAsync(LC_ConveyorCheckEditDto input);
        Task<APIResultDto> ExportConveyorChecksRecord(GetLC_ConveyorChecksInput input);

        /// <summary>
        /// 钉钉通过指定条件获取LC_SortingEquipCheckListDto信息
        /// </summary>
        [AbpAllowAnonymous]
        Task<LC_ConveyorCheckDto> GetByDDWhereAsync(string employeeId);

        /// <summary>
        /// 保养记录和照片拍照记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        Task RecordInsertOrUpdate(InsertLC_ConveyorCheckInput input);
    }
}
