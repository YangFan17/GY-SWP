
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
using GYSWP.Employees.DomainService;
using GYSWP.Documents;
using Abp.Auditing;

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
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IEmployeeManager _employeeManager;

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
        , IEmployeeManager employeeManager
        , IRepository<Document, Guid> documentRepository
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _examineDetailRepository = examineDetailRepository;
            _employeeClauseRepository = employeeClauseRepository;
            _employeeManager = employeeManager;
            _documentRepository = documentRepository;
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
        [AbpAllowAnonymous]
        [Audited]
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
        /// 生成部门内部考核表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> CreateInternalExamineAsync(CriterionExamineInfoDto input)
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
                if (input.Type == GYEnums.CriterionExamineType.外部考核) //默认全体人员
                {
                    input.EmpInfo = await _employeeRepository.GetAll().Where(v => v.Department == "[" + input.DeptId + "]").Select(v => new EmpInfo { EmpId = v.Id, EmpName = v.Name }).ToListAsync();
                }
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
        /// 企管科考核(县局单位，机关单位)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> CreateExamineByQiGuanAsync(CriterionExamineInfoDto input)
        {
            try
            {
                //生成考核基本信息
                #region 考核基本信息
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
                if (input.Type == GYEnums.CriterionExamineType.外部考核)
                {
                    entity.Title = input.DeptName + input.Type.ToString();
                }
                else
                {
                    entity.Title = organization.DepartmentName + input.Type.ToString();
                }
                Guid exaId = await _entityRepository.InsertAndGetIdAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();
                #endregion
                //按照区县生成不同考核规则信息
                #region 公共模块
                string[] employeeIds = await GetEmployeeIdsByDeptId(input.DeptId);
                var empClauseGroupList = await _employeeClauseRepository.GetAll().Where(v => employeeIds.Contains(v.EmployeeId)).GroupBy(v => v.DocumentId).Select(v => v.Key).ToListAsync();
                var doc = _documentRepository.GetAll();
                var org = _organizationRepository.GetAll();
                var docCategroyList = (from e in empClauseGroupList
                                       join d in doc on e equals d.Id
                                       join o in org on d.DeptIds equals o.Id.ToString()
                                       select new
                                       {
                                           e,
                                           DeptName = o.DepartmentName
                                       }).ToList();

                var YingXiaoList = docCategroyList.Where(d => d.DeptName == "市场营销科");//已确认的营销标准
                var ZhuanMaiList = docCategroyList.Where(d => d.DeptName == "专卖科");//已确认的专卖标准


                #endregion
                //纯销区
                #region 纯销区考核
                if (input.DeptId == 59549059 || input.DeptId == 59584066 || input.DeptId == 59587088 || input.DeptId == 59634065)
                {
                    var OtherList = docCategroyList.Where(d => d.DeptName != "专卖科" && d.DeptName != "市场营销科");//已确认的其他标准
                    int total = await _employeeClauseRepository.CountAsync(v => employeeIds.Contains(v.EmployeeId));
                    if (total > 0)
                    {
                        int random = (int)Math.Ceiling(total * 0.2);//计算抽查总数（总和的20%)
                        int YingXiao = (int)Math.Ceiling(random * 0.4);//营销标准40%
                        //如果预期计算结果大于实际数量，取实际结果
                        if (YingXiao > YingXiaoList.Count())
                        {
                            YingXiao = YingXiaoList.Count();
                        }
                        int ZhuanMai = (int)Math.Ceiling(random * 0.4);//专卖标准40%
                        if (ZhuanMai > ZhuanMaiList.Count())
                        {
                            ZhuanMai = ZhuanMaiList.Count();
                        }
                        int Other = random - YingXiao - ZhuanMai;//其他20%
                        if (Other > OtherList.Count())
                        {
                            Other = OtherList.Count();
                        }
                        var YingXiaoClauseList = await _employeeClauseRepository.GetAll().Where(v => YingXiaoList.Select(y => y.e).Contains(v.DocumentId)).OrderBy(v => Guid.NewGuid()).Take(YingXiao).ToListAsync();
                        var ZhuanMaiClauseList = await _employeeClauseRepository.GetAll().Where(v => ZhuanMaiList.Select(z => z.e).Contains(v.DocumentId)).OrderBy(v => Guid.NewGuid()).Take(ZhuanMai).ToListAsync();
                        var OtherClauseList = await _employeeClauseRepository.GetAll().Where(v => OtherList.Select(o => o.e).Contains(v.DocumentId)).OrderBy(v => Guid.NewGuid()).Take(Other).ToListAsync();
                        long empDeptId = await _organizationRepository.GetAll().Where(v => v.ParentId == input.DeptId && (v.DepartmentName == "综合科" || v.DepartmentName == "综合办")).Select(v => v.Id).FirstOrDefaultAsync();
                        var examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + empDeptId + "]" && v.Position == "科长").Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
                        foreach (var item in YingXiaoClauseList)
                        {
                            ExamineDetail edEntity = new ExamineDetail();
                            edEntity.ClauseId = item.ClauseId;
                            edEntity.DocumentId = item.DocumentId;
                            edEntity.CreatorEmpeeId = user.EmployeeId;
                            edEntity.CreatorEmpName = user.EmployeeName;
                            edEntity.EmployeeId = examEmp.Id;
                            edEntity.EmployeeName = examEmp.Name;
                            edEntity.CriterionExamineId = exaId;
                            edEntity.Result = GYEnums.ExamineStatus.未检查;
                            edEntity.Status = GYEnums.ResultStatus.未开始;
                            await _examineDetailRepository.InsertAsync(edEntity);
                        }
                        foreach (var item in ZhuanMaiClauseList)
                        {
                            ExamineDetail edEntity = new ExamineDetail();
                            edEntity.ClauseId = item.ClauseId;
                            edEntity.DocumentId = item.DocumentId;
                            edEntity.CreatorEmpeeId = user.EmployeeId;
                            edEntity.CreatorEmpName = user.EmployeeName;
                            edEntity.EmployeeId = examEmp.Id;
                            edEntity.EmployeeName = examEmp.Name;
                            edEntity.CriterionExamineId = exaId;
                            edEntity.Result = GYEnums.ExamineStatus.未检查;
                            edEntity.Status = GYEnums.ResultStatus.未开始;
                            await _examineDetailRepository.InsertAsync(edEntity);
                        }
                        foreach (var item in OtherClauseList)
                        {
                            ExamineDetail edEntity = new ExamineDetail();
                            edEntity.ClauseId = item.ClauseId;
                            edEntity.DocumentId = item.DocumentId;
                            edEntity.CreatorEmpeeId = user.EmployeeId;
                            edEntity.CreatorEmpName = user.EmployeeName;
                            edEntity.EmployeeId = examEmp.Id;
                            edEntity.EmployeeName = examEmp.Name;
                            edEntity.CriterionExamineId = exaId;
                            edEntity.Result = GYEnums.ExamineStatus.未检查;
                            edEntity.Status = GYEnums.ResultStatus.未开始;
                            await _examineDetailRepository.InsertAsync(edEntity);
                        }
                    }
                }
                #endregion
                //烟产区
                #region 烟产区考核
                else if (input.DeptId == 59569075 || input.DeptId == 59594070 || input.DeptId == 59617065)
                {
                    var YanYeList = docCategroyList.Where(y => y.DeptName == "烟叶生产科" || y.DeptName == "烟叶生产管理科"
                    || y.DeptName == "王家站" || y.DeptName == "剑门烟叶站" || y.DeptName == "普安烟叶站" || y.DeptName == "武连烟叶站"
                    || y.DeptName == "枣林烟叶收购点" || y.DeptName == "檬子烟叶收购点" || y.DeptName == "双汇烟叶收购点");//已确认的烟叶标准
                    var OtherList = docCategroyList.Where(d => d.DeptName != "专卖科" && d.DeptName != "市场营销科"
                    && d.DeptName != "烟叶生产科" && d.DeptName != "烟叶生产管理科"
                    && d.DeptName != "王家站" && d.DeptName != "剑门烟叶站" && d.DeptName != "普安烟叶站" && d.DeptName != "武连烟叶站"
                    && d.DeptName != "枣林烟叶收购点" && d.DeptName != "檬子烟叶收购点" && d.DeptName != "双汇烟叶收购点"
                    );//已确认的其他标准
                    int total = await _employeeClauseRepository.CountAsync(v => employeeIds.Contains(v.EmployeeId));
                    if (total > 0)
                    {
                        int random = (int)Math.Ceiling(total * 0.2);//计算抽查总数（总和的20%)
                        int YingXiao = (int)Math.Ceiling(random * 0.3);//营销标准30%
                        //如果预期计算结果大于实际数量，取实际结果
                        if (YingXiao > YingXiaoList.Count())
                        {
                            YingXiao = YingXiaoList.Count();
                        }
                        int ZhuanMai = (int)Math.Ceiling(random * 0.3);//专卖标准30%
                        if (ZhuanMai > ZhuanMaiList.Count())
                        {
                            ZhuanMai = ZhuanMaiList.Count();
                        }
                        int Yanye = (int)Math.Ceiling(random * 0.3);//烟叶标准30%
                        if (Yanye > YanYeList.Count())
                        {
                            Yanye = YanYeList.Count();
                        }
                        int Other = random - YingXiao - ZhuanMai;//其他10%
                        if (Other > OtherList.Count())
                        {
                            Other = OtherList.Count();
                        }
                        var YingXiaoClauseList = await _employeeClauseRepository.GetAll().Where(v => YingXiaoList.Select(y => y.e).Contains(v.DocumentId)).OrderBy(v => Guid.NewGuid()).Take(YingXiao).ToListAsync();
                        var ZhuanMaiClauseList = await _employeeClauseRepository.GetAll().Where(v => ZhuanMaiList.Select(z => z.e).Contains(v.DocumentId)).OrderBy(v => Guid.NewGuid()).Take(ZhuanMai).ToListAsync();
                        var YanYeClauseList = await _employeeClauseRepository.GetAll().Where(v => YanYeList.Select(z => z.e).Contains(v.DocumentId)).OrderBy(v => Guid.NewGuid()).Take(Yanye).ToListAsync();
                        var OtherClauseList = await _employeeClauseRepository.GetAll().Where(v => OtherList.Select(o => o.e).Contains(v.DocumentId)).OrderBy(v => Guid.NewGuid()).Take(Other).ToListAsync();
                        long empDeptId = await _organizationRepository.GetAll().Where(v => v.ParentId == input.DeptId && (v.DepartmentName == "综合科" || v.DepartmentName == "综合办")).Select(v => v.Id).FirstOrDefaultAsync();
                        var examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[" + empDeptId + "]" && v.Position == "科长").Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
                        foreach (var item in YingXiaoClauseList)
                        {
                            ExamineDetail edEntity = new ExamineDetail();
                            edEntity.ClauseId = item.ClauseId;
                            edEntity.DocumentId = item.DocumentId;
                            edEntity.CreatorEmpeeId = user.EmployeeId;
                            edEntity.CreatorEmpName = user.EmployeeName;
                            edEntity.EmployeeId = examEmp.Id;
                            edEntity.EmployeeName = examEmp.Name;
                            edEntity.CriterionExamineId = exaId;
                            edEntity.Result = GYEnums.ExamineStatus.未检查;
                            edEntity.Status = GYEnums.ResultStatus.未开始;
                            await _examineDetailRepository.InsertAsync(edEntity);
                        }
                        foreach (var item in ZhuanMaiClauseList)
                        {
                            ExamineDetail edEntity = new ExamineDetail();
                            edEntity.ClauseId = item.ClauseId;
                            edEntity.DocumentId = item.DocumentId;
                            edEntity.CreatorEmpeeId = user.EmployeeId;
                            edEntity.CreatorEmpName = user.EmployeeName;
                            edEntity.EmployeeId = examEmp.Id;
                            edEntity.EmployeeName = examEmp.Name;
                            edEntity.CriterionExamineId = exaId;
                            edEntity.Result = GYEnums.ExamineStatus.未检查;
                            edEntity.Status = GYEnums.ResultStatus.未开始;
                            await _examineDetailRepository.InsertAsync(edEntity);
                        }
                        foreach (var item in YanYeClauseList)
                        {
                            ExamineDetail edEntity = new ExamineDetail();
                            edEntity.ClauseId = item.ClauseId;
                            edEntity.DocumentId = item.DocumentId;
                            edEntity.CreatorEmpeeId = user.EmployeeId;
                            edEntity.CreatorEmpName = user.EmployeeName;
                            edEntity.EmployeeId = examEmp.Id;
                            edEntity.EmployeeName = examEmp.Name;
                            edEntity.CriterionExamineId = exaId;
                            edEntity.Result = GYEnums.ExamineStatus.未检查;
                            edEntity.Status = GYEnums.ResultStatus.未开始;
                            await _examineDetailRepository.InsertAsync(edEntity);
                        }
                        foreach (var item in OtherClauseList)
                        {
                            ExamineDetail edEntity = new ExamineDetail();
                            edEntity.ClauseId = item.ClauseId;
                            edEntity.DocumentId = item.DocumentId;
                            edEntity.CreatorEmpeeId = user.EmployeeId;
                            edEntity.CreatorEmpName = user.EmployeeName;
                            edEntity.EmployeeId = examEmp.Id;
                            edEntity.EmployeeName = examEmp.Name;
                            edEntity.CriterionExamineId = exaId;
                            edEntity.Result = GYEnums.ExamineStatus.未检查;
                            edEntity.Status = GYEnums.ResultStatus.未开始;
                            await _examineDetailRepository.InsertAsync(edEntity);
                        }
                    }
                }
                #endregion
                #region 物流中心考核
                else if(input.DeptId == 59593071) //物流中心
                {
                    int total = await _employeeClauseRepository.CountAsync(v => employeeIds.Contains(v.EmployeeId));
                    int random = (int)Math.Ceiling(total * 0.2);
                    var examEmp = await _employeeRepository.GetAll().Where(v => v.Department == "[60007074]" && v.Position == "部长").Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
                    var empClauseList = await _employeeClauseRepository.GetAll().Where(v => employeeIds.Contains(v.EmployeeId)).OrderBy(v => Guid.NewGuid()).Take(random).ToListAsync();
                    foreach (var item in empClauseList)
                    {
                        ExamineDetail edEntity = new ExamineDetail();
                        edEntity.ClauseId = item.ClauseId;
                        edEntity.DocumentId = item.DocumentId;
                        edEntity.CreatorEmpeeId = user.EmployeeId;
                        edEntity.CreatorEmpName = user.EmployeeName;
                        edEntity.EmployeeId = examEmp.Id;
                        edEntity.EmployeeName = examEmp.Name;
                        edEntity.CriterionExamineId = exaId;
                        edEntity.Result = GYEnums.ExamineStatus.未检查;
                        edEntity.Status = GYEnums.ResultStatus.未开始;
                        await _examineDetailRepository.InsertAsync(edEntity);
                    }
                }
                #endregion
                else //机关部门
                {
                    int total = await _employeeClauseRepository.CountAsync(v => employeeIds.Contains(v.EmployeeId));
                    int random = (int)Math.Ceiling(total * 0.2);
                    var adminList = await GetUsersInRoleAsync("StandardAdmin");
                    string[] adminIds = adminList.Select(v => v.EmployeeId).ToArray();
                    var examEmp = await _employeeRepository.GetAll().Where(v => adminIds.Contains(v.Id) && v.Department == "[" + input.DeptId + "]").Select(v => new { v.Id, v.Name }).FirstOrDefaultAsync();
                    var empClauseList = await _employeeClauseRepository.GetAll().Where(v => employeeIds.Contains(v.EmployeeId)).OrderBy(v => Guid.NewGuid()).Take(random).ToListAsync();
                    foreach (var item in empClauseList)
                    {
                        ExamineDetail edEntity = new ExamineDetail();
                        edEntity.ClauseId = item.ClauseId;
                        edEntity.DocumentId = item.DocumentId;
                        edEntity.CreatorEmpeeId = user.EmployeeId;
                        edEntity.CreatorEmpName = user.EmployeeName;
                        edEntity.EmployeeId = examEmp.Id;
                        edEntity.EmployeeName = examEmp.Name;
                        edEntity.CriterionExamineId = exaId;
                        edEntity.Result = GYEnums.ExamineStatus.未检查;
                        edEntity.Status = GYEnums.ResultStatus.未开始;
                        await _examineDetailRepository.InsertAsync(edEntity);
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

        /// <summary>
        /// 根据钉钉Id获取数据
        /// </summary>
        /// <param name="dingId"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task<List<CriterionExamineListDto>> GetPagedExamineByDingIdAsync(GetCriterionExaminesInput input)
        {
            Guid[] examineIds = await _examineDetailRepository.GetAll().Where(v => v.EmployeeId ==input.EmployeeId).GroupBy(v => new { v.CriterionExamineId }).Select(v => v.Key.CriterionExamineId).ToArrayAsync();
            var query = _entityRepository.GetAll().Where(v => examineIds.Contains(v.Id));
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .OrderByDescending(aa=>aa.CreationTime)
                    //.PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<CriterionExamineListDto>>();
            return entityListDtos;
        }
    }
}