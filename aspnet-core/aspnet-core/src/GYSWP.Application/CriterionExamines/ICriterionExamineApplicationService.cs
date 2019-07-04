
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


using GYSWP.CriterionExamines.Dtos;
using GYSWP.CriterionExamines;
using GYSWP.Dtos;

namespace GYSWP.CriterionExamines
{
    /// <summary>
    /// CriterionExamine应用层服务的接口方法
    ///</summary>
    public interface ICriterionExamineAppService : IApplicationService
    {
        /// <summary>
		/// 获取CriterionExamine的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CriterionExamineListDto>> GetPaged(GetCriterionExaminesInput input);


		/// <summary>
		/// 通过指定id获取CriterionExamineListDto信息
		/// </summary>
		Task<CriterionExamineListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCriterionExamineForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改CriterionExamine的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateCriterionExamineInput input);


        /// <summary>
        /// 删除CriterionExamine信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除CriterionExamine
        /// </summary>
        Task BatchDelete(List<Guid> input);
        Task<APIResultDto> CreateInternalExamineAsync(CriterionExamineInfoDto input);
        Task<PagedResultDto<CriterionExamineListDto>> GetPagedExamineByCurrentIdAsync(GetCriterionExaminesInput input);
        Task<APIResultDto> CreateExamineByQiGuanAsync(CriterionExamineInfoDto input);
    }
}
