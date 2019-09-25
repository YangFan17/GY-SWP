
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


using GYSWP.LC_SsjMonthWhByRecords.Dtos;
using GYSWP.LC_SsjMonthWhByRecords;

namespace GYSWP.LC_SsjMonthWhByRecords
{
    /// <summary>
    /// LC_SsjMonthWhByRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_SsjMonthWhByRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_SsjMonthWhByRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_SsjMonthWhByRecordListDto>> GetPaged(GetLC_SsjMonthWhByRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_SsjMonthWhByRecordListDto信息
		/// </summary>
		Task<LC_SsjMonthWhByRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_SsjMonthWhByRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_SsjMonthWhByRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        Task CreateOrUpdate(CreateOrUpdateLC_SsjMonthWhByRecordInput input);


        /// <summary>
        /// 删除LC_SsjMonthWhByRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_SsjMonthWhByRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出LC_SsjMonthWhByRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
