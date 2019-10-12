
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


using GYSWP.EntryExitRegistrations.Dtos;
using GYSWP.EntryExitRegistrations;
using GYSWP.Dtos;

namespace GYSWP.EntryExitRegistrations
{
    /// <summary>
    /// EntryExitRegistration应用层服务的接口方法
    ///</summary>
    public interface IEntryExitRegistrationAppService : IApplicationService
    {
        /// <summary>
		/// 获取EntryExitRegistration的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<EntryExitRegistrationListDto>> GetPagedAsync(GetEntryExitRegistrationsInput input);


		/// <summary>
		/// 通过指定id获取EntryExitRegistrationListDto信息
		/// </summary>
		Task<EntryExitRegistrationListDto> GetByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetEntryExitRegistrationForEditOutput> GetForEditAsync(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改EntryExitRegistration的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateAsync(CreateOrUpdateEntryExitRegistrationInput input);


        /// <summary>
        /// 删除EntryExitRegistration信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAsync(EntityDto<long> input);


        /// <summary>
        /// 批量删除EntryExitRegistration
        /// </summary>
        Task BatchDeleteAsync(List<long> input);


        /// <summary>
        /// 导出EntryExitRegistration为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();
        Task<APIResultDto> ExportEntryExitRegistratione(GetEntryExitRegistrationsInput input);

        Task<APIResultDto> ImportEntryExitRegistrationExcelAsync();
    }
}
