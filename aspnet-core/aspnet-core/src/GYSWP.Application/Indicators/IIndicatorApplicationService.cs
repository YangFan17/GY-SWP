
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


using GYSWP.Indicators.Dtos;
using GYSWP.Indicators;

namespace GYSWP.Indicators
{
    /// <summary>
    /// Indicator应用层服务的接口方法
    ///</summary>
    public interface IIndicatorAppService : IApplicationService
    {
        /// <summary>
		/// 获取Indicator的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<IndicatorListDto>> GetPaged(GetIndicatorsInput input);


		/// <summary>
		/// 通过指定id获取IndicatorListDto信息
		/// </summary>
		Task<IndicatorListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetIndicatorForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Indicator的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateIndicatorInput input);


        /// <summary>
        /// 删除Indicator信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Indicator
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出Indicator为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
