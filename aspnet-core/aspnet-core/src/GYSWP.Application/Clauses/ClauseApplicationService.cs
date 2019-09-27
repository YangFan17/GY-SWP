
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


using GYSWP.Clauses;
using GYSWP.Clauses.Dtos;
using GYSWP.Clauses.DomainService;
using GYSWP.Dtos;
using GYSWP.EmployeeClauses;
using GYSWP.DocAttachments;
using GYSWP.DocRevisions;
using GYSWP.GYEnums;
using GYSWP.ClauseRevisions;
using Abp.Domain.Uow;
using GYSWP.Authorization.Users;
using GYSWP.Documents;

namespace GYSWP.Clauses
{
    /// <summary>
    /// Clause应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ClauseAppService : GYSWPAppServiceBase, IClauseAppService
    {
        private readonly IRepository<Clause, Guid> _entityRepository;
        private readonly IRepository<EmployeeClause, Guid> _employeeClauseRepository;
        private readonly IRepository<DocAttachment, Guid> _docAttachmentRepository;
        private readonly IRepository<DocRevision, Guid> _docRevisionRepository;
        private readonly IRepository<ClauseRevision, Guid> _clauseRevisionRepository;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly UserManager _userManager;
        private readonly IClauseManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ClauseAppService(
        IRepository<Clause, Guid> entityRepository
        , IClauseManager entityManager
        , IRepository<EmployeeClause, Guid> employeeClauseRepository
        , IRepository<DocAttachment, Guid> docAttachmentRepository
        , IRepository<DocRevision, Guid> docRevisionRepository
        , IRepository<ClauseRevision, Guid> clauseRevisionRepository
        , IRepository<Document, Guid> documentRepository
        , UserManager userManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _employeeClauseRepository = employeeClauseRepository;
            _docAttachmentRepository = docAttachmentRepository;
            _docRevisionRepository = docRevisionRepository;
            _clauseRevisionRepository = clauseRevisionRepository;
            _documentRepository = documentRepository;
            _userManager = userManager;
        }


        /// <summary>
        /// 获取Clause的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<ClauseListDto>> GetPaged(GetClausesInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ClauseListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ClauseListDto>>();

            return new PagedResultDto<ClauseListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 获取子条款
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clauseList"></param>
        /// <returns></returns>
        private List<ClauseTreeNodeDto> GetChildren(Guid? id, List<ClauseTreeListDto> clauseList)
        {
            var list = clauseList.Where(c => c.ParentId == id).Select(c => new ClauseTreeNodeDto()
            {
                Id = c.Id,
                ClauseNo = c.ClauseNo,
                Title = c.Title,
                Content = c.Content,
                ParentId = c.ParentId,
                Children = GetChildren(c.Id, clauseList)
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取条款树形表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ClauseTreeNodeDto>> GetClauseTreeAsync(GetClausesInput input)
        {
            var clause = await _entityRepository.GetAll().Where(v => v.DocumentId == input.DocumentId).Select(v => new ClauseTreeListDto()
            {
                Id = v.Id,
                ClauseNo = v.ClauseNo,
                Title = v.Title,
                Content = v.Content,
                ParentId = v.ParentId
            }).OrderBy(v => v.ClauseNo).ToListAsync();
            clause.Sort(Factory.Comparer);
            return GetChildren(null, clause);
        }


        /// <summary>
        /// 通过指定id获取ClauseListDto信息
        /// </summary>

        public async Task<ClauseListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);
            return entity.MapTo<ClauseListDto>();
        }

        /// <summary>
        /// 获取编辑 Clause
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetClauseForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetClauseForEditOutput();
            ClauseEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<ClauseEditDto>();

                //clauseEditDto = ObjectMapper.Map<List<clauseEditDto>>(entity);
            }
            else
            {
                editDto = new ClauseEditDto();
            }

            output.Clause = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Clause的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        //public async Task CreateOrUpdate(CreateOrUpdateClauseInput input)
        //{

        //	if (input.Clause.Id.HasValue)
        //	{
        //		await Update(input.Clause);
        //	}
        //	else
        //	{
        //		await Create(input.Clause);
        //	}
        //}
        public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateClauseInput input)
        {
            if (string.IsNullOrEmpty(input.Clause.Content))
            {
                input.Clause.Content = null;
            }
            if (string.IsNullOrEmpty(input.Clause.Title))
            {
                input.Clause.Title = null;
            }
            if (input.Clause.Id.HasValue)
            {
                await Update(input.Clause);
                return new APIResultDto() { Code = 0, Msg = "保存成功" };
            }
            else
            {
                var entity = await Create(input.Clause);
                await CurrentUnitOfWork.SaveChangesAsync();
                foreach (var attItem in input.DocAttachment)
                {
                    attItem.BLL = entity.Id.Value;
                    var docEntity = attItem.MapTo<DocAttachment>();
                    await _docAttachmentRepository.InsertAsync(docEntity);
                }
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
            }
        }

