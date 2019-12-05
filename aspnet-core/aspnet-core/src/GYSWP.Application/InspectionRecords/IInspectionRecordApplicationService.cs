
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


using GYSWP.InspectionRecords.Dtos;
using GYSWP.InspectionRecords;
using GYSWP.Dtos;

namespace GYSWP.InspectionRecords
{
    /// <summary>
    /// InspectionRecord应用层服务的接口方法
    ///</summary>
    public interface IInspectionRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取InspectionRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<InspectionRecordListDto>> GetPagedAsync(GetInspectionRecordsInput input);


		/// <summary>
		/// 通过指定id获取InspectionRecordListDto信息
		/// </summary>
		Task<InspectionRecordListDto> GetByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetInspectionRecordForEditOutput> GetForEditAsync(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改InspectionRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateAsync(CreateOrUpdateInspectionRecordInput input);


        /// <summary>
        /// 删除InspectionRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAsync(EntityDto<long> input);


        /// <summary>
        /// 批量删除InspectionRecord
        /// </summary>
        Task BatchDeleteAsync(List<long> input);


        /// <summary>
        /// 导出InspectionRecord为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();
        Task<APIResultDto> ImportInspectionRecordExcelAsync();

    }
}
