
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


using GYSWP.Documents.Dtos;
using GYSWP.Documents;
using GYSWP.Employees.Dtos;
using GYSWP.Dtos;

namespace GYSWP.Documents
{
    /// <summary>
    /// Document应用层服务的接口方法
    ///</summary>
    public interface IDocumentAppService : IApplicationService
    {
        /// <summary>
		/// 获取Document的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<DocumentListDto>> GetPaged(GetDocumentsInput input);


		/// <summary>
		/// 通过指定id获取DocumentListDto信息
		/// </summary>
		Task<DocumentListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDocumentForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改Document的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> CreateOrUpdate(CreateOrUpdateDocumentInput input);


        /// <summary>
        /// 删除Document信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除Document
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task<List<DocNzTreeNode>> GetDeptDocNzTreeNodesAsync(string rootName);
        Task<PagedResultDto<DocumentListDto>> GetPagedWithPermission(GetDocumentsInput input);
        Task<DocumentTitleDto> GetDocumentTitleAsync(Guid id);

        Task<List<DocumentListDto>> GetDocumentListByDDUserIdAsync(EntityDto<string> input);

        Task<bool> GetHasDocPermissionFromScanAsync(Guid id, string userId);

        Task<DocumentListDto> GetDocInfoByScanAsync(Guid id, string userId);

        /// <summary>
        /// 钉钉上修改Document的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> DingUpdateAsync(DingDocumentEditDto input);
        Task<APIResultDto> DocumentReadAsync(DocumentReadInput input);
        Task<string> GetDocNameById(EntityDto<Guid> input);
    }
}
