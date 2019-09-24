
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


using GYSWP.IndicatorLibrarys.Dtos;
using GYSWP.IndicatorLibrarys;

namespace GYSWP.IndicatorLibrarys
{
    /// <summary>
    /// IndicatorLibrary应用层服务的接口方法
    ///</summary>
    public interface IIndicatorLibraryAppService : IApplicationService
    {
        /// <summary>
		/// 获取IndicatorLibrary的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<IndicatorLibraryListDto>> GetPaged(GetIndicatorLibrarysInput input);


		/// <summary>
		/// 通过指定id获取IndicatorLibraryListDto信息
		/// </summary>
		Task<IndicatorLibraryListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetIndicatorLibraryForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改IndicatorLibrary的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateIndicatorLibraryInput input);


        /// <summary>
        /// 删除IndicatorLibrary信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除IndicatorLibrary
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出IndicatorLibrary为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
