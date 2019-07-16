
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


using GYSWP.LC_TimeLogs.Dtos;
using GYSWP.LC_TimeLogs;
using GYSWP.Dtos;
using GYSWP.GYEnums;

namespace GYSWP.LC_TimeLogs
{
    /// <summary>
    /// LC_TimeLog应用层服务的接口方法
    ///</summary>
    public interface ILC_TimeLogAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_TimeLog的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_TimeLogListDto>> GetPaged(GetLC_TimeLogsInput input);


		/// <summary>
		/// 通过指定id获取LC_TimeLogListDto信息
		/// </summary>
		Task<LC_TimeLogListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_TimeLogForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_TimeLog的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LC_TimeLogEditDto> CreateOrUpdate(CreateOrUpdateLC_TimeLogInput input);


        /// <summary>
        /// 删除LC_TimeLog信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_TimeLog
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<APIResultDto> CreateBeginInStorageAsync(CreateLC_TimeLogsInput input);

        Task<APIResultDto> CreateScanOverAsync(CreateLC_TimeLogsInput input);

        /// <summary>
        /// 根据id修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<APIResultDto> ModifyStatusById(Guid id, LC_TimeStatus status);
    }
}
