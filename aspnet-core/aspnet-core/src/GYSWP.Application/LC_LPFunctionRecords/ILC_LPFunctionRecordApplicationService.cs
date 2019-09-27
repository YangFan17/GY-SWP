
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


using GYSWP.LC_LPFunctionRecords.Dtos;
using GYSWP.LC_LPFunctionRecords;
using GYSWP.Dtos;

namespace GYSWP.LC_LPFunctionRecords
{
    /// <summary>
    /// LC_LPFunctionRecord应用层服务的接口方法
    ///</summary>
    public interface ILC_LPFunctionRecordAppService : IApplicationService
    {
        /// <summary>
		/// 获取LC_LPFunctionRecord的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LC_LPFunctionRecordListDto>> GetPaged(GetLC_LPFunctionRecordsInput input);


		/// <summary>
		/// 通过指定id获取LC_LPFunctionRecordListDto信息
		/// </summary>
		Task<LC_LPFunctionRecordListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetLC_LPFunctionRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改LC_LPFunctionRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateLC_LPFunctionRecordInput input);


        /// <summary>
        /// 删除LC_LPFunctionRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除LC_LPFunctionRecord
        /// </summary>
        Task BatchDelete(List<Guid> input);


        /// <summary>
        /// 钉钉添加或者修改LC_LPFunctionRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<APIResultDto> CreateOrUpdateByDDAsync(DDCreateOrUpdateLC_LPFunctionRecordInput input);

        /// <summary>
        /// 钉钉通过指定条件获取LC_LPFunctionRecordListDto信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="useTime"></param>
        /// <returns></returns>
        Task<LC_LPFunctionRecordListDto> GetByDDWhereAsync(string employeeId);

        /// <summary>
        /// 导出LC_LPFunctionRecord为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