        /// <summary>
        /// 新增Clause
        /// </summary>

        protected virtual async Task<ClauseEditDto> Create(ClauseEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Clause>(input);
            var entity = input.MapTo<Clause>();


            entity = await _entityRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return entity.MapTo<ClauseEditDto>();
        }

        /// <summary>
        /// 编辑Clause
        /// </summary>

        protected virtual async Task Update(ClauseEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }



        /// <summary>
        /// 删除Clause信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Clause的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public async Task<APIResultDto> ClauseRemoveById(EntityDto<Guid> id)
        {
            int childCount = await _entityRepository.GetAll().Where(v => v.ParentId == id.Id).CountAsync();
            if (childCount != 0)
            {
                return new APIResultDto() { Code = 1, Msg = "存在子条款" };
            }
            else
            {
                await _entityRepository.DeleteAsync(id.Id);
                return new APIResultDto() { Code = 0, Msg = "删除成功" };
            }
        }


        /// <summary>
        /// 获取子条款
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clauseList"></param>
        /// <returns></returns>
        private List<ClauseTreeNodeDto> GetChildrenWithChecked(Guid? id, List<ClauseTreeListDto> clauseList)
        {
            var list = clauseList.Where(c => c.ParentId == id).Select(c => new ClauseTreeNodeDto()
            {
                Id = c.Id,
                ClauseNo = c.ClauseNo,
                Title = c.Title,
                Content = c.Content,
                ParentId = c.ParentId,
                BLLId = c.BLLId,
                LastModificationTime = c.LastModificationTime,
                CreationTime = c.CreationTime,
                Checked = c.Checked,
                Children = GetChildrenWithChecked(c.Id, clauseList)
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取确认后的条款树形表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ClauseTreeNodeDto>> GetClauseTreeWithCheckedAsync(GetClausesInput input)
        {
            var user = await GetCurrentUserAsync();
            var confirmIds = await _employeeClauseRepository.GetAll().Where(v => v.DocumentId == input.DocumentId && v.EmployeeId == user.EmployeeId).Select(v => v.ClauseId).ToListAsync();

            var clause = await _entityRepository.GetAll().Where(v => v.DocumentId == input.DocumentId).Select(v => new ClauseTreeListDto()
            {
                Id = v.Id,
                ClauseNo = v.ClauseNo,
                Title = v.Title,
                Content = v.Content,
                BLLId = v.BLLId,
                LastModificationTime = v.LastModificationTime,
                CreationTime = v.CreationTime,
                ParentId = v.ParentId
            }).OrderBy(v => v.ClauseNo).ToListAsync();
            clause.Sort(Factory.Comparer);
            foreach (var item in clause)
            {
                foreach (var cnfmId in confirmIds)
                {
                    if (item.Id == cnfmId)
                    {
                        item.Checked = true;
                        break;
                    }
                }
            }

            return GetChildrenWithChecked(null, clause);
        }

        /// <summary>
        /// 标准统计修订条款汇总
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ClauseTreeNodeDto>> GetRevisionClauseReportAsync(ReportClauseInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                Guid? docBllId = await _documentRepository.GetAll().Where(v => v.Id == input.DocumentId).Select(v=>v.BLLId).FirstOrDefaultAsync();
                var clause = _entityRepository.GetAll().Where(v => v.DocumentId == input.DocumentId && v.BLLId.HasValue);
                var clauseRevision = _clauseRevisionRepository.GetAll().Where(v => (v.DocumentId == input.DocumentId || (v.DocumentId.HasValue ? v.DocumentId == docBllId:true)) && v.Status == RevisionStatus.审核通过 && v.IsDeleted == false);
                //var clauseRevision = _clauseRevisionRepository.GetAll().Where(v => v.DocumentId == input.DocumentId && v.Status == RevisionStatus.审核通过 && v.RevisionType != RevisionType.标准制定 && v.IsDeleted == false);
                var list = await (from cr in clauseRevision
                                  join c in clause on cr.Id equals c.BLLId
                                  select new ClauseTreeNodeDto()
                                  {
                                      Id = cr.Id,
                                      ClauseNo = c.ClauseNo,
                                      Title = c.Title,
                                      Content = c.Content,
                                      CreationTime = cr.CreationTime,
                                      RevisionType = cr.RevisionType,
                                      CreatorUserId = cr.CreatorUserId
                                  }).ToListAsync();
                list.Sort(ReportFactory.Comparer);
                foreach (var item in list)
                {
                    if (item.CreatorUserId.HasValue)
                    {
                        var user = await _userManager.GetUserByIdAsync(item.CreatorUserId.Value);
                        item.CreatorUserName = user.EmployeeName;
                    }
                    item.Children = await _clauseRevisionRepository.GetAll().Where(v => (v.DocumentId == input.DocumentId || (v.DocumentId.HasValue ? v.DocumentId == docBllId : true)) && v.Status == RevisionStatus.审核通过 && v.ClauseNo == item.ClauseNo && v.Id != item.Id && v.IsDeleted == false).Select(v => new ClauseTreeNodeDto()
                    //item.Children = await _clauseRevisionRepository.GetAll().Where(v => v.DocumentId == input.DocumentId && v.Status == RevisionStatus.审核通过 && v.RevisionType != RevisionType.标准制定 && v.ClauseNo == item.ClauseNo && v.Id != item.Id && v.IsDeleted == false).Select(v => new ClauseTreeNodeDto()
                    {
                        Id = v.Id,
                        ClauseNo = v.ClauseNo,
                        Title = v.Title,
                        Content = v.Content,
                        CreationTime = v.CreationTime,
                        RevisionType = v.RevisionType,
                        CreatorUserId = v.CreatorUserId
                    }).OrderByDescending(v => v.CreationTime).ToListAsync();
                    foreach (var child in item.Children)
                    {
                        var user = await _userManager.GetUserByIdAsync(child.CreatorUserId.Value);
                        child.CreatorUserName = user.EmployeeName;
                    }
                }
                return list;
            }
        }

        class Factory : IComparer<ClauseTreeListDto>
        {
            private Factory() { }
            public static IComparer<ClauseTreeListDto> Comparer
            {
                get { return new Factory(); }
            }
            public int Compare(ClauseTreeListDto x, ClauseTreeListDto y)
            {
                try
                {
                    return x.ClauseNo.Length == y.ClauseNo.Length ? x.ClauseNo.CompareTo(y.ClauseNo) : x.ClauseNo.Length - y.ClauseNo.Length;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 按照自然数排序（无法覆盖null）
        /// </summary>
        class ReportFactory : IComparer<ClauseTreeNodeDto>
        {
            private ReportFactory() { }
            public static IComparer<ClauseTreeNodeDto> Comparer
            {
                get { return new ReportFactory(); }
            }
            public int Compare(ClauseTreeNodeDto x, ClauseTreeNodeDto y)
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
}


