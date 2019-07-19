
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


using GYSWP.DocAttachments.Dtos;
using GYSWP.DocAttachments;
using GYSWP.Dtos;

namespace GYSWP.DocAttachments
{
    /// <summary>
    /// DocAttachment应用层服务的接口方法
    ///</summary>
    public interface IDocAttachmentAppService : IApplicationService
    {
        /// <summary>
		/// 获取DocAttachment的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<DocAttachmentListDto>> GetPaged(GetDocAttachmentsInput input);


		/// <summary>
		/// 通过指定id获取DocAttachmentListDto信息
		/// </summary>
		Task<DocAttachmentListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDocAttachmentForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改DocAttachment的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> CreateOrUpdate(CreateOrUpdateDocAttachmentInput input);


        /// <summary>
        /// 删除DocAttachment信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除DocAttachment
        /// </summary>
        Task BatchDelete(List<Guid> input);

        /// <summary>
        /// 考核附件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<DocAttachmentDto>> GetAttachmentListByIdAsync(GetDocAttachmentsInput input);

        /// <summary>
        /// 条款附件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<DocAttachmentDto>> GetClauseAttachmentsByIdAsync(GetDocAttachmentsInput input);

        /// <summary>
        /// 标准附件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<DocAttachmentDto>> GetCriterionAttachmentByIdAsync(GetDocAttachmentsInput input);
    }
}
