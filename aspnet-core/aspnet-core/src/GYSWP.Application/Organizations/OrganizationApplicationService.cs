
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


using GYSWP.Organizations;
using GYSWP.Organizations.Dtos;
using GYSWP.Organizations.DomainService;
using GYSWP.Employees;
using GYSWP.Dtos;
using Senparc.CO2NET.HttpUtility;
using GYSWP.Employees.Dtos;

namespace GYSWP.Organizations
{
    /// <summary>
    /// Organization应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class OrganizationAppService : GYSWPAppServiceBase, IOrganizationAppService
    {
        private readonly IRepository<Organization, long> _entityRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IOrganizationManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public OrganizationAppService(
        IRepository<Organization, long> entityRepository
        , IRepository<Employee, string> employeeRepository
        , IOrganizationManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _employeeRepository = employeeRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取Organization的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<OrganizationListDto>> GetPaged(GetOrganizationsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<OrganizationListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<OrganizationListDto>>();

            return new PagedResultDto<OrganizationListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取OrganizationListDto信息
        /// </summary>

        public async Task<OrganizationListDto> GetById(EntityDto<long> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<OrganizationListDto>();
        }

        /// <summary>
        /// 获取编辑 Organization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetOrganizationForEditOutput> GetForEdit(NullableIdDto<long> input)
        {
            var output = new GetOrganizationForEditOutput();
            OrganizationEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<OrganizationEditDto>();

                //organizationEditDto = ObjectMapper.Map<List<organizationEditDto>>(entity);
            }
            else
            {
                editDto = new OrganizationEditDto();
            }

            output.Organization = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Organization的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateOrganizationInput input)
        {

            if (input.Organization.Id.HasValue)
            {
                await Update(input.Organization);
            }
            else
            {
                await Create(input.Organization);
            }
        }


        /// <summary>
        /// 新增Organization
        /// </summary>

        protected virtual async Task<OrganizationEditDto> Create(OrganizationEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Organization>(input);
            var entity = input.MapTo<Organization>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<OrganizationEditDto>();
        }

        /// <summary>
        /// 编辑Organization
        /// </summary>

        protected virtual async Task Update(OrganizationEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Organization信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Organization的方法
        /// </summary>

        public async Task BatchDelete(List<long> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 按需获取组织架构
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationNzTreeNode>> GetTreesAsync()
        {
            int? count = 0;
            var organizationList = await (from o in _entityRepository.GetAll()
                                          select new OrganizationListDto()
                                          {
                                              Id = o.Id,
                                              DepartmentName = o.DepartmentName,
                                              OrgDeptName = o.DepartmentName,
                                              ParentId = o.ParentId
                                          }).ToListAsync();
            foreach (var item in organizationList)
            {
                if (item.Id == 1)
                    count = await _employeeRepository.GetAll().CountAsync();
                else
                    count = await _employeeRepository.GetAll().Where(v => v.Department.Contains(item.Id.ToString())).CountAsync();
                item.Id = item.Id;
                item.ParentId = item.ParentId;
                item.DepartmentName = item.DepartmentName + $"({count}人)";
            }
            return GetTrees(0, organizationList);
        }
        private List<OrganizationNzTreeNode> GetTrees(long? id, List<OrganizationListDto> organizationList)
        {
            List<OrganizationNzTreeNode> treeNodeList = organizationList.Where(o => o.ParentId == id).Select(t => new OrganizationNzTreeNode()
            {
                key = t.Id.ToString(),
                title = t.DepartmentName,
                deptName = t.OrgDeptName,
                children = GetTrees(t.Id, organizationList)
            }).ToList();
            return treeNodeList;
        }
        /// <summary>
        /// 同步组织架构&内部员工
        /// </summary>
        public async Task<APIResultDto> SynchronousOrganizationAsync()
        {
            string accessToken = "46a654e963ef3fa299dc5a7a34181cb5";
            //string accessToken = _dingDingAppService.GetAccessTokenByApp(DingDingAppEnum.会议申请); //GetAccessToken();
            var depts = Get.GetJson<DingDepartmentDto>(string.Format("https://oapi.dingtalk.com/department/list?access_token={0}", accessToken));
            var entityByDD = depts.department.Select(o => new OrganizationListDto()
            {
                Id = o.id,
                DepartmentName = o.name,
                ParentId = o.parentid,
                CreationTime = DateTime.Now
            }).ToList();

            var originEntity = await _entityRepository.GetAll().ToListAsync();
            foreach (var item in entityByDD)
            {
                var o = originEntity.Where(r => r.Id == item.Id).FirstOrDefault();
                if (o != null)
                {
                    o.DepartmentName = item.DepartmentName;
                    o.ParentId = item.ParentId;
                    o.CreationTime = DateTime.Now;
                    if (o.Id != 1)
                    {
                        await SynchronousEmployeeAsync(o.Id, accessToken);
                    }
                }
                else
                {
                    var organization = new OrganizationListDto();
                    organization.Id = item.Id;
                    organization.DepartmentName = item.DepartmentName;
                    organization.ParentId = item.ParentId;
                    organization.CreationTime = DateTime.Now;
                    await CreateSyncOrganizationAsync(organization);
                    if (organization.Id != 1)
                    {
                        await SynchronousEmployeeAsync(organization.Id, accessToken);
                    }
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return new APIResultDto() { Code = 0, Msg = "同步组织架构成功" };
        }

        /// <summary>
        /// 同步内部员工
        /// </summary>
        private async Task<APIResultDto> SynchronousEmployeeAsync(long departId, string accessToken)
        {
            try
            {
                var url = string.Format("https://oapi.dingtalk.com/user/list?access_token={0}&department_id={1}", accessToken, departId);
                var user = Get.GetJson<DingUserListDto>(url);
                var entityByDD = user.userlist.Select(e => new EmployeeListDto()
                {
                    Id = e.userid,
                    Name = e.name,
                    Mobile = e.mobile,
                    Position = e.position,
                    Department = e.departmentStr,
                    IsAdmin = e.isAdmin,
                    IsBoss = e.isBoss,
                    Email = e.email,
                    HiredDate = e.hiredDate,
                    Avatar = e.avatar,
                    Active = e.active,
                    Unionid = e.unionid
                }).ToList();
                var originEntity = await _employeeRepository.GetAll().ToListAsync();
                foreach (var item in entityByDD)
                {
                    var e = originEntity.Where(r => r.Id == item.Id).FirstOrDefault();
                    if (e != null)
                    {
                        e.Name = item.Name;
                        e.Mobile = item.Mobile;
                        e.Position = item.Position;
                        e.Department = item.Department;
                        e.IsAdmin = item.IsAdmin;
                        e.IsBoss = item.IsBoss;
                        e.Email = item.Email;
                        e.HiredDate = item.HiredDate;
                        e.Avatar = item.Avatar;
                        e.Active = item.Active;
                        e.Unionid = item.Unionid;
                    }
                    else
                    {
                        var employee = new EmployeeListDto();
                        employee.Id = item.Id;
                        employee.Name = item.Name;
                        employee.Mobile = item.Mobile;
                        employee.Position = item.Position;
                        employee.Department = item.Department;
                        employee.IsAdmin = item.IsAdmin;
                        employee.IsBoss = item.IsBoss;
                        employee.Email = item.Email;
                        employee.HiredDate = item.HiredDate;
                        employee.Avatar = item.Avatar;
                        employee.Active = item.Active;
                        employee.Unionid = item.Unionid;
                        await CreateSyncEmployeeAsync(employee);
                    }
                }
                await CurrentUnitOfWork.SaveChangesAsync();
                return new APIResultDto() { Code = 0, Msg = "同步内部员工成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SynchronousEmployeeAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "同步内部员工失败" };
            }
        }

        /// <summary>
        /// 插入组织架构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<Organization> CreateSyncOrganizationAsync(OrganizationListDto input)
        {
            var entity = ObjectMapper.Map<Organization>(input);
            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<Organization>();
        }

        private async Task<Employee> CreateSyncEmployeeAsync(EmployeeListDto input)
        {
            var entity = ObjectMapper.Map<Employee>(input);
            entity = await _employeeRepository.InsertAsync(entity);
            return entity.MapTo<Employee>();
        }
    }
}


