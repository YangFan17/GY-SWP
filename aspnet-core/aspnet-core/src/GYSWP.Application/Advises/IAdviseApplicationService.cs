
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


using GYSWP.Advises.Dtos;
using GYSWP.Advises;

namespace GYSWP.Advises
{
    /// <summary>
    /// Advise应用层服务的接口方法
    ///</summary>
    public interface IAdviseAppService : IApplicationService
    {
        /// <summary>
		/// 获取Advise的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AdviseListDto>> GetPagedAsync(GetAdvisesInput input);


		/// <summary>
		/// 通过指定id获取AdviseListDto信息
		/// </summary>
		Task<AdviseListDto> GetByIdAsync(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetAdviseForEditOutput> GetForEditAsync(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Advise的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateAsync(CreateOrUpdateAdviseInput input);


        /// <summary>
        /// 删除Advise信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAsync(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Advise
        /// </summary>
        Task BatchDeleteAsync(List<Guid> input);


        /// <summary>
        /// 合理化建议报表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetAdviseReportsDto>> GetAdviseReportsAsync(GetAdviseReportInputDto input);


        /// <summary>
        /// 导出Advise为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
