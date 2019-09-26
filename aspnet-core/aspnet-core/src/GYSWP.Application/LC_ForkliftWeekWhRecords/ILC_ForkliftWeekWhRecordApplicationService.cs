
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


using GYSWP.LC_ForkliftWeekWhRecords.Dtos;
using GYSWP.LC_ForkliftWeekWhRecords;

namespace GYSWP.LC_ForkliftWeekWhRecords
{
    /// <summary>
    /// LC_ForkliftWeekWhRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_ForkliftWeekWhRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_ForkliftWeekWhRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_ForkliftWeekWhRecordListDto>> GetPaged(GetLC_ForkliftWeekWhRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_ForkliftWeekWhRecordListDto信息
		/// </summary>
		Task<LC_ForkliftWeekWhRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_ForkliftWeekWhRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_ForkliftWeekWhRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        Task CreateOrUpdate(CreateOrUpdateLC_ForkliftWeekWhRecordInput input);


        /// <summary>
        /// 删除LC_ForkliftWeekWhRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_ForkliftWeekWhRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出LC_ForkliftWeekWhRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
