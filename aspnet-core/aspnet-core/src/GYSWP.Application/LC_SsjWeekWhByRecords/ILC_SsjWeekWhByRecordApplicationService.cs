
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


using GYSWP.LC_SsjWeekWhByRecords.Dtos;
using GYSWP.LC_SsjWeekWhByRecords;

namespace GYSWP.LC_SsjWeekWhByRecords
{
    /// <summary>
    /// LC_SsjWeekWhByRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_SsjWeekWhByRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_SsjWeekWhByRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_SsjWeekWhByRecordListDto>> GetPaged(GetLC_SsjWeekWhByRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_SsjWeekWhByRecordListDto信息
		/// </summary>
		Task<LC_SsjWeekWhByRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_SsjWeekWhByRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_SsjWeekWhByRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_SsjWeekWhByRecordInput input);


        /// <summary>
        /// 删除LC_SsjWeekWhByRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_SsjWeekWhByRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出LC_SsjWeekWhByRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
