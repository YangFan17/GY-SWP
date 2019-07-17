
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


using GYSWP.PositionInfos.Dtos;
using GYSWP.PositionInfos;

namespace GYSWP.PositionInfos
{
    /// <summary>
    /// MainPointsRecord应用层服务的接口方法
    ///</summary>
    public interface IMainPointsRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取MainPointsRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<MainPointsRecordListDto>> GetPaged(GetMainPointsRecordsInput input);


		/// <summary>
		/// 通过指定id获取MainPointsRecordListDto信息
		/// </summary>
		Task<MainPointsRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetMainPointsRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改MainPointsRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateMainPointsRecordInput input);


        /// <summary>
        /// 删除MainPointsRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除MainPointsRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出MainPointsRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
