
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


using GYSWP.ExamineFeedbacks.Dtos;
using GYSWP.ExamineFeedbacks;

namespace GYSWP.ExamineFeedbacks
{
    /// <summary>
    /// ExamineFeedback应用层服务的接口方法
    ///</summary>
    public interface IExamineFeedbackAppService : IApplicationService
    {
        /// <summary>
		/// 获取ExamineFeedback的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ExamineFeedbackListDto>> GetPaged(GetExamineFeedbacksInput input);


		/// <summary>
		/// 通过指定id获取ExamineFeedbackListDto信息
		/// </summary>
		Task<ExamineFeedbackListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetExamineFeedbackForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改ExamineFeedback的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateExamineFeedbackInput input);


        /// <summary>
        /// 删除ExamineFeedback信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除ExamineFeedback
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出ExamineFeedback为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
