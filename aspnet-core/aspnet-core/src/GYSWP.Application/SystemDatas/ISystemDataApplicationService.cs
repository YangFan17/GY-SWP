
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


using GYSWP.SystemDatas.Dtos;
using GYSWP.SystemDatas;

namespace GYSWP.SystemDatas
{
    /// <summary>
    /// SystemData应用层服务的接口方法
    ///</summary>
    public interface ISystemDataAppService : IApplicationService
    {
        /// <summary>
		/// 获取SystemData的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<SystemDataListDto>> GetPagedAsync(GetSystemDatasInput input);


		/// <summary>
		/// 通过指定id获取SystemDataListDto信息
		/// </summary>
		Task<SystemDataListDto> GetByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetSystemDataForEditOutput> GetForEditAsync(NullableIdDto<int> input);


        /// <summary>
        /// 添加或者修改SystemData的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateAsync(CreateOrUpdateSystemDataInput input);


        /// <summary>
        /// 删除SystemData信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAsync(EntityDto<int> input);


        /// <summary>
        /// 批量删除SystemData
        /// </summary>
        Task BatchDeleteAsync(List<int> input);


		/// <summary>
        /// 导出SystemData为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
