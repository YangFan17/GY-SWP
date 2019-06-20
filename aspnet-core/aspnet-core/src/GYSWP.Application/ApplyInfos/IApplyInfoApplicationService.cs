
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


using GYSWP.ApplyInfos.Dtos;
using GYSWP.ApplyInfos;
using GYSWP.Dtos;

namespace GYSWP.ApplyInfos
{
    /// <summary>
    /// ApplyInfo应用层服务的接口方法
    ///</summary>
    public interface IApplyInfoAppService : IApplicationService
    {
        /// <summary>
		/// 获取ApplyInfo的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ApplyInfoListDto>> GetPaged(GetApplyInfosInput input);


		/// <summary>
		/// 通过指定id获取ApplyInfoListDto信息
		/// </summary>
		Task<ApplyInfoListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetApplyInfoForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改ApplyInfo的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateApplyInfoInput input);


        /// <summary>
        /// 删除ApplyInfo信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除ApplyInfo
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task<APIResultDto> ApplyDocAsync(ApplyInfoEditDto input);
        Task UpdateApplyInfoByPIIdAsync(string pIId, string result);
    }
}
