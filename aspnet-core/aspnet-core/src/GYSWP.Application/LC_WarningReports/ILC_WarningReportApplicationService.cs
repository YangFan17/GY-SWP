
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


using GYSWP.LC_WarningReports.Dtos;
using GYSWP.LC_WarningReports;

namespace GYSWP.LC_WarningReports
{
    /// <summary>
    /// LC_WarningReport应用层服务的接口方法
    ///</summary>
    public interface ILC_WarningReportAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_WarningReport的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_WarningReportListDto>> GetPaged(GetLC_WarningReportsInput input);


		/// <summary>
		/// 通过指定id获取LC_WarningReportListDto信息
		/// </summary>
		Task<LC_WarningReportListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_WarningReportForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_WarningReport的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_WarningReportInput input);


        /// <summary>
        /// 删除LC_WarningReport信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_WarningReport
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出LC_WarningReport为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}