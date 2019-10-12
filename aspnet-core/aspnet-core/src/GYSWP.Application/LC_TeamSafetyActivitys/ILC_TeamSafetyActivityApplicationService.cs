
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


using GYSWP.LC_TeamSafetyActivitys.Dtos;
using GYSWP.LC_TeamSafetyActivitys;
using GYSWP.Dtos;

namespace GYSWP.LC_TeamSafetyActivitys
{
    /// <summary>
    /// LC_TeamSafetyActivity应用层服务的接口方法
    ///</summary>
    public interface ILC_TeamSafetyActivityAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_TeamSafetyActivity的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_TeamSafetyActivityListDto>> GetPaged(GetLC_TeamSafetyActivitysInput input);


		/// <summary>
		/// 通过指定id获取LC_TeamSafetyActivityListDto信息
		/// </summary>
		Task<LC_TeamSafetyActivityListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_TeamSafetyActivityForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_TeamSafetyActivity的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_TeamSafetyActivityInput input);


        /// <summary>
        /// 删除LC_TeamSafetyActivity信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_TeamSafetyActivity
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<APIResultDto> ExportTeamSafetyActivity(GetLC_TeamSafetyActivitysInput input);

        Task<APIResultDto> ImportTeamSafetyActivityExcelAsync();
    }
}
