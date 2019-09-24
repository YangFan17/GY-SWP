
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


using GYSWP.LC_KyjMonthMaintainRecords.Dtos;
using GYSWP.LC_KyjMonthMaintainRecords;

namespace GYSWP.LC_KyjMonthMaintainRecords
{
    /// <summary>
    /// LC_KyjMonthMaintainRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_KyjMonthMaintainRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_KyjMonthMaintainRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_KyjMonthMaintainRecordListDto>> GetPaged(GetLC_KyjMonthMaintainRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_KyjMonthMaintainRecordListDto信息
		/// </summary>
		Task<LC_KyjMonthMaintainRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_KyjMonthMaintainRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_KyjMonthMaintainRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_KyjMonthMaintainRecordInput input);


        /// <summary>
        /// 删除LC_KyjMonthMaintainRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_KyjMonthMaintainRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出LC_KyjMonthMaintainRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
