
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


using GYSWP.LC_InStorageRecords.Dtos;
using GYSWP.LC_InStorageRecords;
using GYSWP.Dtos;

namespace GYSWP.LC_InStorageRecords
{
    /// <summary>
    /// LC_InStorageRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_InStorageRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_InStorageRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_InStorageRecordListDto>> GetPaged(GetLC_InStorageRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_InStorageRecordListDto信息
		/// </summary>
		Task<LC_InStorageRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_InStorageRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_InStorageRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_InStorageRecordInput input);


        /// <summary>
        /// 删除LC_InStorageRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_InStorageRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<APIResultDto> ExportInStorageRecord(GetLC_InStorageRecordsInput input);

        Task<APIResultDto> ImportInStorageRecordExcelAsync();
    }
}
