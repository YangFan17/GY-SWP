
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


using GYSWP.LC_KyjFunctionRecords.Dtos;
using GYSWP.LC_KyjFunctionRecords;
using GYSWP.Dtos;

namespace GYSWP.LC_KyjFunctionRecords
{
    /// <summary>
    /// LC_KyjFunctionRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_KyjFunctionRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_KyjFunctionRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_KyjFunctionRecordListDto>> GetPaged(GetLC_KyjFunctionRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_KyjFunctionRecordListDto信息
		/// </summary>
		Task<LC_KyjFunctionRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_KyjFunctionRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_KyjFunctionRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_KyjFunctionRecordInput input);


        /// <summary>
        /// 删除LC_KyjFunctionRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_KyjFunctionRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 钉钉添加或者修改LC_KyjFunctionRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> CreateOrUpdateByDDAsync(DDCreateOrUpdateLC_KyjFunctionRecordInput input);

        /// <summary>
        /// 钉钉通过指定条件获取LC_KyjFunctionRecordListDto信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="useTime"></param>
        /// <returns></returns>
        Task<LC_KyjFunctionRecordListDto> GetByDDWhereAsync(string employeeId);

        /// <summary>
        /// 导出LC_KyjFunctionRecord为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
