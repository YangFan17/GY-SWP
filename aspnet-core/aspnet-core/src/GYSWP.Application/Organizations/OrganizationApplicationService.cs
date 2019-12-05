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
using GYSWP.DingDing.Dtos;
using GYSWP.DingDing;
using GYSWP.DingDingApproval;

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
        private readonly IDingDingAppService _dingDingAppService;
        private readonly IApprovalAppService _approvalAppService;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public OrganizationAppService(
        IRepository<Organization, long> entityRepository
        , IRepository<Employee, string> employeeRepository
        , IOrganizationManager entityManager
        , IDingDingAppService dingDingAppService
        , IApprovalAppService approvalAppService
        )
        {
            _entityRepository = entityRepository;
            _employeeRepository = employeeRepository;
            _entityManager = entityManager;
            _dingDingAppService = dingDingAppService;
            _approvalAppService = approvalAppService;
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
        /// 按需获取组织架构(带人统计)
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
                                              ParentId = o.ParentId,
                                              Order = o.Order
                                          }).OrderBy(v => v.Order).ToListAsync();
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
            //string accessToken = "46a654e963ef3fa299dc5a7a34181cb5";
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
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
        //private List<OrganizationTreeNodeDto>  getDeptChildTree(long pid, List<Organization> depts)
        //{
        //    var trees = depts.Where(d => d.ParentId == pid).Select(d => new OrganizationTreeNodeDto()
        //    {
        //        Key = d.Id,
        //        Title = d.DepartmentName,
        //        Children = getDeptChildTree(d.Id, depts)
        //    });

        //    return trees.ToList();
        //}
        //private async Task<List<OrganizationTreeNodeDto>> GetExamineChildrenAsync(long id)
        //{
        //    var trees = new List<OrganizationTreeNodeDto>();
        //    var depts = await _entityRepository.GetAll().AsNoTracking().ToListAsync();
        //    var dept = depts.Where(d => d.Id == id).First();
        //    trees.Add(new OrganizationTreeNodeDto()
        //    {
        //        Key = dept.Id,
        //        Title = dept.DepartmentName,
        //        Children = getDeptChildTree(id, depts)
        //    });
        //    return trees;
        //}
        private async Task<List<OrganizationTreeNodeDto>> GetExamineChildrenAsync(long? id)
        {
            var list = await _entityRepository.GetAll().Where(c => c.ParentId == id).Select(c => new OrganizationTreeNodeDto()
            {
                Key = c.Id,
                Title = c.DepartmentName,
                ParentId = c.ParentId,
                Order = c.Order
                //Children = GetExamineChildren(c.Id)
            }).OrderBy(v => v.Order).ToListAsync();
            return list;
        }

        /// <summary>
        /// 获取可抽查的部门列表树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>        
        public async Task<OrganizationTreeNodeDto> GetDeptExamineTreeAsync()
        {
            OrganizationTreeNodeDto result = new OrganizationTreeNodeDto();
            result.Key = 0;
            result.Title = "考核部门";
            var user = await GetCurrentUserAsync();
            string deptId = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            string position = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Position).FirstOrDefaultAsync();
            var organization = await _entityRepository.GetAll().Where(v => "[" + v.Id + "]" == deptId).Select(v => new OrganizationTreeNodeDto()
            {
                Key = v.Id,
                Title = v.DepartmentName,
                ParentId = v.ParentId
            }).FirstOrDefaultAsync();
            organization.Children = await GetExamineChildrenAsync(organization.Key);
            result.Children.Add(organization);
            if (organization.ParentId == 1 && (position == "主任" || position == "科长" || (organization.Title == "信息中心" && position == "副主任")))
            {
                List<OrganizationTreeNodeDto> list = new List<OrganizationTreeNodeDto>();
                //string subordinate = "";
                if (organization.Title == "营销中心")
                {
                    //subordinate = "市场营销科";
                    list = await _entityRepository.GetAll().Where(v => (v.DepartmentName == "市场营销科") && v.Id != organization.Key).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId,
                    }).OrderBy(v => v.Key).ToListAsync();
                }
                else if (organization.Title == "专卖科" || organization.Title == "内管派驻办")
                {
                    //subordinate = "专卖科";
                    list = await _entityRepository.GetAll().Where(v => (v.DepartmentName == "专卖科") && v.Id != organization.Key).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId,
                    }).OrderBy(v => v.Key).ToListAsync();
                }
                else if (organization.Title == "办公室" || organization.Title == "人事科" || organization.Title == "思政科" || organization.Title == "信息中心")
                {
                    //subordinate = "办公室";//subordinate = "综合管理部";
                    list = await _entityRepository.GetAll().Where(v => (v.DepartmentName == "办公室" || v.DepartmentName == "综合管理部") && v.Id != organization.Key).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId,
                    }).OrderBy(v => v.Key).ToListAsync();
                }
                else if (organization.Title == "财务科")
                {
                    //subordinate = "财务管理科";//烟产区 //subordinate = "办公室";//纯销区
                    list = await _entityRepository.GetAll()
                        .Where(v => ((v.ParentId == 59634065 && v.DepartmentName == "办公室")
                        || (v.ParentId == 59587088 && v.DepartmentName == "办公室")
                        || (v.ParentId == 59584066 && v.DepartmentName == "办公室")
                        || (v.ParentId == 59549059 && v.DepartmentName == "办公室")
                        || v.DepartmentName == "综合管理部"
                        || v.DepartmentName == "财务管理科") && v.Id != organization.Key).Select(v => new OrganizationTreeNodeDto()
                        {
                            Key = v.Id,
                            Title = v.DepartmentName,
                            ParentId = v.ParentId,
                        }).OrderBy(v => v.Key).ToListAsync();
                }
                else if (organization.Title == "法规科" || organization.Title == "派驻审计办" || organization.Title == "企管科" || organization.Title == "监察科")
                {
                    //subordinate = "综合办";//subordinate = "综合管理部";//subordinate = "综合科";
                    list = await _entityRepository.GetAll().Where(v => (v.DepartmentName == "综合办" || v.DepartmentName == "综合管理部" || v.DepartmentName == "综合科") && v.Id != organization.Key).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId,
                    }).OrderBy(v => v.Key).ToListAsync();
                }
                else if (organization.Title == "安全科")
                {
                    //subordinate = "综合办";//subordinate = "综合管理部";//subordinate = "综合科";//subordinate = "安全保卫科";//subordinate = "安全管理部";
                    list = await _entityRepository.GetAll().Where(v => (v.DepartmentName == "综合办" || v.DepartmentName == "综合管理部" || v.DepartmentName == "综合科" || v.DepartmentName == "安全保卫科" || v.DepartmentName == "安全管理部") && v.Id != organization.Key).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId,
                    }).OrderBy(v => v.Key).ToListAsync();
                }
                else if (organization.Title == "投资科")
                {
                    //subordinate = "综合办";//subordinate = "综合管理部";//subordinate = "综合科";//subordinate = "烟叶基础设施建设办公室";
                    list = await _entityRepository.GetAll().Where(v => (v.DepartmentName == "综合办" || v.DepartmentName == "综合管理部" || v.DepartmentName == "综合科" || v.DepartmentName == "烟叶基础设施建设办公室") && v.Id != organization.Key).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId,
                    }).OrderBy(v => v.Key).ToListAsync();
                }
                else if (organization.Title == "烟叶生产经营中心")
                {
                    //subordinate = "烟叶生产科";//subordinate = "烟站||烟点";
                    //list = await _entityRepository.GetAll().Where(v => (v.DepartmentName == "烟叶生产科" || v.Id == 59523784 || v.Id == 59523787 //昭化
                    //        || v.Id == 59954059 || v.Id == 99768033 || v.Id == 100648776 || v.Id == 100738759//旺苍
                    //        || v.Id == 59943105 || v.Id == 59949047 || v.Id == 59949048 || v.Id == 60034045) && v.Id != organization.Key).Select(v => new OrganizationTreeNodeDto()
                    //        {
                    //            Key = v.Id,
                    //            Title = v.DepartmentName,
                    //            ParentId = v.ParentId,
                    //        }).OrderBy(v => v.Key).ToListAsync();
                    //昭化
                    var zhaoHuaZhanDian = await _entityRepository.GetAll().Where(v => v.Id == 59523787).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId
                    }).OrderBy(v => v.Key).ToListAsync();
                    var zhaoHua = await _entityRepository.GetAll().Where(v => v.Id == 59523784)
                        .Select(v => new OrganizationTreeNodeDto()
                        {
                            Key = v.Id,
                            Title = v.DepartmentName,
                            ParentId = v.ParentId,
                            Children = zhaoHuaZhanDian
                        }).OrderBy(v => v.Key).FirstOrDefaultAsync();
                    //剑阁
                    var jianGeZhanDian = await _entityRepository.GetAll().Where(v => v.Id == 59949047 || v.Id == 59949048 || v.Id == 60034045).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId
                    }).OrderBy(v => v.Key).ToListAsync();
                    var jianGe = await _entityRepository.GetAll().Where(v => v.Id == 59943105).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId,
                        Children = jianGeZhanDian
                    }).OrderBy(v => v.Key).FirstOrDefaultAsync();
                    //旺苍
                    var wangCangZhanDian = await _entityRepository.GetAll().Where(v => v.Id == 99768033 || v.Id == 100648776 || v.Id == 100738759).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId
                    }).OrderBy(v => v.Key).ToListAsync();
                    var wangCang = await _entityRepository.GetAll().Where(v => v.Id == 59954059).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        ParentId = v.ParentId,
                        Children = wangCangZhanDian
                    }).OrderBy(v => v.Key).FirstOrDefaultAsync();
                    list.Add(zhaoHua);
                    list.Add(jianGe);
                    list.Add(wangCang);
                }

                var parentIds = list.GroupBy(v => v.ParentId).Select(v => v.Key).Where(v => v != 1).ToList();
                foreach (var pId in parentIds)
                {
                    var temp = await _entityRepository.GetAll().Where(v => v.Id == pId).Select(v => new OrganizationTreeNodeDto()
                    {
                        Key = v.Id,
                        Title = v.DepartmentName,
                        Disabled = true
                    }).FirstOrDefaultAsync();
                    foreach (var item in list.Where(v => v.ParentId == pId))
                    {
                        temp.Children.Add(item);
                    }
                    result.Children.Add(temp);
                }
                //foreach (var item in list)
                //{
                //    var temp = await _entityRepository.GetAll().Where(v => v.Id == item.ParentId).Select(v => new OrganizationTreeNodeDto()
                //    {
                //        Key = v.Id,
                //        Title = v.DepartmentName,
                //        Disabled = true
                //    }).FirstOrDefaultAsync();
                //    temp.Children.Add(item);
                //    result.Children.Add(temp);
                //}
            }
            if (result.Children.Count == 0)
            {
                result.Children.Add(new OrganizationTreeNodeDto()
                {
                    Key = -1,
                    Title = "没有任何部门权限"
                });
            }
            else
            {
                result.Children[0].Selected = true;
            }
            return result;
        }

        /// <summary>
        /// 企管科超级管理员考核部门
        /// </summary>
        /// <returns></returns>
        public async Task<OrganizationTreeNodeDto> GetDeptTreeByQGAdminAsync()
        {
            OrganizationTreeNodeDto result = new OrganizationTreeNodeDto();
            result.Key = 0;
            result.Title = "考核部门";
            var organization = await _entityRepository.GetAll().Where(v => v.ParentId == 1).OrderBy(v=>v.Order).Select(v => new OrganizationTreeNodeDto()
            {
                Key = v.Id,
                Title = v.DepartmentName,
                ParentId = v.ParentId
            }).ToListAsync();
            result.Children.AddRange(organization);
            return result;
        }

        /// <summary>
        /// 县局管理员考核部门
        /// </summary>
        /// <returns></returns>
        public async Task<OrganizationTreeNodeDto> GetDeptTreeByCountyAdminAsync()
        {
            var user = await GetCurrentUserAsync();
            string deptId = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var orgInfo = await _entityRepository.GetAll().Where(v => "[" + v.Id + "]" == deptId).Select(v => new { v.Id, v.ParentId }).FirstOrDefaultAsync();
            long? id;
            if (orgInfo.ParentId != 1)
            {
                long? resultId = orgInfo.ParentId;
                id = GetTopDeptId(orgInfo.ParentId, ref resultId);
            }
            else
            {
                id = orgInfo.Id;
            }
            OrganizationTreeNodeDto result = new OrganizationTreeNodeDto();
            result.Key = 0;
            result.Title = "考核部门";
            var organization = await _entityRepository.GetAll().Where(v => v.Id == id).Select(v => new OrganizationTreeNodeDto()
            {
                Key = v.Id,
                Title = v.DepartmentName,
                ParentId = v.ParentId
            }).FirstOrDefaultAsync();
            var allList = await _entityRepository.GetAll().Select(v => new OrganizationTreeNodeDto()
            {
                Key = v.Id,
                Title = v.DepartmentName,
                ParentId = v.ParentId
            }).ToListAsync();
            organization.Children = GetDeptChildrenAsync(organization.Key, allList);
            result.Children.Add(organization);
            return result;
        }

        /// <summary>
        /// 查询当前部门所以子级部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<OrganizationTreeNodeDto> GetDeptChildrenAsync(long id, List<OrganizationTreeNodeDto> childrenList)
        {
            var list = childrenList.Where(c => c.ParentId == id).Select(c => new OrganizationTreeNodeDto()
            {
                Key = c.Key,
                Title = c.Title,
                ParentId = c.ParentId,
                Children = GetDeptChildrenAsync(c.Key, childrenList)
            });
            return list.ToList();
        }

        /// <summary>
        /// 查询顶级部门Id
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        private long? GetTopDeptId(long? pId, ref long? resultId)
        {
            var result = _entityRepository.GetAll().Where(v => v.Id == pId).Select(v => new { v.ParentId, v.Id }).FirstOrDefault();
            resultId = result.Id;
            if (result.ParentId != 1)
            {
                GetTopDeptId(result.ParentId, ref resultId);
            }
            return resultId;
        }

        /// <summary>
        /// 考核指标部门树
        /// </summary>
        /// <returns></returns>
        public async Task<OrganizationTreeNodeDto> GetTargetExamineDeptTreeAsync()
        {
            OrganizationTreeNodeDto result = new OrganizationTreeNodeDto
            {
                Key = 0,
                Title = "考核部门"
            };
            var organization = await _entityRepository.GetAll().Where(v => v.ParentId == 1 && v.DepartmentName != "物流中心" && !v.DepartmentName.Contains("公司")).Select(v => new OrganizationTreeNodeDto()
            {
                Key = v.Id,
                Title = v.DepartmentName,
                ParentId = v.ParentId
            }).ToListAsync();
            result.Children.AddRange(organization);
            return result;
        }

        /// <summary>
        /// 指标考核部门
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationNzTreeNode>> GetTargetTreesAsync()
        {
            var organizationList = await (from o in _entityRepository.GetAll()
                                          select new OrganizationListDto()
                                          {
                                              Id = o.Id,
                                              DepartmentName = o.DepartmentName,
                                              OrgDeptName = o.DepartmentName,
                                              ParentId = o.ParentId,
                                              Order = o.Order
                                          }).OrderBy(v => v.Order).ToListAsync();

            return GetTargetTrees(0, organizationList);
        }
        private List<OrganizationNzTreeNode> GetTargetTrees(long? id, List<OrganizationListDto> organizationList)
        {
            List<OrganizationNzTreeNode> treeNodeList = organizationList.Where(o => o.ParentId == id).Select(t => new OrganizationNzTreeNode()
            {
                key = t.Id.ToString(),
                title = t.DepartmentName,
                deptName = t.OrgDeptName,
                order = t.Order,
                children = GetTargetTrees(t.Id, organizationList)
            }).OrderBy(v => v.order).ToList();
            return treeNodeList;
        }

        /// <summary>
        /// 获取当前用户部门Id
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetDeptIdAsync()
        {
            var user = await GetCurrentUserAsync();
            string deptStr = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            string deptId = deptStr.Replace('[', ' ').Replace(']', ' ').Trim();
            return deptId;
        }

        /// <summary>
        /// 判断是否为基层单位
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<bool> GetIsCountrnDeptAsync(long deptId)
        {
            var user = await GetCurrentUserAsync();
            if (deptId == 59549057 || deptId == 59646091 || deptId == 59552081 || deptId == 59632058
              || deptId == 59571109 || deptId == 59584063 || deptId == 59644078 || deptId == 59620071 || deptId == 59628060
              || deptId == 59538081 || deptId == 59490590 || deptId == 59591062 || deptId == 59481641 || deptId == 59534185
              || deptId == 59534184 || deptId == 59593071 || deptId == 59534183)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 钉钉接口方法---根据用户ID查用户部门名字
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<APIResultDto> GetOrganizationInfo(string EmployeeId)
        {
            string deptStr = await _employeeRepository.GetAll().Where(v => v.Id == EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            string deptId = "";
            if (deptStr.Contains("]["))
            {
                var depts = deptStr.Substring(1, deptStr.Length - 2).Split("][");//多部门拆分
                deptId = depts[0];
            }
            else
            {
                deptId = deptStr.Replace('[', ' ').Replace(']', ' ').Trim();
            }
            var deptInfo = await _entityRepository.GetAll().Where(v => v.Id.ToString() == deptId).Select(v => new { v.Id, v.DepartmentName }).FirstOrDefaultAsync();
            DingDingAppConfig ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
            string accessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
            APIResultDto spaceInfo = _approvalAppService.GetProcessinstanceSpace(accessToken, EmployeeId);
            if (spaceInfo.Code == 0)
            {
                return new APIResultDto()
                {
                    Code = 0,
                    Data = new {
                        deptInfo.Id,
                        deptInfo.DepartmentName,
                        spaceInfo.Data
                    }
                };
            }
            return new APIResultDto()
            {
                Code = 999,
                Msg = "审批钉盘信息获取失败"
            };
        }
    }
}