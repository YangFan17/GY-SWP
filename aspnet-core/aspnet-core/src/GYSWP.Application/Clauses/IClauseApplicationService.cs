
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


using GYSWP.Clauses.Dtos;
using GYSWP.Clauses;
using GYSWP.Dtos;

namespace GYSWP.Clauses
{
    /// <summary>
    /// Clause应用层服务的接口方法
    ///</summary>
    public interface IClauseAppService : IApplicationService
    {
        /// <summary>
		/// 获取Clause的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ClauseListDto>> GetPaged(GetClausesInput input);


		/// <summary>
		/// 通过指定id获取ClauseListDto信息
		/// </summary>
		Task<ClauseListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetClauseForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Clause的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> CreateOrUpdate(CreateOrUpdateClauseInput input);


        /// <summary>
        /// 删除Clause信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Clause
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task<List<ClauseTreeNodeDto>> GetClauseTreeAsync(GetClausesInput input);
        Task<APIResultDto> ClauseRemoveById(EntityDto<Guid> id);
        Task<List<ClauseTreeNodeDto>> GetClauseTreeWithCheckedAsync(GetClausesInput input);
    }
}
