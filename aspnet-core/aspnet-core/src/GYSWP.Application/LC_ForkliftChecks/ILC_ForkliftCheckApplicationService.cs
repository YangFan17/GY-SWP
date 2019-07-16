
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


using GYSWP.LC_ForkliftChecks.Dtos;
using GYSWP.LC_ForkliftChecks;
using GYSWP.Dtos;

namespace GYSWP.LC_ForkliftChecks
{
    /// <summary>
    /// LC_ForkliftCheck应用层服务的接口方法
    ///</summary>
    public interface ILC_ForkliftCheckAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_ForkliftCheck的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_ForkliftCheckListDto>> GetPaged(GetLC_ForkliftChecksInput input);


		/// <summary>
		/// 通过指定id获取LC_ForkliftCheckListDto信息
		/// </summary>
		Task<LC_ForkliftCheckListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_ForkliftCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_ForkliftCheck的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_ForkliftCheckInput input);


        /// <summary>
        /// 删除LC_ForkliftCheck信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_ForkliftCheck
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 导出LC_ForkliftCheck为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

        Task<APIResultDto> CreateForkliftCheckRecordAsync(LC_ForkliftCheckEditDto input);
    }
}
