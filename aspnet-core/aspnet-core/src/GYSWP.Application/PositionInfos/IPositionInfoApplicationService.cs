
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


using GYSWP.PositionInfos.Dtos;
using GYSWP.PositionInfos;
using GYSWP.Dtos;

namespace GYSWP.PositionInfos
{
    /// <summary>
    /// PositionInfo应用层服务的接口方法
    ///</summary>
    public interface IPositionInfoAppService : IApplicationService
    {
        /// <summary>
		/// 获取PositionInfo的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PositionInfoListDto>> GetPaged(GetPositionInfosInput input);


        /// <summary>
        /// 通过指定id获取PositionInfoListDto信息
        /// </summary>
        Task<PositionInfoListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPositionInfoForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改PositionInfo的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> CreateOrUpdate(CreateOrUpdatePositionInfoInput input);
        /// <summary>
        /// 批量删除PositionInfo
        /// </summary>
        Task BatchDelete(List<Guid> input);


        Task<List<PosInfoListOut>> GetPositionListByCurrentUserAsync();

        Task<APIResultDto> CreatePositionInfoAsync(PosInfoInput input);

        Task<List<HomeCategoryOption>> GetHomeCategoryOptionsAsync();

        Task<APIResultDto> PositionInfoImportAsync(string input);
        Task<List<HomePositionList>> GetPositionTreeByIdAsync();
        Task<string> GetCurrentPositionAsync();
        Task Delete(EntityDto<Guid> input);
        Task<List<HomePositionList>> GetDDPositionTreeByIdAsync(string empId);
    }
}
