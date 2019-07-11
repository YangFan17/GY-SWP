
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


using GYSWP.LC_UseOutStorages.Dtos;
using GYSWP.LC_UseOutStorages;

namespace GYSWP.LC_UseOutStorages
{
    /// <summary>
    /// LC_UseOutStorage应用层服务的接口方法
    ///</summary>
    public interface ILC_UseOutStorageAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_UseOutStorage的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_UseOutStorageListDto>> GetPaged(GetLC_UseOutStoragesInput input);


		/// <summary>
		/// 通过指定id获取LC_UseOutStorageListDto信息
		/// </summary>
		Task<LC_UseOutStorageListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_UseOutStorageForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_UseOutStorage的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_UseOutStorageInput input);


        /// <summary>
        /// 删除LC_UseOutStorage信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_UseOutStorage
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出LC_UseOutStorage为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
