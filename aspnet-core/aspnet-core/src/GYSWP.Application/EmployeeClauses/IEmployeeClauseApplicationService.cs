
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


using GYSWP.EmployeeClauses.Dtos;
using GYSWP.EmployeeClauses;
using GYSWP.Dtos;

namespace GYSWP.EmployeeClauses
{
    /// <summary>
    /// EmployeeClause应用层服务的接口方法
    ///</summary>
    public interface IEmployeeClauseAppService : IApplicationService
    {
        /// <summary>
		/// 获取EmployeeClause的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<EmployeeClauseListDto>> GetPaged(GetEmployeeClausesInput input);


		/// <summary>
		/// 通过指定id获取EmployeeClauseListDto信息
		/// </summary>
		Task<EmployeeClauseListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetEmployeeClauseForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改EmployeeClause的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateEmployeeClauseInput input);


        /// <summary>
        /// 删除EmployeeClause信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除EmployeeClause
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task<APIResultDto> ConfirmClauseAsync(ConfirmClauseInput input);
        Task<APIResultDto> GetUserOperateAsync(ConfirmClauseInput input);
        Task<APIResultDto> GetUserOperateDraftAsync();
        Task<APIResultDto> GetDraftOperateDraftAsync();
    }
}
