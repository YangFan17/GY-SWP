
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


using GYSWP.ExamineDetails.Dtos;
using GYSWP.ExamineDetails;
using GYSWP.Dtos;

namespace GYSWP.ExamineDetails
{
    /// <summary>
    /// ExamineDetail应用层服务的接口方法
    ///</summary>
    public interface IExamineDetailAppService : IApplicationService
    {
        /// <summary>
		/// 获取ExamineDetail的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ExamineDetailListDto>> GetPaged(GetExamineDetailsInput input);


		/// <summary>
		/// 通过指定id获取ExamineDetailListDto信息
		/// </summary>
		Task<ExamineDetailListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetExamineDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改ExamineDetail的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateExamineDetailInput input);


        /// <summary>
        /// 删除ExamineDetail信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除ExamineDetail
        /// </summary>
        Task BatchDelete(List<Guid> input);

        Task<PagedResultDto<ExamineRecordDto>> GetExamineRecordByIdAsync(GetExamineDetailsInput input);
        Task<PagedResultDto<ExamineRecordDto>> GetExamineDetailByCurrentIdAsync(GetExamineDetailsInput input);
        Task<ExamineRecordDto> GetExamineDetailByIdAsync(GetExamineDetailsInput input);
        Task<APIResultDto> ChangeStatusByIdAsync(GetExamineDetailsInput input);
        Task<PagedResultDto<ExamineListDto>> GetExamineDetailByEmpIdAsync(GetExamineDetailsInput input);
    }
}
