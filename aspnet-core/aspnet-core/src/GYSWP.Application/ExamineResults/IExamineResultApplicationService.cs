
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


using GYSWP.ExamineResults.Dtos;
using GYSWP.ExamineResults;

namespace GYSWP.ExamineResults
{
    /// <summary>
    /// ExamineResult应用层服务的接口方法
    ///</summary>
    public interface IExamineResultAppService : IApplicationService
    {
        /// <summary>
		/// 获取ExamineResult的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ExamineResultListDto>> GetPaged(GetExamineResultsInput input);


		/// <summary>
		/// 通过指定id获取ExamineResultListDto信息
		/// </summary>
		Task<ExamineResultListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetExamineResultForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改ExamineResult的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateExamineResultInput input);


        /// <summary>
        /// 删除ExamineResult信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除ExamineResult
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出ExamineResult为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
