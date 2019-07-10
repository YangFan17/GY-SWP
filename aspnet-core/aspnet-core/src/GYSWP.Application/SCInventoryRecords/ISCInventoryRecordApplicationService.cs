
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


using GYSWP.SCInventoryRecords.Dtos;
using GYSWP.SCInventoryRecords;

namespace GYSWP.SCInventoryRecords
{
    /// <summary>
    /// SCInventoryRecord应用层服务的接口方法
    ///</summary>
    public interface ISCInventoryRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取SCInventoryRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<SCInventoryRecordListDto>> GetPagedAsync(GetSCInventoryRecordsInput input);


		/// <summary>
		/// 通过指定id获取SCInventoryRecordListDto信息
		/// </summary>
		Task<SCInventoryRecordListDto> GetByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetSCInventoryRecordForEditOutput> GetForEditAsync(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改SCInventoryRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateAsync(CreateOrUpdateSCInventoryRecordInput input);


        /// <summary>
        /// 删除SCInventoryRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAsync(EntityDto<long> input);


        /// <summary>
        /// 批量删除SCInventoryRecord
        /// </summary>
        Task BatchDeleteAsync(List<long> input);


		/// <summary>
        /// 导出SCInventoryRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
