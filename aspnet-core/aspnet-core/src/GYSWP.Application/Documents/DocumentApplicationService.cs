
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
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _categoryRepository = categoryRepository;
            _clauseRepository = clauseRepository;
            _employeeClauseRepository = employeeClauseRepository;
            _categoryManager = categoryManager;
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

        public async Task<DocumentListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<DocumentListDto>();
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
                children = getDeptChildTree(d.Id, depts)
            });

            return trees.ToList();
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
            //如果包含市级管理员 和 系统管理员 全部架构
            if (roles.Contains(RoleCodes.Admin))
            {
                root.children = await getDeptTreeAsync(new long[] { 1 });//顶级部门
            }
            //else if (roles.Contains(RoleCodes.EnterpriseAdmin))//本部门架构
            //{
            //    var user = await GetCurrentUserAsync();
            //    if (!string.IsNullOrEmpty(user.EmployeeId))
            //    {
            //        var employee = await _employeeRepository.GetAsync(user.EmployeeId);
            //        var depts = employee.Department.Substring(1, employee.Department.Length - 2).Split("][");//多部门拆分
            //        root.children = await getDeptTreeAsync(Array.ConvertAll(depts, d => long.Parse(d)));
            //    }
            //}
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
        public async Task<PagedResultDto<DocumentListDto>> GetPagedWithPermission(GetDocumentsInput input)
        {
            var curUser = await GetCurrentUserAsync();
            string deptStr = await _employeeRepository.GetAll().Where(v => v.Id == curUser.EmployeeId).Select(v => v.Department).FirstOrDefaultAsync();
            var deptId = deptStr.Replace('[', ' ').Replace(']', ' ').Trim();
            var query = _entityRepository.GetAll().Where(v => v.IsAction == true && (v.PublishTime.HasValue ? v.PublishTime <= DateTime.Today : false) && (v.IsAllUser == true || v.DeptIds.Contains(deptId) || v.EmployeeIds.Contains(curUser.EmployeeId)))
                .WhereIf(input.CategoryId.HasValue, v => v.CategoryId == input.CategoryId)
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), e => e.Name.Contains(input.KeyWord) || e.DocNo.Contains(input.KeyWord));
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(v => v.CategoryId)
                    .ThenByDescending(v => v.PublishTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            var entityListDtos = entityList.MapTo<List<DocumentListDto>>();
            return new PagedResultDto<DocumentListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 自查学习标题相关信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DocumentTitleDto> GetDocumentTitleAsync(Guid id)
        {
            var query = await _entityRepository.GetAsync(id);
            string deptName = await _organizationRepository.GetAll().Where(v => v.Id.ToString() == query.DeptIds).Select(v => v.DepartmentName).FirstOrDefaultAsync();
            var result = query.MapTo<DocumentTitleDto>();
            result.DeptName = deptName;
            return result;
        }

        [AbpAllowAnonymous]
        public async Task<List<DocumentListDto>> GetDocumentListByDDUserIdAsync(EntityDto<string> input)
        {
            var query = _entityRepository.GetAll().Where(v => v.IsAction == true
                                                        && (v.PublishTime.HasValue ? v.PublishTime <= DateTime.Today : false)
                                                        && (v.IsAllUser == true || v.EmployeeIds.Contains(input.Id)));
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
            string deptName = await _organizationRepository.GetAll().Where(v => v.Id.ToString() == doc.DeptIds).Select(v => v.DepartmentName).FirstOrDefaultAsync();
            var result = doc.MapTo<DocumentListDto>();
            result.DeptName = deptName;

            var confirmIds = await _employeeClauseRepository.GetAll().Where(v => v.DocumentId == id && v.EmployeeId == userId).Select(v => v.ClauseId).ToListAsync();
            var clauseList = await _clauseRepository.GetAll().Where(v => v.DocumentId == id).OrderBy(v => v.ClauseNo).AsNoTracking().ToListAsync();
            var clauseDtoList = clauseList.MapTo<List<ClauseListDto>>();
            clauseDtoList.ForEach((c) =>
            {
                c.IsConfirm = confirmIds.Any(e => e == c.Id);
            });
            result.ClauseList = clauseDtoList;
            return result;
        }
        #region 文档导入
        public async Task<bool> documentReadAsync(string path)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Console.WriteLine("文档转换");
            var docs = GetDocsByDirectory($@"{path}");
            foreach (var doc in docs)
            {   //0编号 1名称 2发布时间 3类型
                string[] docInfoArry = doc.DocName.Split("#");
                Document entity = new Document();
                entity.Name = docInfoArry[1];
                entity.DocNo = docInfoArry[0];
                entity.IsAllUser = true;
                entity.IsAction = true;
                entity.PublishTime = Convert.ToDateTime(docInfoArry[2]);
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

            return true;
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
}