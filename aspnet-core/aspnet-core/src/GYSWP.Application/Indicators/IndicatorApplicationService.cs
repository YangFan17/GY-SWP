
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

        /// <summary>
        /// 构造函数 
        ///</summary>
        public IndicatorAppService(
        IRepository<Indicator, Guid> entityRepository
        , IIndicatorManager entityManager
        , IRepository<IndicatorsDetail, Guid> indicatorsDetailRepository
        , IRepository<Organization, long> organizationRepository
        , IRepository<Employee, string> employeeRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _indicatorsDetailRepository = indicatorsDetailRepository;
            _organizationRepository = organizationRepository;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// 获取Indicator的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<IndicatorListDto>> GetPaged(GetIndicatorsInput input)
        {
            var user = await GetCurrentUserAsync();
            //var detail =  _indicatorsDetailRepository.GetAll();
            var query = _entityRepository.GetAll().Where(v => v.CreatorEmpeeId == user.EmployeeId);
            //var query = from i in indicator
            //            join d in detail on i.Id equals d.IndicatorsId into g
            //            from table in g.DefaultIfEmpty()
            //            select new IndicatorShowDto() {
            //                Id = i.Id,
            //                CreationTime = i.CreationTime,
            //                CreatorEmpName = i.CreatorEmpName,
            //                Title = i.Title,
            //                Paraphrase = i.Paraphrase,
            //                MeasuringWay = i.MeasuringWay,
            //                ExpectedValue = i.ExpectedValue,
            //                CycleTimeName = i.CycleTime.ToString(),
            //                DeptIds = i.DeptId,
            //                StatusName = table.Status.ToString(),
            //                ActualValue = table.ActualValue,
            //                CreatorDeptName = i.CreatorDeptName,
            //                DeptNames = i.DeptName
            //            };

            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(v => v.CycleTime).ThenByDescending(v => v.CreationTime).AsNoTracking()
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

            return entity.MapTo<IndicatorListDto>();
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
                var entity = await Create(input.Indicator);
                await CurrentUnitOfWork.SaveChangesAsync();
                var adminList = await GetUsersInRoleAsync("StandardAdmin");
                string[] adminIds = adminList.Select(v => v.EmployeeId).ToArray();
                foreach (var item in input.DeptInfo)
                {
                    var examEmp = new
                    {
                        Id = "",
                        Name = ""
                    };
                    //机关单位
                    if (item.DeptId == 59481641 || item.DeptId == 59490590 || item.DeptId == 59534183 || item.DeptId == 59534184 || item.DeptId == 59534185
                        || item.DeptId == 59538081 || item.DeptId == 59552081 || item.DeptId == 59571109 || item.DeptId == 59584063 || item.DeptId == 59591062
                        || item.DeptId == 59620071 || item.DeptId == 59628060 || item.DeptId == 59632058 || item.DeptId == 59644078 || item.DeptId == 59646091)
                    {
                        examEmp = await _employeeRepository.GetAll().Where(v => adminIds.Contains(v.Id) && v.Department == "[" + item.DeptId + "]").Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
                    }
                    else if (item.DeptId == 59593071)
                    {
                        examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[60007074]" && v.Position == "部长").Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
                    }
                    else
                    {
                        examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + item.DeptId + "]" && (v.Position == "主任" || v.Position == "科长")).Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
                    }
                    IndicatorsDetail detail = new IndicatorsDetail();
                    detail.IndicatorsId = entity.Id.Value;
                    detail.DeptId = item.DeptId;
                    detail.DeptName = item.DeptName;
                    detail.EmployeeId = examEmp.Id;
                    detail.EmployeeName = examEmp.Name;
                    detail.Status = GYEnums.IndicatorStatus.未填写;
                    await _indicatorsDetailRepository.InsertAsync(detail);
                }
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
            }
        }


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
            var detail = _indicatorsDetailRepository.GetAll().Where(v => v.EmployeeId == user.EmployeeId);
            var indicator = _entityRepository.GetAll();
            var query = from i in indicator
                        join d in detail on i.Id equals d.IndicatorsId into g
                        from table in g.DefaultIfEmpty()
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
                            DeptName = table.DeptName,
                            Status = table.Status,
                            CreatorDeptName = i.CreatorDeptName,
                            IndicatorDetailId = table.Id
                        };

            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(v => v.CycleTimeName).ThenByDescending(v => v.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            return new PagedResultDto<IndicatorShowDto>(count, entityList);
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
            var result = from i in indicator
                        join d in detail on i.Id equals d.IndicatorsId into g
                        from table in g.DefaultIfEmpty()
                        select new IndicatorShowDto()
                        {
                            Id = i.Id,
                            Title = i.Title,
                            Paraphrase = i.Paraphrase,
                            MeasuringWay = i.MeasuringWay,
                            ExpectedValue = i.ExpectedValue,
                            CycleTimeName = i.CycleTime.ToString(),
                            Status = table.Status,
                            CreatorDeptName = i.CreatorDeptName,
                            IndicatorDetailId = table.Id,
                            ActualValue = table.ActualValue
                        };

            return await result.FirstOrDefaultAsync();
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
            var result = from i in indicator
                         join d in detail on i.Id equals d.IndicatorsId into g
                         from table in g.DefaultIfEmpty()
                         select new IndicatorReviewDto()
                         {
                             Id = i.Id,
                             //Title = i.Title,
                             //Paraphrase = i.Paraphrase,
                             //MeasuringWay = i.MeasuringWay,
                             ExpectedValue = i.ExpectedValue,
                             Status = table.Status,
                             EmployeeName = table.EmployeeName,
                             CompleteTime = table.CompleteTime,
                             IndicatorDetailId = table.Id,
                             ActualValue = table.ActualValue,
                             EmployeeDeptName = table.DeptName,
                         };

            return await result.OrderByDescending(v=>v.Status).ThenByDescending(v=>v.CompleteTime).ToListAsync();
        }
    }
}