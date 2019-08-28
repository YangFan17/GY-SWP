
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


using GYSWP.Documents;
using GYSWP.Documents.Dtos;
using GYSWP.Documents.DomainService;
using GYSWP.Employees.Dtos;
using GYSWP.Employees;
using GYSWP.Organizations;
using GYSWP.Dtos;
using GYSWP.Categorys;
using System.IO;
using GYSWP.Clauses;
using GYSWP.EmployeeClauses;
using GYSWP.Clauses.Dtos;
using GYSWP.Categorys.DomainService;
using System.Text;
using GYSWP.DingDing;
using GYSWP.DingDing.Dtos;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.HttpUtility;
using GYSWP.Authorization.Users;
using Abp.Auditing;
using GYSWP.DocAttachments;

namespace GYSWP.Documents
{
    /// <summary>
    /// Document应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class DocumentAppService : GYSWPAppServiceBase, IDocumentAppService
    {
        private readonly IRepository<Document, Guid> _entityRepository;
        private readonly IRepository<Employee, string> _employeeRepository;
        private readonly IRepository<Organization, long> _organizationRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IDocumentManager _entityManager;
        private readonly IRepository<Clause, Guid> _clauseRepository;
        private readonly IRepository<EmployeeClause, Guid> _employeeClauseRepository;
        private readonly ICategoryManager _categoryManager;
        private readonly IDingDingAppService _dingDingAppService;
        private readonly UserManager _userManager;
        private readonly IRepository<DocAttachment, Guid> _docAttachmentRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public DocumentAppService(
        IRepository<Document, Guid> entityRepository
        , IRepository<Employee, string> employeeRepository
        , IRepository<Organization, long> organizationRepository
        , IRepository<Category> categoryRepository
        , IDocumentManager entityManager
        , IRepository<Clause, Guid> clauseRepository
        , IRepository<EmployeeClause, Guid> employeeClauseRepository
        , ICategoryManager categoryManager
        , IDingDingAppService dingDingAppService
        , UserManager userManager
        , IRepository<DocAttachment, Guid> docAttachmentRepository
        )
        {
            _userManager = userManager;
            _dingDingAppService = dingDingAppService;
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _categoryRepository = categoryRepository;
            _clauseRepository = clauseRepository;
            _employeeClauseRepository = employeeClauseRepository;
            _categoryManager = categoryManager;
            _docAttachmentRepository = docAttachmentRepository;
        }


        /// <summary>
        /// 获取Document的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<DocumentListDto>> GetPaged(GetDocumentsInput input)
        {
            var categories = await _categoryRepository.GetAll().Where(d => d.DeptId == input.DeptId).Select(c => c.Id).ToArrayAsync();
            var carr = Array.ConvertAll(categories, c => "," + c + ",");
            var query = _entityRepository.GetAll().Where(e => carr.Any(c => ("," + e.CategoryCode + ",").Contains(c)))
                .WhereIf(!string.IsNullOrEmpty(input.CategoryCode), e => ("," + e.CategoryCode + ",").Contains(input.CategoryCode))
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), e => e.Name.Contains(input.KeyWord) || e.DocNo.Contains(input.KeyWord));
            //var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderByDescending(v => v.PublishTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<DocumentListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<DocumentListDto>>();

