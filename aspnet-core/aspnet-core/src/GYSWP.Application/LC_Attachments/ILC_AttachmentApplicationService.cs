
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
    /// LC_Attachment应用层服务的接口方法
    ///</summary>
    public interface ILC_AttachmentAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_Attachment的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_AttachmentListDto>> GetPaged(GetLC_AttachmentsInput input);


		/// <summary>
		/// 通过指定id获取LC_AttachmentListDto信息
		/// </summary>
		Task<LC_AttachmentListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_AttachmentForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_Attachment的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_AttachmentInput input);


        /// <summary>
        /// 删除LC_Attachment信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_Attachment
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<APIResultDto> CreateAttachmentAsync(DingDingAttachmentEditDto input);
    }
}
