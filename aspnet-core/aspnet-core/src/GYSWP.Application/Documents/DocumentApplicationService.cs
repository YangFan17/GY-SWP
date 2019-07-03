
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
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _categoryRepository = categoryRepository;
            _clauseRepository = clauseRepository;
            _employeeClauseRepository = employeeClauseRepository;
        }


        /// <summary>
        /// 获取Document的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<DocumentListDto>> GetPaged(GetDocumentsInput input)
        {
            //var categories = await _categoryRepository.GetAll().Where(d => d.DeptId == input.DeptId).Select(c => c.Id).ToArrayAsync();
            //var carr = Array.ConvertAll(categories, c => "," + c + ",");
            var query = _entityRepository.GetAll().Where(v => v.DeptIds == input.DeptId)
                .WhereIf(input.CategoryId.HasValue, v => v.CategoryId == input.CategoryId)
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
            var query = _entityRepository.GetAll().Where(v => v.IsAction == true && (v.PublishTime.HasValue ? v.PublishTime <= DateTime.Today : false) && (v.IsAllUser == true || v.EmployeeIds.Contains(curUser.EmployeeId)))
                .WhereIf(input.CategoryId.HasValue, v => v.CategoryId == input.CategoryId)
                .WhereIf(!string.IsNullOrEmpty(input.KeyWord), e => e.Name.Contains(input.KeyWord) || e.DocNo.Contains(input.KeyWord));
            var count = await query.CountAsync();
            var entityList = await query
                    .OrderBy(v=>v.CategoryId)
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
            return await _entityRepository.GetAll().AnyAsync(v => v.Id == id && (v.IsAllUser == true || v.EmployeeIds.Contains(userId)));
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

        //public async Task<object> getPdf1Async(Guid id)
        //{
        ////XWPFDocument docx = new XWPFDocument();
        ////MemoryStream ms = new MemoryStream();

        //////设置文档
        ////docx.Document.body.sectPr = new CT_SectPr();
        ////CT_SectPr setPr = docx.Document.body.sectPr;
        //////获取页面大小
        ////Tuple<int, int> size = GetPaperSize(setting.PaperType);
        ////setPr.pgSz.w = (ulong)size.Item1;
        ////setPr.pgSz.h = (ulong)size.Item2;
        //////创建一个段落
        ////CT_P p = docx.Document.body.AddNewP();
        //////段落水平居中
        ////p.AddNewPPr().AddNewJc().val = ST_Jc.center;
        ////XWPFParagraph gp = new XWPFParagraph(p, docx);

        ////XWPFRun gr = gp.CreateRun();
        //////创建标题
        ////if (!string.IsNullOrEmpty(setting.TitleSetting.Title))
        ////{
        ////    gr.GetCTR().AddNewRPr().AddNewRFonts().ascii = setting.TitleSetting.FontName;
        ////    gr.GetCTR().AddNewRPr().AddNewRFonts().eastAsia = setting.TitleSetting.FontName;
        ////    gr.GetCTR().AddNewRPr().AddNewRFonts().hint = ST_Hint.eastAsia;
        ////    gr.GetCTR().AddNewRPr().AddNewSz().val = (ulong)setting.TitleSetting.FontSize;//2号字体
        ////    gr.GetCTR().AddNewRPr().AddNewSzCs().val = (ulong)setting.TitleSetting.FontSize;
        ////    gr.GetCTR().AddNewRPr().AddNewB().val = setting.TitleSetting.HasBold; //加粗
        ////    gr.GetCTR().AddNewRPr().AddNewColor().val = "black";//字体颜色
        ////    gr.SetText(setting.TitleSetting.Title);
        ////}

        //////创建文档主要内容
        ////if (!string.IsNullOrEmpty(setting.MainContentSetting.MainContent))
        ////{
        ////    p = docx.Document.body.AddNewP();
        ////    p.AddNewPPr().AddNewJc().val = ST_Jc.both;
        ////    gp = new XWPFParagraph(p, docx)
        ////    {
        ////        IndentationFirstLine = 2
        ////    };

        ////    //单倍为默认值（240）不需设置，1.5倍=240X1.5=360，2倍=240X2=480
        ////    p.AddNewPPr().AddNewSpacing().line = "400";//固定20磅
        ////    p.AddNewPPr().AddNewSpacing().lineRule = ST_LineSpacingRule.exact;

        ////    gr = gp.CreateRun();
        ////    CT_RPr rpr = gr.GetCTR().AddNewRPr();
        ////    CT_Fonts rfonts = rpr.AddNewRFonts();
        ////    rfonts.ascii = setting.MainContentSetting.FontName;
        ////    rfonts.eastAsia = setting.MainContentSetting.FontName;
        ////    rpr.AddNewSz().val = (ulong)setting.MainContentSetting.FontSize;//5号字体-21
        ////    rpr.AddNewSzCs().val = (ulong)setting.MainContentSetting.FontSize;
        ////    rpr.AddNewB().val = setting.MainContentSetting.HasBold;

        ////    gr.SetText(setting.MainContentSetting.MainContent);
        ////}

        //////开始写入
        ////docx.Write(ms);

        ////using (FileStream fs = new FileStream(setting.SavePath, FileMode.Create, FileAccess.Write))
        ////{
        ////    byte[] data = ms.ToArray();
        ////    fs.Write(data, 0, data.Length);
        ////    fs.Flush(); 
        ////}
        ////ms.Close();
        //var data = await _clauseRepository.GetAll().Where(v => v.DocumentId == id).OrderBy(v=>v.ClauseNo).ToListAsync();
        //var title = await _entityRepository.GetAll().Where(v => v.Id == id).Select(v => v.Name).FirstOrDefaultAsync();
        //var newFile2 = $@"{title}.docx";
        //using (var fs = new FileStream(newFile2, FileMode.Create, FileAccess.Write))
        //{
        //    XWPFDocument doc = new XWPFDocument();
        //    //CT_SectPr m_SectPr = new CT_SectPr();       //实例一个尺寸类的实例
        //    //m_SectPr.pgSz.w = 16838;        //设置宽度（这里是一个ulong类型）
        //    //m_SectPr.pgSz.h = 11906;        //设置高度（这里是一个ulong类型）
        //    //doc.Document.body.sectPr = m_SectPr;
        //    foreach (var item in data)
        //    {
        //        var p0 = doc.CreateParagraph();
        //        p0.Alignment = ParagraphAlignment.LEFT;
        //        XWPFRun r0 = p0.CreateRun();
        //        r0.FontFamily = "黑体";
        //        r0.FontSize = 10;
        //        r0.SetTextPosition(15);
        //        //r0.IsBold = false;
        //        r0.SetText(item.ClauseNo + ' ' + (item.Title ?? ""));
        //        var p1 = doc.CreateParagraph();
        //        p1.Alignment = ParagraphAlignment.LEFT;
        //        XWPFRun r1 = p1.CreateRun();
        //        p1.IndentationFirstLine = 600;
        //        r1.FontFamily = "宋体";
        //        r1.FontSize = 10;
        //        r1.SetTextPosition(15);
        //        r1.SetText(item.Content??"");
        //    }
        //    //var p1 = doc.CreateParagraph();
        //    //p1.Alignment = ParagraphAlignment.LEFT;
        //    //p1.IndentationFirstLine = 500;
        //    //XWPFRun r1 = p1.CreateRun();
        //    //r1.FontFamily = "·ÂËÎ";
        //    //r1.FontSize = 12;
        //    //r1.IsBold = true;
        //    //r1.SetText("This is content, content content content content content content content content content");

        //    doc.Write(fs);
        //}
        //return true;
        //}
    }
}