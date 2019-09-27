
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


using GYSWP.LC_ForkliftMonthWhRecords.Dtos;
using GYSWP.LC_ForkliftMonthWhRecords;

namespace GYSWP.LC_ForkliftMonthWhRecords
{
    /// <summary>
    /// LC_ForkliftMonthWhRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_ForkliftMonthWhRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_ForkliftMonthWhRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_ForkliftMonthWhRecordListDto>> GetPaged(GetLC_ForkliftMonthWhRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_ForkliftMonthWhRecordListDto信息
		/// </summary>
		Task<LC_ForkliftMonthWhRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_ForkliftMonthWhRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_ForkliftMonthWhRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        Task CreateOrUpdate(CreateOrUpdateLC_ForkliftMonthWhRecordInput input);


        /// <summary>
        /// 删除LC_ForkliftMonthWhRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_ForkliftMonthWhRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 导出LC_ForkliftMonthWhRecord为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();
        /// <summary>
        /// 保养记录和照片拍照记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        Task RecordInsert(InsertLC_ForkliftMonthWhRecordInput input);

    }
}
