
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


using GYSWP.LC_SortingEquipChecks.Dtos;
using GYSWP.LC_SortingEquipChecks;

namespace GYSWP.LC_SortingEquipChecks
{
    /// <summary>
    /// LC_SortingEquipCheck应用层服务的接口方法
    ///</summary>
    public interface ILC_SortingEquipCheckAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_SortingEquipCheck的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_SortingEquipCheckListDto>> GetPaged(GetLC_SortingEquipChecksInput input);


		/// <summary>
		/// 通过指定id获取LC_SortingEquipCheckListDto信息
		/// </summary>
		Task<LC_SortingEquipCheckListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_SortingEquipCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_SortingEquipCheck的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_SortingEquipCheckInput input);


        /// <summary>
        /// 删除LC_SortingEquipCheck信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_SortingEquipCheck
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出LC_SortingEquipCheck为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
