
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


using GYSWP.LC_ScanRecords.Dtos;
using GYSWP.LC_ScanRecords;
using GYSWP.Dtos;

namespace GYSWP.LC_ScanRecords
{
    /// <summary>
    /// LC_ScanRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_ScanRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_ScanRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_ScanRecordListDto>> GetPaged(GetLC_ScanRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_ScanRecordListDto信息
		/// </summary>
		Task<LC_ScanRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_ScanRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_ScanRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Guid> CreateOrUpdate(CreateOrUpdateLC_ScanRecordInput input);


        /// <summary>
        /// 删除LC_ScanRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_ScanRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<APIResultDto> CreateOutStorageSacnAsync(LC_ScanRecordEditDto input);


        /// <summary>
        /// 入库扫码开始
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        Task<APIResultDto> CreateInStorageScanAsync(CreateLC_ScanRecordInput input);
    }
}
