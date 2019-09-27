
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


using GYSWP.IndicatorsDetails;
using GYSWP.IndicatorsDetails.Dtos;
using GYSWP.IndicatorsDetails.DomainService;
using GYSWP.Dtos;
using GYSWP.DingDingApproval;
using GYSWP.Organizations;
using GYSWP.Employees;

namespace GYSWP.IndicatorsDetails
{
    /// <summary>
    /// IndicatorsDetail应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class IndicatorsDetailAppService : GYSWPAppServiceBase, IIndicatorsDetailAppService
    {
        private readonly IRepository<IndicatorsDetail, Guid> _entityRepository;
        private readonly IApprovalAppService _approvalAppService;
        private readonly IIndicatorsDetailManager _entityManager;
        private readonly IRepository<Organization, long> _organizationRepository;
        private readonly IRepository<Employee, string> _employeeRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public IndicatorsDetailAppService(
        IRepository<IndicatorsDetail, Guid> entityRepository
        , IIndicatorsDetailManager entityManager
        , IApprovalAppService approvalAppService
        , IRepository<Organization, long> organizationRepository
        , IRepository<Employee, string> employeeRepository
        )
        {
            _employeeRepository = employeeRepository;
            _entityRepository = entityRepository;
            _approvalAppService = approvalAppService;
            _entityManager = entityManager;
            _organizationRepository = organizationRepository;
        }


        /// <summary>
        /// 获取IndicatorsDetail的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<IndicatorsDetailListDto>> GetPaged(GetIndicatorsDetailsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<IndicatorsDetailListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<IndicatorsDetailListDto>>();

            return new PagedResultDto<IndicatorsDetailListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取IndicatorsDetailListDto信息
        /// </summary>

        public async Task<IndicatorsDetailListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<IndicatorsDetailListDto>();
        }

        /// <summary>
        /// 获取编辑 IndicatorsDetail
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetIndicatorsDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetIndicatorsDetailForEditOutput();
            IndicatorsDetailEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<IndicatorsDetailEditDto>();

                //indicatorsDetailEditDto = ObjectMapper.Map<List<indicatorsDetailEditDto>>(entity);
            }
            else
            {
                editDto = new IndicatorsDetailEditDto();
            }

            output.IndicatorsDetail = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改IndicatorsDetail的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateIndicatorsDetailInput input)
        {

            if (input.IndicatorsDetail.Id.HasValue)
            {
                await Update(input.IndicatorsDetail);
            }
            else
            {
                await Create(input.IndicatorsDetail);
            }
        }


        /// <summary>
        /// 新增IndicatorsDetail
        /// </summary>

        protected virtual async Task<IndicatorsDetailEditDto> Create(IndicatorsDetailEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <IndicatorsDetail>(input);
            var entity = input.MapTo<IndicatorsDetail>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<IndicatorsDetailEditDto>();
        }

        /// <summary>
        /// 编辑IndicatorsDetail
        /// </summary>

        protected virtual async Task Update(IndicatorsDetailEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除IndicatorsDetail信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除IndicatorsDetail的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 填写考核指标
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ChangeStatusByIdAsync(IndicatorsDetailUpDateDto input)
        {
            var entity = await _entityRepository.GetAsync(input.Id.Value);
            entity.ActualValue = input.ActualValue;
            entity.Status = input.Status;
            entity.CompleteTime = DateTime.Now;
            await _entityRepository.UpdateAsync(entity);
            //合格or不合格发送消息通知
            if (input.Status != GYEnums.IndicatorStatus.未填写)
            {
                string infoEmpIds = entity.EmployeeId;
                //基层单位附带通知局长、主任
                if (entity.DeptId != 1 && entity.DeptId != 59549057 && entity.DeptId != 59646091 && entity.DeptId != 59591062
                    && entity.DeptId != 59552081 && entity.DeptId != 59632058 && entity.DeptId != 59571109 && entity.DeptId != 59584063
                    && entity.DeptId != 59644078 && entity.DeptId != 59620071 && entity.DeptId != 59628060 && entity.DeptId != 59538081 
                    && entity.DeptId != 59490590 && entity.DeptId != 59481641 && entity.DeptId != 59534185 && entity.DeptId != 59534184 
                    && entity.DeptId != 59534183)
                {
                    var orgInfo = await _organizationRepository.GetAll().Where(v => v.Id == entity.DeptId).Select(v => new { v.Id, v.ParentId }).FirstOrDefaultAsync();
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
                    string deptBossId = await _employeeRepository.GetAll().Where(v => v.Department == "[" + id + "]" && (v.Position.Contains("县区局（分公司）局长") || v.Position.Contains("物流中心主任"))).Select(v => v.Id).FirstOrDefaultAsync();
                    if (!string.IsNullOrEmpty(deptBossId))
                    {
                        infoEmpIds = infoEmpIds + "," + deptBossId;
                    }
                }
                _approvalAppService.SendIndicatorResultAsync(input.Status, infoEmpIds);
            }
            return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
        }
        
        /// <summary>
        /// 领导推送测试接口
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        //[AbpAllowAnonymous]
        //public async Task<string> TestInfo(long pid)
        //{
        //    var entity = new IndicatorsDetail();
        //    entity.EmployeeId = "0216056802753589";
        //    entity.DeptId = pid;
        //    string infoEmpIds = entity.EmployeeId;
        //    //基层单位附带通知局长、主任
        //    if (entity.DeptId != 1 && entity.DeptId != 59549057 && entity.DeptId != 59646091 && entity.DeptId != 59591062
        //        && entity.DeptId != 59552081 && entity.DeptId != 59632058 && entity.DeptId != 59571109 && entity.DeptId != 59584063
        //        && entity.DeptId != 59644078 && entity.DeptId != 59620071 && entity.DeptId != 59628060 && entity.DeptId != 59538081
        //        && entity.DeptId != 59490590 && entity.DeptId != 59481641 && entity.DeptId != 59534185 && entity.DeptId != 59534184
        //        && entity.DeptId != 59534183)
        //    {
        //        var orgInfo = await _organizationRepository.GetAll().Where(v => v.Id == entity.DeptId).Select(v => new { v.Id, v.ParentId }).FirstOrDefaultAsync();
        //        long? id;
        //        if (orgInfo.ParentId != 1)
        //        {
        //            long? resultId = orgInfo.ParentId;
        //            id = GetTopDeptId(orgInfo.ParentId, ref resultId);
        //        }
        //        else
        //        {
        //            id = orgInfo.Id;
        //        }
        //        string deptBossId = await _employeeRepository.GetAll().Where(v => v.Department == "[" + id + "]" && (v.Position.Contains("县区局（分公司）局长") || v.Position.Contains("物流中心主任"))).Select(v => v.Id).FirstOrDefaultAsync();
        //        if (!string.IsNullOrEmpty(deptBossId))
        //        {
        //            infoEmpIds = infoEmpIds + "," + deptBossId;
        //        }
        //    }
        //    return infoEmpIds;
        //}

        /// <summary>
        /// 查询顶级部门Id
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        private long? GetTopDeptId(long? pId, ref long? resultId)
        {
            var result = _organizationRepository.GetAll().Where(v => v.Id == pId).Select(v => new { v.ParentId, v.Id }).FirstOrDefault();
            resultId = result.Id;
            if (result.ParentId != 1)
            {
                GetTopDeptId(result.ParentId, ref resultId);
            }
            return resultId;
        }
    }
}


