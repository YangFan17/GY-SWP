
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


using GYSWP.Employees.Dtos;
using GYSWP.Employees;

namespace GYSWP.Employees
{
    /// <summary>
    /// Employee应用层服务的接口方法
    ///</summary>
    public interface IEmployeeAppService : IApplicationService
    {
        /// <summary>
		/// 获取Employee的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<EmployeeListDto>> GetPaged(GetEmployeesInput input);


		/// <summary>
		/// 通过指定id获取EmployeeListDto信息
		/// </summary>
		Task<EmployeeListDto> GetById(EntityDto<string> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetEmployeeForEditOutput> GetForEdit(string id);


        /// <summary>
        /// 添加或者修改Employee的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateEmployeeInput input);


        /// <summary>
        /// 删除Employee信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<string> input);


        /// <summary>
        /// 批量删除Employee
        /// </summary>
        Task BatchDelete(List<string> input);
        Task<PagedResultDto<EmployeeListDto>> GetEmployeeListByIdAsync(GetEmployeesInput input);
        Task<List<EmployeeListDto>> GetEmployeeListByDeptIdAsync(GetEmployeesInput input);

        Task<ScanUserInfo> GetEmployeeByUnionIdAsync(string unionId);
    }
}
