
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


using GYSWP.CriterionExamines;
using GYSWP.CriterionExamines.Dtos;
using GYSWP.CriterionExamines.DomainService;
using GYSWP.Employees;
using GYSWP.Organizations;
using GYSWP.ExamineDetails;
using GYSWP.EmployeeClauses;
using GYSWP.Dtos;

namespace GYSWP.CriterionExamines
{
    /// <summary>
    /// CriterionExamine应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class CriterionExamineAppService : GYSWPAppServiceBase, ICriterionExamineAppService
    {
        private readonly IRepository<CriterionExamine, Guid> _entityRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly ICriterionExamineManager _entityManager;
        private readonly IRepository<Organization, long> _organizationRepository;
        private readonly IRepository<ExamineDetail, Guid> _examineDetailRepository;
        private readonly IRepository<EmployeeClause, Guid> _employeeClauseRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public CriterionExamineAppService(
        IRepository<CriterionExamine, Guid> entityRepository
        , ICriterionExamineManager entityManager
        , IRepository<Employee, string> employeeRepository
        , IRepository<Organization, long> organizationRepository
        , IRepository<ExamineDetail, Guid> examineDetailRepository
        , IRepository<EmployeeClause, Guid> employeeClauseRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _examineDetailRepository = examineDetailRepository;
            _employeeClauseRepository = employeeClauseRepository;
        }


        /// <summary>
        /// 获取CriterionExamine的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<CriterionExamineListDto>> GetPaged(GetCriterionExaminesInput input)
        {

            var query = _entityRepository.GetAll().Where(v => v.DeptId == input.DeptId);
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<CriterionExamineListDto>>();
            return new PagedResultDto<CriterionExamineListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取CriterionExamineListDto信息
        /// </summary>

        public async Task<CriterionExamineListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<CriterionExamineListDto>();
        }

        /// <summary>
        /// 获取编辑 CriterionExamine
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetCriterionExamineForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetCriterionExamineForEditOutput();
            CriterionExamineEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<CriterionExamineEditDto>();

                //criterionExamineEditDto = ObjectMapper.Map<List<criterionExamineEditDto>>(entity);
            }
            else
            {
                editDto = new CriterionExamineEditDto();
            }

            output.CriterionExamine = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改CriterionExamine的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateCriterionExamineInput input)
        {

            if (input.CriterionExamine.Id.HasValue)
            {
                await Update(input.CriterionExamine);
            }
            else
            {
                await Create(input.CriterionExamine);
            }
        }


        /// <summary>
        /// 新增CriterionExamine
        /// </summary>

        protected virtual async Task<CriterionExamineEditDto> Create(CriterionExamineEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <CriterionExamine>(input);
            var entity = input.MapTo<CriterionExamine>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<CriterionExamineEditDto>();
        }

        /// <summary>
        /// 编辑CriterionExamine
        /// </summary>

        protected virtual async Task Update(CriterionExamineEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除CriterionExamine信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除CriterionExamine的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 生成考核表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> CreateExamineAsync(CriterionExamineInfoDto input)
        {
            try
            {
                //生成考核基本信息
                var user = await GetCurrentUserAsync();
                string deptId = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
                var organization = await _organizationRepository.GetAll().Where(v => "[" + v.Id + "]" == deptId).Select(v => new { v.Id, v.DepartmentName }).FirstOrDefaultAsync();
                CriterionExamine entity = new CriterionExamine();
                entity.Type = input.Type;
                entity.DeptId = input.DeptId;
                entity.DeptName = input.DeptName;
                entity.CreatorEmpeeId = user.EmployeeId;
                entity.CreatorEmpName = user.EmployeeName;
                entity.CreatorDeptId = organization.Id;
                entity.CreatorDeptName = organization.DepartmentName;
                if (input.Type == GYEnums.CriterionExamineType.内部考核)
                {
                    entity.Title = input.DeptName + input.Type.ToString();
                }
                else
                {
                    entity.Title = organization.DepartmentName + input.Type.ToString();
                }
                Guid exaId = await _entityRepository.InsertAndGetIdAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();
                //生成考核详情 每种标准平均按比例抽取（20%）
                //if (input.EmpInfo.Count == 0) //默认全体人员
                //{
                //    input.EmpInfo = await _employeeRepository.GetAll().Where(v => v.Department == "[" + input.DeptId + "]").Select(v => new EmpInfo { EmpId = v.Id, EmpName = v.Name }).ToListAsync();
                //}
                foreach (var emp in input.EmpInfo)
                {
                    var empClauseGroupList = await _employeeClauseRepository.GetAll().Where(v => v.EmployeeId == emp.EmpId).GroupBy(v => new { v.DocumentId })
                        .Select(v => new { DocId = v.Key.DocumentId, Count = v.Count() }).ToListAsync();
                    if (empClauseGroupList.Count > 0)
                    {
                        foreach (var groupInfo in empClauseGroupList)
                        {
                            if (groupInfo.Count > 0)
                            {
                                int random = (int)Math.Ceiling(groupInfo.Count * 0.2);
                                var empClauseList = await _employeeClauseRepository.GetAll().Where(v => v.EmployeeId == emp.EmpId && v.DocumentId == groupInfo.DocId).OrderBy(v => Guid.NewGuid()).Take(random).ToListAsync();
                                foreach (var item in empClauseList)
                                {
                                    ExamineDetail edEntity = new ExamineDetail();
                                    edEntity.ClauseId = item.ClauseId;
                                    edEntity.DocumentId = item.DocumentId;
                                    edEntity.CreatorEmpeeId = user.EmployeeId;
                                    edEntity.CreatorEmpName = user.EmployeeName;
                                    edEntity.EmployeeId = emp.EmpId;
                                    edEntity.EmployeeName = emp.EmpName;
                                    edEntity.CriterionExamineId = exaId;
                                    edEntity.Result = GYEnums.ExamineStatus.未检查;
                                    edEntity.Status = GYEnums.ResultStatus.未开始;
                                    await _examineDetailRepository.InsertAsync(edEntity);
                                }
                            }
                        }
                    }
                }
                return new APIResultDto() { Code = 0, Msg = "考核表创建成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateExamineAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "考核表创建失败" };
            }
        }

        /// <summary>
        /// 获取用户考核信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CriterionExamineListDto>> GetPagedExamineByCurrentIdAsync(GetCriterionExaminesInput input)
        {
            var user = await GetCurrentUserAsync();
            Guid[] examineIds = await _examineDetailRepository.GetAll().Where(v => v.EmployeeId == user.EmployeeId).GroupBy(v => new { v.CriterionExamineId }).Select(v => v.Key.CriterionExamineId).ToArrayAsync();
            var query = _entityRepository.GetAll().Where(v => examineIds.Contains(v.Id));
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<CriterionExamineListDto>>();
            return new PagedResultDto<CriterionExamineListDto>(count, entityListDtos);
        }
    }
}