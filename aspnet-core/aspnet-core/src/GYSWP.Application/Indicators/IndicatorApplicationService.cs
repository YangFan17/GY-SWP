
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


using GYSWP.Indicators;
using GYSWP.Indicators.Dtos;
using GYSWP.Indicators.DomainService;
using GYSWP.IndicatorsDetails;
using GYSWP.Dtos;
using GYSWP.Organizations;
using GYSWP.Employees;
using GYSWP.DingDingApproval;
using GYSWP.Documents;
using GYSWP.Employees.DomainService;

namespace GYSWP.Indicators
{
    /// <summary>
    /// Indicator应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class IndicatorAppService : GYSWPAppServiceBase, IIndicatorAppService
    {
        private readonly IRepository<Indicator, Guid> _entityRepository;
        private readonly IIndicatorManager _entityManager;
        private readonly IRepository<IndicatorsDetail, Guid> _indicatorsDetailRepository;
        private readonly IRepository<Organization, long> _organizationRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IApprovalAppService _approvalAppService;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IEmployeeManager _employeeManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public IndicatorAppService(
        IRepository<Indicator, Guid> entityRepository
        , IIndicatorManager entityManager
        , IRepository<IndicatorsDetail, Guid> indicatorsDetailRepository
        , IRepository<Organization, long> organizationRepository
        , IRepository<Employee, string> employeeRepository
        , IRepository<Document, Guid> documentRepository
        , IApprovalAppService approvalAppService
        , IEmployeeManager employeeManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _indicatorsDetailRepository = indicatorsDetailRepository;
            _organizationRepository = organizationRepository;
            _employeeRepository = employeeRepository;
            _approvalAppService = approvalAppService;
            _documentRepository = documentRepository;
            _employeeManager = employeeManager;
        }


        /// <summary>
        /// 获取Indicator的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<IndicatorListDto>> GetPaged(GetIndicatorsInput input)
        {
            var user = await GetCurrentUserAsync();
            string deptStr = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            string deptId = deptStr.Replace('[', ' ').Replace(']', ' ').Trim();
            var doc = _documentRepository.GetAll().Select(v => new { v.Id, v.Name });
            var indicator = _entityRepository.GetAll().Where(v => v.CreatorDeptId.ToString() == deptId).WhereIf(input.CycleTime.HasValue, v => v.CycleTime == input.CycleTime);
            var query = from i in indicator
                        join d in doc on i.SourceDocId equals d.Id
                        select new IndicatorListDto()
                        {
                            Id = i.Id,
                            CreationTime = i.CreationTime,
                            //CreatorEmpName = i.CreatorEmpName,
                            Title = i.Title,
                            Paraphrase = i.Paraphrase,
                            MeasuringWay = i.MeasuringWay,
                            ExpectedValue = i.ExpectedValue,
                            CycleTime = i.CycleTime,
                            AchieveType = i.AchieveType,
                            SourceDocName = d.Name,
                            IsAction = i.IsAction
                        };

            var count = await query.CountAsync();
            var entityList = await query
                    .OrderByDescending(v => v.IsAction).ThenByDescending(v => v.CycleTime).ThenByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<IndicatorListDto>>();
            return new PagedResultDto<IndicatorListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取IndicatorListDto信息
        /// </summary>
        public async Task<IndicatorListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);
            string docName = await _documentRepository.GetAll().Where(v => v.Id == entity.SourceDocId).Select(v => v.Name).FirstOrDefaultAsync();
            var result = entity.MapTo<IndicatorListDto>();
            result.SourceDocName = docName;
            return result;
        }

        /// <summary>
        /// 获取编辑 Indicator
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetIndicatorForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetIndicatorForEditOutput();
            IndicatorEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<IndicatorEditDto>();

