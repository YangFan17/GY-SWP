
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


using GYSWP.DocRevisions.Dtos;
using GYSWP.DocRevisions;
using GYSWP.Dtos;

namespace GYSWP.DocRevisions
{
    /// <summary>
    /// DocRevision应用层服务的接口方法
    ///</summary>
    public interface IDocRevisionAppService : IApplicationService
    {
        /// <summary>
		/// 获取DocRevision的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<DocRevisionListDto>> GetPaged(GetDocRevisionsInput input);


		/// <summary>
		/// 通过指定id获取DocRevisionListDto信息
		/// </summary>
		Task<DocRevisionListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDocRevisionForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改DocRevision的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> CreateOrUpdate(CreateOrUpdateDocRevisionInput input);


        /// <summary>
        /// 删除DocRevision信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除DocRevision
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出DocRevision为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
