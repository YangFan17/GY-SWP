
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

using GYSWP.Employees;
using GYSWP.Employees.Dtos;
using GYSWP.Employees.DomainService;
using GYSWP.Authorization.Users;
using GYSWP.DingDing.Dtos;
using Abp.Auditing;
using GYSWP.DingDing;

namespace GYSWP.Employees
{
    /// <summary>
    /// Employee应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class EmployeeAppService : GYSWPAppServiceBase, IEmployeeAppService
    {
        private readonly IRepository<Employee, string> _entityRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IDingDingAppService _dingDingAppService;
        private readonly IEmployeeManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public EmployeeAppService(
        IRepository<Employee, string> entityRepository
        , IEmployeeManager entityManager
        , IDingDingAppService dingDingAppService
        , IRepository<User, long> userRepository
        )
        {
            _entityRepository = entityRepository;
            _dingDingAppService = dingDingAppService;
            _userRepository = userRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取Employee的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<EmployeeListDto>> GetPaged(GetEmployeesInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<EmployeeListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<EmployeeListDto>>();

            return new PagedResultDto<EmployeeListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取EmployeeListDto信息
        /// </summary>

        public async Task<EmployeeListDto> GetById(EntityDto<string> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<EmployeeListDto>();
        }

        /// <summary>
        /// 获取编辑 Employee
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetEmployeeForEditOutput> GetForEdit(string id)
        {
            var output = new GetEmployeeForEditOutput();
            EmployeeEditDto editDto;

            if (!string.IsNullOrEmpty(id))
            {
                var entity = await _entityRepository.GetAsync(id);

                editDto = entity.MapTo<EmployeeEditDto>();

                //employeeEditDto = ObjectMapper.Map<List<employeeEditDto>>(entity);
            }
            else
            {
                editDto = new EmployeeEditDto();
            }

            output.Employee = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Employee的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateEmployeeInput input)
        {

            if (!string.IsNullOrEmpty(input.Employee.Id))
            {
                await Update(input.Employee);
            }
            else
            {
                await Create(input.Employee);
            }
        }


        /// <summary>
        /// 新增Employee
        /// </summary>

        protected virtual async Task<EmployeeEditDto> Create(EmployeeEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Employee>(input);
            var entity = input.MapTo<Employee>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<EmployeeEditDto>();
        }

        /// <summary>
        /// 编辑Employee
        /// </summary>

        protected virtual async Task Update(EmployeeEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Employee信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<string> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Employee的方法
        /// </summary>

        public async Task BatchDelete(List<string> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task<PagedResultDto<EmployeeListDto>> GetEmployeeListByIdAsync(GetEmployeesInput input)
        {
            if (input.DepartId == "1" || input.DepartId == null)
            {
                var query = _entityRepository.GetAll()
                    //.WhereIf(input.AreaCode.HasValue, e => e.AreaCode == input.AreaCode || deptArr.Contains(e.Department))
                    .WhereIf(!string.IsNullOrEmpty(input.Mobile), u => u.Mobile.Contains(input.Mobile))
                    .WhereIf(!string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name));

                var employeeCount = await query.CountAsync();
                var employees = await query
                        .OrderBy(v => v.Department)
                        .ThenBy(v => v.Id).AsNoTracking()
                        .PageBy(input)
                        .ToListAsync();
                var employeeListDtos = employees.MapTo<List<EmployeeListDto>>();

                return new PagedResultDto<EmployeeListDto>(
                        employeeCount,
                        employeeListDtos
                    );
            }
            else
            {
                var query = _entityRepository.GetAll()
                      .WhereIf(!string.IsNullOrEmpty(input.Mobile), u => u.Mobile.Contains(input.Mobile))
                .WhereIf(!string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name))
                    .Where(v => v.Department.Contains(input.DepartId));
                var employeeCount = await query.CountAsync();

                var employees = await query
                        .OrderBy(v => v.Id).AsNoTracking()
                        .PageBy(input)
                        .ToListAsync();
                var employeeListDtos = employees.MapTo<List<EmployeeListDto>>();

                return new PagedResultDto<EmployeeListDto>(
                        employeeCount,
                        employeeListDtos
                    );
            }
        }

        /// <summary>
        /// 部门选择员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<EmployeeListDto>> GetEmployeeListByDeptIdAsync(GetEmployeesInput input)
        {
            var query = _entityRepository.GetAll().Where(v => v.Department.Contains(input.DepartId))
                  .WhereIf(!string.IsNullOrEmpty(input.KeyWord), u => u.Mobile.Contains(input.KeyWord) || u.Name.Contains(input.KeyWord));
            var employees = await query
                    .OrderBy(v => v.Name).AsNoTracking()
                    .ToListAsync();
            var employeeListDtos = employees.MapTo<List<EmployeeListDto>>();
            return employeeListDtos;
        }

        [AbpAllowAnonymous]
        public async Task<ScanUserInfo> GetEmployeeByUnionIdAsync(string unionId)
        {
            //var emp =  _entityRepository.GetAll().Where(v => v.Unionid == unionId);
            var user = await _userRepository.GetAll().Where(v => v.UnionId == unionId).Select(v => new ScanUserInfo()
            {
                UserName = v.UserName,
                Password = v.Password
            }).FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// 部门选择员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<EmployeeListDto>> GetEmployeeListByExamineAsync(GetEmployeesInput input)
        {
            var user = await GetCurrentUserAsync();
            string position = await _entityRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Position).FirstOrDefaultAsync();
            if (input.DepartName == "营销中心" || input.DepartName == "烟叶生产经营中心")//机关单位含内设部门
            {
                if(position == "主任" )//主任考核部长
                {
                    var query = _entityRepository.GetAll().Where(v => v.Department.Contains(input.DepartId) && v.Position.Contains("部长") &&v.Id != user.EmployeeId);
                    var employees = await query
                            .OrderBy(v => v.Name).AsNoTracking()
                            .ToListAsync();
                    var employeeListDtos = employees.MapTo<List<EmployeeListDto>>();
                    return employeeListDtos;
                }
                else //部长自主选则员工
                {
                    var query = _entityRepository.GetAll().Where(v => v.Department.Contains(input.DepartId) && v.Id != user.EmployeeId && !v.Position.Contains("主任") &&!v.Position.Contains("部长"));
                    var employees = await query
                            .OrderBy(v => v.Name).AsNoTracking()
                            .ToListAsync();
                    var employeeListDtos = employees.MapTo<List<EmployeeListDto>>();
                    return employeeListDtos;
                }

            }
            else if(input.DepartName == "市场营销科" || input.DepartName == "专卖科")//机关无内设部门
            {
                if (position == "主任")//主任考核县级部门
                {
                    var query = _entityRepository.GetAll().Where(v => v.Department.Contains(input.DepartId) && v.Id != user.EmployeeId);
                    var employees = await query
                            .OrderBy(v => v.Name).AsNoTracking()
                            .ToListAsync();
                    var employeeListDtos = employees.MapTo<List<EmployeeListDto>>();
                    return employeeListDtos;
                }
                else //县局科长内部考核
                {
                    var query = _entityRepository.GetAll().Where(v => v.Department.Contains(input.DepartId) && v.Id != user.EmployeeId);
                    var employees = await query
                            .OrderBy(v => v.Name).AsNoTracking()
                            .ToListAsync();
                    var employeeListDtos = employees.MapTo<List<EmployeeListDto>>();
                    return employeeListDtos;
                }
            }
            else
            {
                var query = _entityRepository.GetAll().Where(v => v.Department.Contains(input.DepartId));
                var employees = await query
                        .OrderBy(v => v.Name).AsNoTracking()
                        .ToListAsync();
                var employeeListDtos = employees.MapTo<List<EmployeeListDto>>();
                return employeeListDtos;
            }
        }

        [AbpAllowAnonymous]
        [Audited]

        public async Task<DingDingUserDto> GetDingDingUserByCodeAsync(string code, DingDingAppEnum appId)
        {
            var ddConfig = _dingDingAppService.GetDingDingConfigByApp(appId);
            //测试环境注释
            var assessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            var userId = _dingDingAppService.GetUserId(assessToken, code);
            userId = "16550049332052666774";//测试
            if (userId == null)
            {
                return new DingDingUserDto();
            }
            var query = await _entityRepository.GetAsync(userId);
            return query.MapTo<DingDingUserDto>();
        }
    }
}


