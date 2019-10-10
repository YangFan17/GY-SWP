
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
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using GYSWP.Advises;
using GYSWP.Advises.Dtos;
using GYSWP.Advises.DomainService;
using GYSWP.Employees;
using GYSWP.Organizations;
using GYSWP.Authorization.Users;
using GYSWP.Dtos;

namespace GYSWP.Advises
{
    /// <summary>
    /// Advise应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class AdviseAppService : GYSWPAppServiceBase, IAdviseAppService
    {
        private readonly IRepository<Advise, Guid> _entityRepository;
        private readonly UserManager _userManager;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<Organization, long> _organizationRepository;

        private readonly IAdviseManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public AdviseAppService(
        IRepository<Advise, Guid> entityRepository
        , UserManager userManager
        , IRepository<Employee, string> employeeRepository
        , IRepository<Organization, long> organizationRepository
        , IAdviseManager entityManager
        )
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _organizationRepository = organizationRepository;
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取Advise的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<AdviseListDto>> GetPagedAsync(GetAdvisesInput input)
        {
            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            var query = _entityRepository.GetAll().Where(aa => aa.EmployeeId == user.EmployeeId);
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<AdviseListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<AdviseListDto>>();

            return new PagedResultDto<AdviseListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取AdviseListDto信息
        /// </summary>

        public async Task<AdviseListDto> GetByIdAsync(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<AdviseListDto>();
        }

        /// <summary>
        /// 获取编辑 Advise
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetAdviseForEditOutput> GetForEditAsync(NullableIdDto<Guid> input)
        {
            var output = new GetAdviseForEditOutput();
            AdviseEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<AdviseEditDto>();

                //adviseEditDto = ObjectMapper.Map<List<adviseEditDto>>(entity);
            }
            else
            {
                editDto = new AdviseEditDto();
            }

            output.Advise = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Advise的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdateAsync(CreateOrUpdateAdviseInput input)
        {

            if (input.Advise.Id.HasValue)
            {
                await Update(input.Advise);
            }
            else
            {
                await Create(input.Advise);
            }
        }

        /// <summary>
        /// 新增Advise
        /// </summary>

        protected virtual async Task<AdviseEditDto> Create(AdviseEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增
            input.IsAdoption = false;
            Organization dept = null;
            Employee employee = null;
            User user=null;
            if (AbpSession.UserId.HasValue)
                user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            if (user != null && !String.IsNullOrEmpty(user.EmployeeId))
            {
                employee =await _employeeRepository.GetAsync(user.EmployeeId);
                long deptId = long.Parse(employee.Department.Replace("[", "").Replace("]",""));
                dept = await _organizationRepository.GetAsync(deptId);
            }
            input.EmployeeId = employee.Id;
            input.EmployeeName = employee.Name;
            input.DeptId = dept.Id;
            input.DeptName = dept.DepartmentName;

            // var entity = ObjectMapper.Map <Advise>(input);
            var entity = input.MapTo<Advise>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<AdviseEditDto>();
        }

        /// <summary>
        /// 编辑Advise
        /// </summary>

        protected virtual async Task Update(AdviseEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Advise信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task DeleteAsync(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Advise的方法
        /// </summary>

        public async Task BatchDeleteAsync(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 合理化建议报表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetAdviseReportsDto>> GetAdviseReportsAsync(GetAdviseReportInputDto input)
        {
            List<GetAdviseReportsDto> list = new List<GetAdviseReportsDto>();
            var advises = _entityRepository.GetAll()
                .Where(aa =>aa.CreationTime >= input.StartTime && aa.CreationTime < input.EndTime);
            GetAdviseReportsDto adviseReportsDto = new GetAdviseReportsDto();
            if (input.DeptId != 1)
                adviseReportsDto.AdviseTotal = await advises.CountAsync(aa => aa.DeptId == input.DeptId);
            else
                adviseReportsDto.AdviseTotal = await advises.CountAsync();
            if (adviseReportsDto.AdviseTotal > 0)
            {
                adviseReportsDto.AdoptionAdviseTotal = await advises.CountAsync(aa => aa.IsAdoption == true);
                float percentage = (float)adviseReportsDto.AdoptionAdviseTotal / adviseReportsDto.AdviseTotal;
                adviseReportsDto.Percentage = Math.Round(percentage, 4) * 100;
                list.Add(adviseReportsDto);
            }
            return list;
        }

        /// <summary>
        /// 导出Advise为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

        /// <summary>
        /// 钉钉添加建议方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateDDAdviceAsync(CreateDDAdviseInput input)
        {
                Advise newAdvise = new Advise();
                newAdvise.AdviseName = input.Advise.AdviseName;
                newAdvise.CurrentSituation = input.Advise.CurrentSituation;
                newAdvise.Solution = input.Advise.Solution;
                newAdvise.EmployeeId = input.Advise.EmployeeId;
                newAdvise.EmployeeName = input.Advise.EmployeeName;
                newAdvise.DeptId = input.Advise.DeptId;
                newAdvise.DeptName = input.Advise.DeptName;
                newAdvise.UnionEmpName = input.Advise.UnionEmpName;

            var entity = await _entityRepository.InsertAsync(newAdvise);
            return new APIResultDto()
            {
                Data = entity,
                Code = 0
            };
        }
    }
}


