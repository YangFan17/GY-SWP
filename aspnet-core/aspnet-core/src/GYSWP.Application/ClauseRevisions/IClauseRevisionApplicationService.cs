
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


using GYSWP.ClauseRevisions.Dtos;
using GYSWP.ClauseRevisions;
using GYSWP.Clauses.Dtos;
using GYSWP.Dtos;

namespace GYSWP.ClauseRevisions
{
    /// <summary>
    /// ClauseRevision应用层服务的接口方法
    ///</summary>
    public interface IClauseRevisionAppService : IApplicationService
    {
        /// <summary>
		/// 获取ClauseRevision的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ClauseRevisionListDto>> GetPaged(GetClauseRevisionsInput input);


		/// <summary>
		/// 通过指定id获取ClauseRevisionListDto信息
		/// </summary>
		Task<ClauseRevisionListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetClauseRevisionForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改ClauseRevision的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> CreateOrUpdate(CreateOrUpdateClauseRevisionInput input);


        /// <summary>
        /// 删除ClauseRevision信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除ClauseRevision
        /// </summary>
        Task BatchDelete(List<Guid> input);


        Task<ClauseRecordResult> GetClauseRevisionListByIdAsync(GetClauseRevisionsInput input);
        Task<APIResultDto> CreateRevisionAsync(ApplyInput input);
        Task<APIResultDto> ClauseRevisionRemoveById(ApplyDeleteInput input);
        Task<APIResultDto> ClauseRevisionDeleteById(EntityDto<Guid> id);
    }
}