                //indicatorEditDto = ObjectMapper.Map<List<indicatorEditDto>>(entity);
            }
            else
            {
                editDto = new IndicatorEditDto();
            }

            output.Indicator = editDto;
            return output;
        }

        /// <summary>
        /// 添加或者修改Indicator的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateIndicatorInput input)
        {
            if (input.Indicator.Id.HasValue)
            {
                await Update(input.Indicator);
                return new APIResultDto() { Code = 0, Msg = "保存成功" };
            }
            else
            {
                var user = await GetCurrentUserAsync();
                string deptId = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
                var organization = await _organizationRepository.GetAll().Where(v => "[" + v.Id + "]" == deptId).Select(v => new { v.Id, v.DepartmentName }).FirstOrDefaultAsync();
                input.Indicator.CreatorEmpeeId = user.EmployeeId;
                input.Indicator.CreatorEmpName = user.EmployeeName;
                input.Indicator.CreatorDeptId = organization.Id;
                input.Indicator.CreatorDeptName = organization.DepartmentName;
                //input.Indicator.EndTime = input.Indicator.EndTime.ToDayEnd();
                var entity = await Create(input.Indicator);
                await CurrentUnitOfWork.SaveChangesAsync();
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
            }
        }

        /// <summary>
        /// 批量发布指标考核
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> PublishIndicatorByIdAsync(PublishIndicatorInput input)
        {
            foreach (var id in input.IndicatorList)
            {
                var entity = await _entityRepository.GetAsync(id);
                string[] deptStrList = entity.DeptIds.Split(',');
                long[] deptIdList = Array.ConvertAll<string, long>(deptStrList, s => long.Parse(s));
                foreach (var deptId in deptIdList.Where(v => v != 1 && v != 59549057))//过滤顶级部门
                {
                    string deptName = await _organizationRepository.GetAll().Where(v => v.Id == deptId).Select(v => v.DepartmentName).FirstOrDefaultAsync();
                    var examEmp = new ExamineUser();
                    //机关部门
                    if (deptId == 59481641 || deptId == 59490590 || deptId == 59534183 || deptId == 59534184 || deptId == 59534185
                        || deptId == 59538081 || deptId == 59552081 || deptId == 59571109 || deptId == 59584063 || deptId == 59591062
                        || deptId == 59620071 || deptId == 59628060 || deptId == 59632058 || deptId == 59644078 || deptId == 59646091)
                    {
                        var adminList = await GetUsersInRoleAsync("StandardAdmin");
                        string[] adminIds = adminList.Select(v => v.EmployeeId).ToArray();
                        examEmp = await _employeeRepository.GetAll().Where(v => adminIds.Contains(v.Id) && v.Department == "[" + deptId + "]").Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
                    }
                    //基层单位
                    else
                    {
                        examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + deptId + "]" && (v.Position.Contains("主任") && !v.Position.Contains("!副主任"))).Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
                        if (examEmp == null)
                        {
                            examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + deptId + "]" && v.Position.Contains("副主任")).Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
                            if (examEmp == null)
                            {
                                examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + deptId + "]" && (v.Position.Contains("部长") && !v.Position.Contains("!副部长"))).Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
                                if (examEmp == null)
                                {
                                    examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + deptId + "]" && v.Position.Contains("!副部长")).Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
                                    if (examEmp == null)
                                    {
                                        examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + deptId + "]" && (v.Position.Contains("科长") && !v.Position.Contains("!副科长"))).Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
                                        if (examEmp == null)
                                        {
                                            examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + deptId + "]" && v.Position.Contains("!副科长")).Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
                                            if (examEmp == null)
                                            {
                                                return new APIResultDto() { Code = 999, Msg = $"发布失败，当前部门[{deptName}]无负责人" };
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    IndicatorsDetail detail = new IndicatorsDetail();
                    detail.IndicatorsId = entity.Id;
                    detail.DeptId = deptId;
                    detail.DeptName = deptName;
                    detail.EmployeeId = examEmp.Id;
                    detail.EmployeeName = examEmp.Name;
                    detail.Status = GYEnums.IndicatorStatus.未填写;
                    detail.EndTime = input.EndTime.ToDayEnd();
                    await _indicatorsDetailRepository.InsertAsync(detail);
                }
            }
            return new APIResultDto() { Code = 0, Msg = "发布成功" };
        }

        //public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateIndicatorInput input)
        //{
        //    if (input.Indicator.Id.HasValue)
        //    {
        //        await Update(input.Indicator);
        //        return new APIResultDto() { Code = 0, Msg = "保存成功" };

        //    }
        //    else
        //    {
        //        var user = await GetCurrentUserAsync();
        //        string deptId = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
        //        var organization = await _organizationRepository.GetAll().Where(v => "[" + v.Id + "]" == deptId).Select(v => new { v.Id, v.DepartmentName }).FirstOrDefaultAsync();
        //        input.Indicator.CreatorEmpeeId = user.EmployeeId;
        //        input.Indicator.CreatorEmpName = user.EmployeeName;
        //        input.Indicator.CreatorDeptId = organization.Id;
        //        input.Indicator.CreatorDeptName = organization.DepartmentName;
        //        input.Indicator.EndTime = input.Indicator.EndTime.ToDayEnd();
        //        var entity = await Create(input.Indicator);
        //        await CurrentUnitOfWork.SaveChangesAsync();
        //        var adminList = await GetUsersInRoleAsync("StandardAdmin");
        //        string[] adminIds = adminList.Select(v => v.EmployeeId).ToArray();
        //        var countryAdminList = await GetUsersInRoleAsync("CountyAdmin");
        //        string[] countryAdminIds = countryAdminList.Select(v => v.EmployeeId).ToArray();
        //        foreach (var item in input.DeptInfo.Where(v => v.DeptId != 1))//过滤顶级部门
        //        {
        //            var examEmp = new ExamineUser();
        //            //机关单位
        //            if (item.DeptId == 59481641 || item.DeptId == 59490590 || item.DeptId == 59534183 || item.DeptId == 59534184 || item.DeptId == 59534185
        //                || item.DeptId == 59538081 || item.DeptId == 59552081 || item.DeptId == 59571109 || item.DeptId == 59584063 || item.DeptId == 59591062
        //                || item.DeptId == 59620071 || item.DeptId == 59628060 || item.DeptId == 59632058 || item.DeptId == 59644078 || item.DeptId == 59646091)
        //            {
        //                examEmp = await _employeeRepository.GetAll().Where(v => adminIds.Contains(v.Id) && v.Department == "[" + item.DeptId + "]").Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
        //            }
        //            //else if (item.DeptId == 59593071)//物流中心
        //            //{
        //            //    examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[60007074]" && v.Position == "部长").Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
        //            //}
        //            //else//县局
        //            //{
        //            //    examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + item.DeptId + "]" && (v.Position == "主任" || v.Position == "科长")).Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
        //            //}
        //            else
        //            {
        //                examEmp = await GetCountryStandardAdminByIdAsync(item.DeptId);
        //            }
        //            IndicatorsDetail detail = new IndicatorsDetail();
        //            detail.IndicatorsId = entity.Id.Value;
        //            detail.DeptId = item.DeptId;
        //            detail.DeptName = item.DeptName;
        //            detail.EmployeeId = examEmp.Id;
        //            detail.EmployeeName = examEmp.Name;
        //            detail.Status = GYEnums.IndicatorStatus.未填写;
        //            await _indicatorsDetailRepository.InsertAsync(detail);
        //            //发送钉钉通知
        //            if (!string.IsNullOrEmpty(examEmp.Id))
        //            {
        //                _approvalAppService.SendIndicatorMessageAsync(examEmp.Id);
        //            }
        //        }
        //        return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
        //    }
        //}


        /// <summary>
        /// 新增Indicator
        /// </summary>

        protected virtual async Task<IndicatorEditDto> Create(IndicatorEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Indicator>(input);
            var entity = input.MapTo<Indicator>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<IndicatorEditDto>();
        }

        /// <summary>
        /// 编辑Indicator
        /// </summary>

        protected virtual async Task Update(IndicatorEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Indicator信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Indicator的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 查询个人考核指标数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<IndicatorShowDto>> GetPagedCurrentIndicatorAsync(GetIndicatorsInput input)
        {
            var user = await GetCurrentUserAsync();
            var userInfo = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => new { v.Position, v.Department }).FirstOrDefaultAsync();
            if ((userInfo.Department == "[" + 59593071 + "]" || userInfo.Department == "[" + 59634065 + "]"|| userInfo.Department == "[" + 59569075 + "]" || userInfo.Department == "[" + 59584066 + "]" || userInfo.Department == "[" + 59594070 + "]"|| userInfo.Department == "[" + 59617065 + "]" || userInfo.Department == "[" + 59549059 + "]" || userInfo.Department == "[" + 59587088 + "]")
                 && (userInfo.Position.Contains("县区局（分公司）局长") || userInfo.Position.Contains("物流中心主任")))
            {
                string deptId = userInfo.Department.Replace('[', ' ').Replace(']', ' ').Trim();
                string[] empIds = await GetEmployeeIdsByDeptId(long.Parse(deptId));
                var detail = _indicatorsDetailRepository.GetAll().Where(v => empIds.Contains(v.EmployeeId));
                if (detail.Count() == 0)
                {
                    return new PagedResultDto<IndicatorShowDto>(0, null);
                }
                var indicator = _entityRepository.GetAll();
                var docInfo = _documentRepository.GetAll().Select(v => new { v.Id, v.Name });
                var query = from d in detail
                            join i in indicator on d.IndicatorsId equals i.Id
                            join doc in docInfo on i.SourceDocId equals doc.Id
                            select new IndicatorShowDto()
                            {
                                Id = i.Id,
                                CreationTime = i.CreationTime,
                                CreatorEmpName = i.CreatorEmpName,
                                Title = i.Title,
                                Paraphrase = i.Paraphrase,
                                MeasuringWay = i.MeasuringWay,
                                ExpectedValue = i.ExpectedValue,
                                CycleTimeName = i.CycleTime.ToString(),
                                DeptName = d.DeptName,
                                Status = d.Status,
                                CreatorDeptName = i.CreatorDeptName,
                                IndicatorDetailId = d.Id,
                                AchieveType = i.AchieveType,
                                SourceDocName = doc.Name
                            };
                var count = await query.CountAsync();
                var entityList = await query
                        .OrderBy(v => v.CycleTimeName).ThenByDescending(v => v.CreationTime).AsNoTracking()
                        .PageBy(input)
                        .ToListAsync();
                return new PagedResultDto<IndicatorShowDto>(count, entityList);
            }
            else
            {
                var detail = _indicatorsDetailRepository.GetAll().Where(v => v.EmployeeId == user.EmployeeId);
                if (detail.Count() == 0)
                {
                    return new PagedResultDto<IndicatorShowDto>(0, null);
                }
                var indicator = _entityRepository.GetAll();
                var docInfo = _documentRepository.GetAll().Select(v => new { v.Id, v.Name });
                var query = from d in detail
                            join i in indicator on d.IndicatorsId equals i.Id
                            join doc in docInfo on i.SourceDocId equals doc.Id
                            select new IndicatorShowDto()
                            {
                                Id = i.Id,
                                CreationTime = i.CreationTime,
                                CreatorEmpName = i.CreatorEmpName,
                                Title = i.Title,
                                Paraphrase = i.Paraphrase,
                                MeasuringWay = i.MeasuringWay,
                                ExpectedValue = i.ExpectedValue,
                                CycleTimeName = i.CycleTime.ToString(),
                                DeptName = d.DeptName,
                                Status = d.Status,
                                CreatorDeptName = i.CreatorDeptName,
                                IndicatorDetailId = d.Id,
                                AchieveType = i.AchieveType,
                                SourceDocName = doc.Name
                            };
                var count = await query.CountAsync();
                var entityList = await query
                        .OrderBy(v => v.CycleTimeName).ThenByDescending(v => v.CreationTime).AsNoTracking()
                        .PageBy(input)
                        .ToListAsync();
                return new PagedResultDto<IndicatorShowDto>(count, entityList);
            }
        }

        /// <summary>
        /// 获取指标详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IndicatorShowDto> GetIndicatorDetailByIdAsync(EntityDto<Guid> input)
        {
            var detail = _indicatorsDetailRepository.GetAll().Where(v => v.Id == input.Id);
            var indicator = _entityRepository.GetAll();
            var docInfo = _documentRepository.GetAll().Select(v => new { v.Id, v.Name });
            //var result = from i in indicator
            //            join d in detail on i.Id equals d.IndicatorsId into g
            //            from table in g.DefaultIfEmpty()
            //            select new IndicatorShowDto()
            //            {
            //                Id = i.Id,
            //                Title = i.Title,
            //                Paraphrase = i.Paraphrase,
            //                MeasuringWay = i.MeasuringWay,
            //                ExpectedValue = i.ExpectedValue,
            //                CycleTimeName = i.CycleTime.ToString(),
            //                Status = table.Status,
            //                CreatorDeptName = i.CreatorDeptName,
            //                IndicatorDetailId = table.Id,
            //                ActualValue = table.ActualValue
            //            };
            var result = from d in detail
                         join i in indicator on d.IndicatorsId equals i.Id
                         join doc in docInfo on i.SourceDocId equals doc.Id
                         select new IndicatorShowDto()
                         {
                             Id = i.Id,
                             Title = i.Title,
                             Paraphrase = i.Paraphrase,
                             MeasuringWay = i.MeasuringWay,
                             ExpectedValue = i.ExpectedValue,
                             CycleTimeName = i.CycleTime.ToString(),
                             Status = d.Status,
                             CreatorDeptName = i.CreatorDeptName,
                             IndicatorDetailId = d.Id,
                             ActualValue = d.ActualValue,
                             AchieveType = i.AchieveType,
                             SourceDocName = doc.Name
                         };

            return await result.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据id获取指标考核记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<IndicatorReviewDto>> GetPagedIndicatorRecordByIdAsync(GetIndicatorsInput input)
        {
            var detail = _indicatorsDetailRepository.GetAll().Where(v => v.IndicatorsId == input.Id);
            var indicator = _entityRepository.GetAll();
            var result = from i in indicator
                         join d in detail on i.Id equals d.IndicatorsId
                         select new IndicatorReviewDto()
                         {
                             Id = i.Id,
                             ExpectedValue = i.ExpectedValue,
                             Status = d.Status,
                             EmployeeName = d.EmployeeName,
                             CompleteTime = d.CompleteTime,
                             IndicatorDetailId = d.Id,
                             ActualValue = d.ActualValue,
                             EmployeeDeptName = d.DeptName,
                             AchieveType = i.AchieveType,
                             PublishTime = i.CreationTime,
                             EndTime = d.EndTime
                         };
            var count = await result.CountAsync();
            var entityList = await result
              .OrderBy(v => v.Status).ThenBy(v => v.EndTime)
                    .PageBy(input)
                    .ToListAsync();
            return new PagedResultDto<IndicatorReviewDto>(count, entityList);
        }

        /// <summary>
        /// 查询指标所属部门指标列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<IndicatorReviewDto>> GetDeptIndicatorDetailByIdAsync(EntityDto<Guid> input)
        {
            var detail = _indicatorsDetailRepository.GetAll().Where(v => v.IndicatorsId == input.Id);
            var indicator = _entityRepository.GetAll();
            //var result = from i in indicator
            //             join d in detail on i.Id equals d.IndicatorsId into g
            //             from table in g.DefaultIfEmpty()
            //             select new IndicatorReviewDto()
            //             {
            //                 Id = i.Id,
            //                 //Title = i.Title,
            //                 //Paraphrase = i.Paraphrase,
            //                 //MeasuringWay = i.MeasuringWay,
            //                 //ExpectedValue = i.ExpectedValue,
            //                 //Status = table.Status,
            //                 EmployeeName = table.EmployeeName,
            //                 //CompleteTime = table.CompleteTime,
            //                 //IndicatorDetailId = table.Id,
            //                 //ActualValue = table.ActualValue,
            //                 EmployeeDeptName = table.DeptName,
            //             };
            var result = from i in indicator
                         join d in detail on i.Id equals d.IndicatorsId
                         select new IndicatorReviewDto()
                         {
                             Id = i.Id,
                             //Title = i.Title,
                             //Paraphrase = i.Paraphrase,
                             //MeasuringWay = i.MeasuringWay,
                             ExpectedValue = i.ExpectedValue,
                             Status = d.Status,
                             EmployeeName = d.EmployeeName,
                             CompleteTime = d.CompleteTime,
                             IndicatorDetailId = d.Id,
                             ActualValue = d.ActualValue,
                             EmployeeDeptName = d.DeptName,
                             AchieveType = i.AchieveType
                         };

            return await result.OrderByDescending(v => v.Status).ThenByDescending(v => v.CompleteTime).ToListAsync();
        }

        /// <summary>
        /// 自动更新逾期状态
        /// </summary>
        [AbpAllowAnonymous]
        public async Task AutoUpdateIndicatorStatusAsync()
        {
            DateTime curTime = DateTime.Today.AddDays(1);
            Logger.InfoFormat(curTime.ToString());
            //var indicator = _entityRepository.GetAll().Where(v => v.EndTime < curTime);
            //var detail = _indicatorsDetailRepository.GetAll().Where(v => v.Status == GYEnums.IndicatorStatus.未填写);
            //var query = from d in detail
            //            join i in indicator on d.IndicatorsId equals i.Id
            //            select d;
            //var overdueList = await query.ToListAsync();
            //foreach (var item in overdueList)
            //{
            //    item.Status = GYEnums.IndicatorStatus.已逾期;
            //}
            var detail = _indicatorsDetailRepository.GetAll().Where(v => v.Status == GYEnums.IndicatorStatus.未填写 && v.EndTime < curTime);
            var overdueList = await detail.ToListAsync();
            foreach (var item in overdueList)
            {
                item.Status = GYEnums.IndicatorStatus.已逾期;
            }
        }

        /// <summary>
        /// 获取基层单位标准化管理员
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        private async Task<ExamineUser> GetCountryStandardAdminByIdAsync(long deptId)
        {
            var adminList = await GetUsersInRoleAsync("CountyAdmin");//查询所有标准化管理员
            string[] adminIds = adminList.Select(v => v.EmployeeId).ToArray();
            string[] deptUserIds = await GetEmployeeIdsByDeptId(deptId);
            string id = deptUserIds.Where(v => adminIds.Contains(v)).FirstOrDefault();
            var userInfo = await _employeeRepository.GetAll().Where(v => v.Id == id).Select(v => new ExamineUser { Id = v.Id, Name = v.Name }).FirstOrDefaultAsync();
            return userInfo;
        }

        /// <summary>
        /// 获取部门及下级员工id
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        private async Task<string[]> GetEmployeeIdsByDeptId(long deptId)
        {
            var childrenDeptIds = await _employeeManager.GetDeptIdArrayAsync(deptId);
            var query = _employeeRepository.GetAll().Where(e => childrenDeptIds.Any(c => e.Department.Contains(c))).Select(e => e.Id);
            return await query.ToArrayAsync();
        }

        /// <summary>
        /// 修改指标启用状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ChangeActionStatusAsync(ChangeStatusDto input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);
            entity.IsAction = input.IsAction;
            return new APIResultDto() { Code = 0, Msg = "保存成功" };
        }
    }
}