
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


using GYSWP.LC_QualityRecords.Dtos;
using GYSWP.LC_QualityRecords;
using GYSWP.Dtos;

namespace GYSWP.LC_QualityRecords
{
    /// <summary>
    /// LC_QualityRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_QualityRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_QualityRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_QualityRecordListDto>> GetPaged(GetLC_QualityRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_QualityRecordListDto信息
		/// </summary>
		Task<LC_QualityRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_QualityRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_QualityRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_QualityRecordInput input);


        /// <summary>
        /// 删除LC_QualityRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_QualityRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<APIResultDto> ExportQualityRecord(GetLC_QualityRecordsInput input);
    }
}