            return new PagedResultDto<DocumentListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取DocumentListDto信息
        /// </summary>
        [Audited]
        public async Task<DocumentListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<DocumentListDto>();
        }

        /// <summary>
        /// 钉钉编号获取标准名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<string> GetDocNameById(EntityDto<Guid> input)
        {
            string docName = await _entityRepository.GetAll().Where(v => v.Id == input.Id).Select(v => v.Name).FirstOrDefaultAsync();
            return docName;
        }

        /// <summary>
        /// 获取编辑 Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetDocumentForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetDocumentForEditOutput();
            DocumentEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<DocumentEditDto>();

                //documentEditDto = ObjectMapper.Map<List<documentEditDto>>(entity);
            }
            else
            {
                editDto = new DocumentEditDto();
            }

            output.Document = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Document的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateDocumentInput input)
        {

            //if (input.Document.Id.HasValue)
            //{
            //	await Update(input.Document);
            //}
            //else
            //{
            //	await Create(input.Document);
            //}
            if (input.Document.Id.HasValue)
            {
                var entity = await Update(input.Document);
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity };
            }
            else
            {
                var entity = await Create(input.Document);
                await CurrentUnitOfWork.SaveChangesAsync();
                foreach (var attItem in input.DocAttachment)
                {
                    attItem.BLL = entity.Id.Value;
                    var docEntity = attItem.MapTo<DocAttachment>();
                    await _docAttachmentRepository.InsertAsync(docEntity);
                }
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity };
            }
        }


        /// <summary>
        /// 新增Document
        /// </summary>

        protected virtual async Task<DocumentEditDto> Create(DocumentEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Document>(input);
            var entity = input.MapTo<Document>();
            var categoryList = await _categoryManager.GetHierarchyCategories(input.CategoryId);
            entity.CategoryCode = string.Join(',', categoryList.Select(c => c.Id).ToArray());
            entity.CategoryDesc = string.Join(',', categoryList.Select(c => c.Name).ToArray());
            entity = await _entityRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return entity.MapTo<DocumentEditDto>();
        }

        /// <summary>
        /// 编辑Document
        /// </summary>

        protected virtual async Task<DocumentEditDto> Update(DocumentEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return entity.MapTo<DocumentEditDto>();
        }



        /// <summary>
        /// 删除Document信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Document的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 构建子部门树
        /// </summary>
        private List<DocNzTreeNode> getDeptChildTree(long pid, List<Organization> depts)
        {
            var trees = depts.Where(d => d.ParentId == pid).Select(d => new DocNzTreeNode()
            {
                key = d.Id.ToString(),
                title = d.DepartmentName,
                order = d.Order,
                children = getDeptChildTree(d.Id, depts)
            });

            return trees.OrderBy(v => v.order).ToList();
        }

        /// <summary>
        /// 构建部门树
        /// </summary>
        private async Task<List<DocNzTreeNode>> getDeptTreeAsync(long[] deptids)
        {
            var trees = new List<DocNzTreeNode>();
            var depts = await _organizationRepository.GetAll().AsNoTracking().ToListAsync();
            foreach (var id in deptids)
            {
                if (id == 1)//顶级市
                {
                    trees.AddRange(getDeptChildTree(id, depts));
                }
                else
                {
                    var dept = depts.Where(d => d.Id == id).First();
                    trees.Add(new DocNzTreeNode()
                    {
                        key = dept.Id.ToString(),
                        title = dept.DepartmentName,
                        order = dept.Order,
                        children = getDeptChildTree(id, depts)
                    });
                }
            }
            return trees;
        }

        public async Task<List<DocNzTreeNode>> GetDeptDocNzTreeNodesAsync(string rootName)
        {
            var docDeptList = new List<DocNzTreeNode>();
            var root = new DocNzTreeNode()
            {
                key = "0",
                title = rootName //"标准归口部门"
            };

            //当前用户角色
            var roles = await GetUserRolesAsync();
            ////如果包含市级管理员 和 系统管理员 全部架构
            ////if (roles.Contains(RoleCodes.Admin))
            ////{
            //root.children = await getDeptTreeAsync(new long[] { 1 });//顶级部门
            ////}
            ////else if (roles.Contains(RoleCodes.EnterpriseAdmin))//本部门架构
            ////{
            ////    var user = await GetCurrentUserAsync();
            ////    if (!string.IsNullOrEmpty(user.EmployeeId))
            ////    {
            ////        var employee = await _employeeRepository.GetAsync(user.EmployeeId);
            ////        var depts = employee.Department.Substring(1, employee.Department.Length - 2).Split("][");//多部门拆分
            ////        root.children = await getDeptTreeAsync(Array.ConvertAll(depts, d => long.Parse(d)));
            ////    }
            ////}
            if (roles.Contains(RoleCodes.Admin) || roles.Contains(RoleCodes.QiGuanAdmin))
            {
                root.children = await getDeptTreeAsync(new long[] { 1 });//顶级部门
            }
            else//本部门架构
            {
                var user = await GetCurrentUserAsync();
                if (!string.IsNullOrEmpty(user.EmployeeId))
                {
                    var employee = await _employeeRepository.GetAsync(user.EmployeeId);
                    var depts = employee.Department.Substring(1, employee.Department.Length - 2).Split("][");//多部门拆分
                    root.children = await getDeptTreeAsync(Array.ConvertAll(depts, d => long.Parse(d)));
                }
            }
            if (root.children.Count == 0)
            {
                root.children.Add(new DocNzTreeNode()
                {
                    key = "-1",
                    title = "没有任何部门权限"
                });
            }
            else
            {
                root.children[0].selected = true;
            }
            docDeptList.Add(root);
            return docDeptList;
        }


        /// <summary>
        /// 获取当前用户所属标准
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DocumentTitleDto>> GetPagedWithPermission(GetDocumentsInput input)
        {
            var curUser = await GetCurrentUserAsync();
            string deptStr = await _employeeRepository.GetAll().Where(v => v.Id == curUser.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var deptId = deptStr.Replace('[', ' ').Replace(']', ' ').Trim();
            var query = _entityRepository.GetAll().Where(v => v.IsAction == true && (v.PublishTime.HasValue ? v.PublishTime <= DateTime.Today : false) && (v.IsAllUser == true || v.DeptIds.Contains(deptId) || v.EmployeeIds.Contains(curUser.EmployeeId)))
                .WhereIf(input.CategoryId.HasValue, v => v.CategoryId == input.CategoryId)
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), e => e.Name.Contains(input.KeyWord) || e.DocNo.Contains(input.KeyWord));

            var cate = _categoryRepository.GetAll();
            var org = _organizationRepository.GetAll();
            var result = from q in query
                         join c in cate on q.CategoryId equals c.Id
                         join o in org on c.DeptId equals o.Id
                         select new DocumentTitleDto()
                         {
                             Id = q.Id,
                             DocNo = q.DocNo,
                             Name = q.Name,
                             CategoryDesc = q.CategoryDesc,
                             PublishTime = q.PublishTime,
                             DeptName = o.DepartmentName
                         };
            var count = await result.CountAsync();
            var entityListDtos = await result.OrderBy(v => v.DeptName).ThenBy(v => v.CategoryDesc).ThenByDescending(v => v.PublishTime).AsNoTracking().PageBy(input).ToListAsync();
            // var entityListDtos = ObjectMapper.Map<List<DocumentListDto>>(entityList);
            //var entityListDtos = entityList.MapTo<List<DocumentListDto>>();
            //var count = await query.CountAsync();
            //var entityList = await query
            //        .OrderBy(v => v.CategoryId)
            //        .ThenByDescending(v => v.PublishTime).AsNoTracking()
            //        .PageBy(input)
            //        .ToListAsync();
            //var entityListDtos = entityList.MapTo<List<DocumentListDto>>();
            return new PagedResultDto<DocumentTitleDto>(count, entityListDtos);
        }

        /// <summary>
        /// 自查学习标题相关信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DocumentTitleDto> GetDocumentTitleAsync(Guid id)
        {
            var query = await _entityRepository.GetAsync(id);
            long depptId = await _categoryRepository.GetAll().Where(v => v.Id == query.CategoryId).Select(v => v.DeptId).FirstOrDefaultAsync();
            string deptName = await _organizationRepository.GetAll().Where(v => v.Id == depptId).Select(v => v.DepartmentName).FirstOrDefaultAsync();
            var result = query.MapTo<DocumentTitleDto>();
            result.DeptName = deptName;
            return result;
        }

        [AbpAllowAnonymous]
        public async Task<List<DocumentListDto>> GetDocumentListByDDUserIdAsync(EntityDto<string> input)
        {
            string deptStr = await _employeeRepository.GetAll().Where(v => v.Id.ToString() == input.Id).Select(v => v.Department).FirstOrDefaultAsync();
            string deptId = deptStr.Replace('[', ' ').Replace(']', ' ').Trim();
            var query = _entityRepository.GetAll().Where(v => v.IsAction == true
                                                        && (v.PublishTime.HasValue ? v.PublishTime <= DateTime.Today : false)
                                                        && (v.IsAllUser == true || v.DeptIds.Contains(deptId) || v.EmployeeIds.Contains(input.Id)));
            var entityList = await query.OrderBy(v => v.CategoryId)
                                        .ThenByDescending(v => v.PublishTime)
                                        .AsNoTracking()
                                        .ToListAsync();
            return entityList.MapTo<List<DocumentListDto>>();
        }

        [AbpAllowAnonymous]
        public async Task<bool> GetHasDocPermissionFromScanAsync(Guid id, string userId)
        {
            string dept = await _employeeRepository.GetAll().Where(v => v.Id == userId).Select(v => v.Department).FirstOrDefaultAsync();
            long deptId = await _organizationRepository.GetAll().Where(v => "[" + v.Id + "]" == dept).Select(v => v.Id).FirstOrDefaultAsync();
            return await _entityRepository.GetAll().AnyAsync(v => v.Id == id && v.IsAction == true && v.PublishTime <= DateTime.Today && (v.IsAllUser == true || v.DeptIds.Contains(deptId.ToString()) || v.EmployeeIds.Contains(userId)));
        }

        [AbpAllowAnonymous]
        public async Task<DocumentListDto> GetDocInfoByScanAsync(Guid id, string userId)
        {
            var doc = await _entityRepository.GetAsync(id);
            long deptId = await _categoryRepository.GetAll().Where(v => v.Id == doc.CategoryId).Select(v => v.DeptId).FirstOrDefaultAsync();
            string deptName = await _organizationRepository.GetAll().Where(v => v.Id == deptId).Select(v => v.DepartmentName).FirstOrDefaultAsync();
            //string deptName = await _organizationRepository.GetAll().Where(v => v.Id.ToString() == doc.DeptIds).Select(v => v.DepartmentName).FirstOrDefaultAsync();
            var result = doc.MapTo<DocumentListDto>();
            result.DeptName = deptName;
            var confirmIds = await _employeeClauseRepository.GetAll().Where(v => v.DocumentId == id && v.EmployeeId == userId).Select(v => v.ClauseId).ToListAsync();
            var clauseList = await _clauseRepository.GetAll().Where(v => v.DocumentId == id).OrderBy(v => v.ClauseNo).AsNoTracking().ToListAsync();
            var clauseDtoList = clauseList.MapTo<List<ClauseListDto>>();
            clauseDtoList.Sort(Factory.Comparer);
            clauseDtoList.ForEach((c) =>
            {
                c.IsConfirm = confirmIds.Any(e => e == c.Id);
            });
            result.ClauseList = clauseDtoList;
            return result;
        }



        /// <summary>
        /// 钉钉上修改Document的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task<APIResultDto> DingUpdateAsync(DingDocumentEditDto input)
        {
            if (input.Id.HasValue)
            {
                Document document = await _entityRepository.GetAsync(input.Id.Value);
                document.Stamps = input.Stamps;
                document.DocNo = input.DocNo;
                document.IsAction = true;
                //await _entityRepository.UpdateAsync(document);
                if (document.CreatorUserId.HasValue)
                {
                    var user = await _userManager.GetUserByIdAsync(document.CreatorUserId.Value);
                    DingRemind(user.EmployeeId);
                    return new APIResultDto() { Code = 0, Msg = "提交成功" };
                }
                else
                {
                    return new APIResultDto() { Code = 1, Msg = "未找到该标准的创建用户" };
                }
            }
            else
            {
                return new APIResultDto() { Code = 2, Msg = "未查询到所属标准" };
            }
        }

        /// <summary>
        /// 审批回调钉钉
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        private APIResultDto DingRemind(string employeeId)
        {
            try
            {
                var ddConfig = _dingDingAppService.GetDingDingConfigByApp(DingDingAppEnum.标准化工作平台);
                var assessToken = _dingDingAppService.GetAccessToken(ddConfig.Appkey, ddConfig.Appsecret);
                var url = string.Format("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2?access_token={0}", assessToken);
                DingMsgs dingMsgs = new DingMsgs();
                dingMsgs.userid_list = employeeId;
                dingMsgs.to_all_user = false;
                dingMsgs.agent_id = ddConfig.AgentID;
                dingMsgs.msg.msgtype = "text";
                dingMsgs.msg.text.content = "您制定的标准管理员已编号,请前往标准化工作平台分配部门和用户";
                var jsonString = SerializerHelper.GetJsonString(dingMsgs, null);
                MessageResponseResult response = new MessageResponseResult();
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonString);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    response = Post.PostGetJson<MessageResponseResult>(url, null, ms);
                };
                return new APIResultDto() { Code = 0, Msg = "钉钉消息发送成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendEmpToChooseUserAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "钉钉消息发送失败" };
            }
        }

        /// <summary>
        /// 标准认领报表列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DocumentConfirmDto>> GetReportDocumentConfirmsListAsync(GetDocumentsInput input)
        {
            var categoryIds = await _categoryRepository.GetAll().Where(v => v.DeptId == input.DeptId).Select(v => v.Id).ToArrayAsync();
            var query = _entityRepository.GetAll().Where(v => categoryIds.Contains(v.CategoryId) && v.IsAction == true).WhereIf(!string.IsNullOrEmpty(input.KeyWord), v => v.Name.Contains(input.KeyWord) || v.DocNo.Contains(input.KeyWord));
            var entityList = await query.OrderBy(v => v.CategoryId).ThenByDescending(v => v.PublishTime).AsNoTracking().PageBy(input).ToListAsync();

            var entityListDtos = entityList.MapTo<List<DocumentConfirmDto>>();
            foreach (var item in entityListDtos)
            {
                if (item.IsAllUser == true)
                {
                    item.ShouldNum = await _employeeRepository.CountAsync();
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.DeptIds))
                    {
                        string[] deptIds = item.DeptIds.Split(',');
                        item.ShouldNum = await _employeeRepository.CountAsync(e => deptIds.Contains(e.Department.Replace('[', ' ').Replace(']', ' ').Trim()));
                    }
                    if (!string.IsNullOrEmpty(item.EmployeeIds))
                    {
                        item.ShouldNum += item.EmployeeIds.Split(',').Count();
                    }
                }

                item.ActualNum = await _employeeClauseRepository.GetAll().Where(v => v.DocumentId == item.Id).Select(v => v.EmployeeId).Distinct().CountAsync();
            }
            var count = await query.CountAsync();
            return new PagedResultDto<DocumentConfirmDto>(count, entityListDtos);
        }

        /// <summary>
        /// 查询已认领or未认领人员名单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<EmpBriefInfo>> GetPagedEmpConfirmListByIdAsync(GetConfirmTypeInput input)
        {
            List<EmpBriefInfo> entityListDtos = new List<EmpBriefInfo>();
            int count = 0;
            if (input.Type == 1)//已认领
            {
                string[] empIds = await _employeeClauseRepository.GetAll().Where(v => v.DocumentId == input.DocId).Select(v => v.EmployeeId).Distinct().ToArrayAsync();
                var entityList = _employeeRepository.GetAll().Where(v => empIds.Contains(v.Id)).Select(v => new EmpBriefInfo()
                {
                    Id = v.Id,
                    Name = v.Name,
                    Phone = v.Mobile,
                    Position = v.Position
                });
                count = await entityList.CountAsync();
                entityListDtos = await entityList.OrderBy(v => v.Name).PageBy(input).ToListAsync();
                return new PagedResultDto<EmpBriefInfo>(count, entityListDtos);
            }
            else
            {
                var doc = await _entityRepository.GetAsync(input.DocId);
                string[] confirmEmpIds = await _employeeClauseRepository.GetAll().Where(v => v.DocumentId == input.DocId).Select(v => v.EmployeeId).Distinct().ToArrayAsync();
                if (doc.IsAllUser)
                {
                    var entityList = _employeeRepository.GetAll().Where(v => !confirmEmpIds.Contains(v.Id)).Select(v => new EmpBriefInfo()
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Phone = v.Mobile,
                        Position = v.Position
                    });
                    count = await entityList.CountAsync();
                    entityListDtos = await entityList.OrderBy(v => v.Name).PageBy(input).ToListAsync();
                    return new PagedResultDto<EmpBriefInfo>(count, entityListDtos);
                }
                else
                {
                    List<EmpBriefInfo> tempListDtos = new List<EmpBriefInfo>();
                    string[] deptIds = null;
                    string[] empIds = null;
                    if (!string.IsNullOrEmpty(doc.DeptIds))
                    {
                        deptIds = doc.DeptIds.Split(',');
                    }
                    if (!string.IsNullOrEmpty(doc.EmployeeIds))
                    {
                        empIds = doc.EmployeeIds.Split(',');
                    }
                    var entityList = _employeeRepository.GetAll()
                    //    .WhereIf(deptIds.Length > 0, e => deptIds.Contains(e.Department.Replace('[', ' ').Replace(']', ' ').Trim())
                    //|| det
                    //)
                    //.WhereIf(empIds.Length>0,e=>empIds.Contains(e.Id))
                    .Where(v => (deptIds.Length > 0 ? deptIds.Contains(v.Department.Replace('[', ' ').Replace(']', ' ').Trim()) : true)
                    || (empIds.Length > 0 ? empIds.Contains(v.Id) : true) && !confirmEmpIds.Contains(v.Id))
                    .Select(v => new EmpBriefInfo()
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Phone = v.Mobile,
                        Position = v.Position
                        //}).OrderBy(v => v.Name).PageBy(input).ToListAsync();
                    });

                    count = await entityList.CountAsync();
                    entityListDtos = await entityList.OrderBy(v => v.Name).PageBy(input).ToListAsync();
                    return new PagedResultDto<EmpBriefInfo>(count, entityListDtos);
                }
            }
        }

        /// <summary>
        /// 检查指标获取当前部门标准来源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DocumentTitleDto>> GetPagedCurDeptDocListAsync(GetDocumentsInput input)
        {
            var user = await GetCurrentUserAsync();
            string empDept = await _employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var categoryIds = await _categoryRepository.GetAll().Where(v => "[" + v.DeptId + "]" == empDept).Select(v => v.Id).ToArrayAsync();
            var query = _entityRepository.GetAll().Where(v => categoryIds.Contains(v.CategoryId) && v.IsAction == true);
            var entityList = await query.OrderBy(v => v.CategoryId).ThenByDescending(v => v.PublishTime).AsNoTracking().PageBy(input).ToListAsync();
            var entityListDtos = entityList.MapTo<List<DocumentTitleDto>>();
            var count = await query.CountAsync();
            return new PagedResultDto<DocumentTitleDto>(count, entityListDtos);
        }


        #region 文档导入
        [AbpAllowAnonymous]
        public async Task<APIResultDto> DocumentReadAsync(DocumentReadInput input)
        {
            try
            {
                var categoryList = await _categoryManager.GetHierarchyCategories(input.CategoryId);
                string CategoryCode = string.Join(',', categoryList.Select(c => c.Id).ToArray());
                string CategoryDesc = string.Join(',', categoryList.Select(c => c.Name).ToArray());
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                //Console.WriteLine("文档转换");
                var docs = GetDocsByDirectory($@"{input.Path}");
                foreach (var doc in docs)
                {   //0编号 1名称 2发布时间 3类型
                    string[] docInfoArry = doc.DocName.Split("#");
                    DateTime dt = DateTime.ParseExact(docInfoArry[2].Split('.')[0], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    Document entity = new Document();
                    entity.Name = docInfoArry[1];
                    entity.DocNo = docInfoArry[0];
                    entity.CategoryCode = CategoryCode;
                    entity.CategoryDesc = CategoryDesc;
                    entity.CategoryId = input.CategoryId;
                    entity.IsAllUser = false;
                    entity.IsAction = true;
                    entity.PublishTime = dt;
                    var id = await _entityRepository.InsertAndGetIdAsync(entity);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    foreach (var item in doc.Sections)
                    {
                        Clause cla = new Clause();
                        cla.Id = item.Id.Value;
                        cla.DocumentId = id;
                        cla.ClauseNo = item.No;
                        cla.Title = item.Title;
                        cla.Content = item.Context;
                        cla.ParentId = doc.Sections.Where(v => v.No == item.PNo).Select(v => v.Id).FirstOrDefault();
                        var pId = await _clauseRepository.InsertAsync(cla);
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }

                return new APIResultDto() { Code = 0, Msg = "导入成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DocumentReadAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 999, Msg = "导入失败" };
            }

        }
        private static List<DocInfo> GetDocsByDirectory(string directoryPath)
        {
            var docs = new List<DocInfo>();

            DirectoryInfo di = new DirectoryInfo(directoryPath);
            FileInfo[] fis = di.GetFiles();
            foreach (var fi in fis)
            {
                var doc = new DocInfo();
                doc.DocName = fi.Name;
                doc.Sections = new List<SectionInfo>();
                var SectionNo = new SectionNo("1");

                string data = string.Empty;
                using (StreamReader sr = new StreamReader(fi.OpenRead(), System.Text.Encoding.GetEncoding("GB2312")))
                {
                    data = sr.ReadToEnd();
                }
                string[] brandData = data.Split("\r\n");
                int index = 1;
                foreach (string b in brandData)
                {
                    var line = b.Trim();
                    if (line.Length > 0)
                    {
                        if (index == 1 && line.StartsWith("1"))//找到第一行
                        {
                            var section = new SectionInfo();
                            section.Id = Guid.NewGuid();
                            section.No = SectionNo.No;
                            section.Title = line.Substring(section.No.Length).Trim();
                            section.Seq = index;
                            section.Level = SectionNo.Level;
                            doc.Sections.Add(section);
                            index++;
                        }
                        else if (index > 1)
                        {
                            var newNo = string.Empty;
                            bool isMap = false;
                            foreach (var nextNo in SectionNo.NextNos)
                            {
                                if (line.Length < nextNo.Length)//如果长度不够，继续循环
                                {
                                    continue;
                                }
                                newNo = line.Substring(0, nextNo.Length);
                                if (newNo == nextNo)//匹配成功 跳出本循环
                                {
                                    isMap = true;
                                    break;
                                }
                            }

                            if (isMap)//匹配成功 表示已为下一个章节
                            {
                                SectionNo.No = newNo;
                                var section = new SectionInfo();
                                section.Id = Guid.NewGuid();
                                section.No = SectionNo.No;
                                section.Level = SectionNo.Level;
                                var tc = line.Substring(section.No.Length).Trim();
                                if (tc.Length > 0)
                                {
                                    if (SectionNo.Level == 1)
                                    {
                                        section.Title = tc;
                                    }
                                    else
                                    {

                                        if (tc.Length < SectionNo.TitleMaxLength && tc.LastIndexOfAny(SectionNo.NoTitleEndChars) != (tc.Length - 1))//小于最大title长度 且不包含特殊字符 表示为title
                                        {
                                            section.Title = tc;
                                        }
                                        else
                                        {
                                            section.Context = tc + "\r\n";
                                        }
                                    }
                                }

                                section.Seq = index;
                                doc.Sections.Add(section);
                                index++;
                            }
                            else
                            {
                                if (doc.Sections.Count() > 0)
                                {
                                    doc.Sections.Last().Context += (line + "\r\n");
                                }
                            }
                        }
                    }
                }

                docs.Add(doc);
            }

            return docs;
        }
    }
    public class SectionNo
    {
        public SectionNo(string no)
        {
            No = no;
        }

        public string No { get; set; }

        public string[] NextNos
        {
            get
            {
                var noList = new List<string>();
                var prefix = string.Empty;
                int i = 1;
                int len = NoInts.Length;
                foreach (var no in NoInts)
                {
                    var newNo = prefix + (no + 1);
                    noList.Add(newNo);
                    prefix = prefix + no + ".";
                    if (i == len)
                    {
                        newNo = prefix + 1;
                        noList.Add(newNo);
                    }
                    i++;
                }

                return noList.OrderByDescending(n => n.Length).ToArray();
            }
        }

        public int[] NoInts
        {
            get
            {
                return Array.ConvertAll(No.Split('.'), n => int.Parse(n));
            }
        }

        public int Level
        {
            get
            {
                return NoInts.Length;
            }
        }

        public int TitleMaxLength
        {
            get
            {
                return 20;
            }
        }

        public char[] NoTitleEndChars
        {
            get
            {
                return new char[] { '：', '；', '，', '。', '？', '！' };
            }
        }
    }
    public class DocInfo
    {
        public string DocName { get; set; }

        public List<SectionInfo> Sections { get; set; }
    }
    public class SectionInfo
    {
        public Guid? Id
        {
            //get
            //{
            //    //if (!Id.HasValue)
            //    //{
            //    //}
            //    return Guid.NewGuid();
            //}
            get; set;
        }
        public string No { get; set; }

        public string Title { get; set; }

        public string Context { get; set; }

        public int Seq { get; set; }
        public int Level { get; set; }
        public string PNo
        {
            get
            {
                if (Level == 1)
                {
                    return string.Empty;
                }
                return No.Substring(0, No.LastIndexOf('.'));
            }
        }
    }
    #endregion

    /// <summary>
    /// 按照自然数排序（无法覆盖null）
    /// </summary>
    class Factory : IComparer<ClauseListDto>
    {
        private Factory() { }
        public static IComparer<ClauseListDto> Comparer
        {
            get { return new Factory(); }
        }
        public int Compare(ClauseListDto x, ClauseListDto y)
        {
            try
            {
                int ret = 0;
                var xsplit = x.ClauseNo.Split(".".ToCharArray()).Select(z => int.Parse(z)).ToList();
                var ysplit = y.ClauseNo.Split(".".ToCharArray()).Select(z => int.Parse(z)).ToList();
                for (int i = 0; i < Math.Max(xsplit.Count, ysplit.Count); i++)
                {

                    if (xsplit.Count - 1 < i)
                    {
                        ret = -1;
                        return ret;
                    }
                    else if (ysplit.Count - 1 < i)
                    {
                        ret = 1;
                        return ret;
                    }
                    else
                    {
                        ret = xsplit[i] - ysplit[i];
                        if (ret != 0)
                            return ret;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}