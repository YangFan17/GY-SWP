
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


using GYSWP.LC_OutScanRecords.Dtos;
using GYSWP.LC_OutScanRecords;
using GYSWP.Dtos;

namespace GYSWP.LC_OutScanRecords
{
    /// <summary>
    /// LC_OutScanRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_OutScanRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_OutScanRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_OutScanRecordListDto>> GetPaged(GetLC_OutScanRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_OutScanRecordListDto信息
		/// </summary>
		Task<LC_OutScanRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_OutScanRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_OutScanRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Guid> CreateOrUpdate(CreateOrUpdateLC_OutScanRecordInput input);


        /// <summary>
        /// 删除LC_OutScanRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_OutScanRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<APIResultDto> CreateOutSacnRecordAsync(CreateOrUpdateLC_OutScanRecordInput input);
    }
}
