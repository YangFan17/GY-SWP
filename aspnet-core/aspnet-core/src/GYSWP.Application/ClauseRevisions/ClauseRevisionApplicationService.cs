
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


using GYSWP.ClauseRevisions;
using GYSWP.ClauseRevisions.Dtos;
using GYSWP.ClauseRevisions.DomainService;
using GYSWP.Clauses.Dtos;
using GYSWP.Dtos;
using GYSWP.Clauses;
using GYSWP.DingDingApproval;

namespace GYSWP.ClauseRevisions
{
    /// <summary>
    /// ClauseRevision应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ClauseRevisionAppService : GYSWPAppServiceBase, IClauseRevisionAppService
    {
        private readonly IRepository<ClauseRevision, Guid> _entityRepository;
        private readonly IRepository<Clause, Guid> _clauseRepository;
        private readonly IApprovalAppService _approvalAppService;
        private readonly IClauseRevisionManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ClauseRevisionAppService(
        IRepository<ClauseRevision, Guid> entityRepository
        , IClauseRevisionManager entityManager
        , IRepository<Clause, Guid> clauseRepository
        , IApprovalAppService approvalAppService
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _clauseRepository = clauseRepository;
            _approvalAppService = approvalAppService;
        }


        /// <summary>
        /// 获取ClauseRevision的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<ClauseRevisionListDto>> GetPaged(GetClauseRevisionsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ClauseRevisionListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ClauseRevisionListDto>>();

            return new PagedResultDto<ClauseRevisionListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取ClauseRevisionListDto信息
        /// </summary>

        public async Task<ClauseRevisionListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<ClauseRevisionListDto>();
        }

        /// <summary>
        /// 获取编辑 ClauseRevision
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetClauseRevisionForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetClauseRevisionForEditOutput();
            ClauseRevisionEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<ClauseRevisionEditDto>();

                //clauseRevisionEditDto = ObjectMapper.Map<List<clauseRevisionEditDto>>(entity);
            }
            else
            {
                editDto = new ClauseRevisionEditDto();
            }

            output.ClauseRevision = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改ClauseRevision的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<APIResultDto> CreateOrUpdate(CreateOrUpdateClauseRevisionInput input)
        {
            if (string.IsNullOrEmpty(input.ClauseRevision.Content))
            {
                input.ClauseRevision.Content = null;
            }
            if (string.IsNullOrEmpty(input.ClauseRevision.Title))
            {
                input.ClauseRevision.Title = null;
            }
            if (input.ClauseRevision.Id.HasValue)
            {
                await Update(input.ClauseRevision);
                return new APIResultDto() { Code = 0, Msg = "保存成功" };
            }
            else
            {
                var user = await GetCurrentUserAsync();
                input.ClauseRevision.EmployeeId = user.EmployeeId;
                input.ClauseRevision.EmployeeName = user.EmployeeName;
                //input.ClauseRevision.RevisionType = GYEnums.RevisionType.新增;
                input.ClauseRevision.Status = GYEnums.RevisionStatus.待审核;
                var entity = await Create(input.ClauseRevision);
                return new APIResultDto() { Code = 0, Msg = "保存成功", Data = entity.Id };
            }
        }


        /// <summary>
        /// 新增ClauseRevision
        /// </summary>

        protected virtual async Task<ClauseRevisionEditDto> Create(ClauseRevisionEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <ClauseRevision>(input);
            var entity = input.MapTo<ClauseRevision>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<ClauseRevisionEditDto>();
        }

        /// <summary>
        /// 编辑ClauseRevision
        /// </summary>

        protected virtual async Task Update(ClauseRevisionEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除ClauseRevision信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除ClauseRevision的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 获取当前制修订修改记录
        /// </summary>
        /// <returns></returns>
        public async Task<ClauseRecordResult> GetClauseRevisionListByIdAsync(GetClauseRevisionsInput input)
        {
            ClauseRecordResult result = new ClauseRecordResult();
            var list = await _entityRepository.GetAll().Where(v => v.DocumentId == input.DocumentId && v.ApplyInfoId == input.ApplyInfoId)
                .Select(v => new ClauseRecordListDto
                {
                    Id = v.Id,
                    ClauseNo = v.ClauseNo,
                    Content = v.Content,
                    CreationTime = v.CreationTime,
                    RevisionType = v.RevisionType,
                    Title = v.Title
                }).OrderBy(v => v.RevisionType).ThenBy(v=>v.ClauseNo).ThenByDescending(v=>v.CreationTime).ToListAsync();
            result.List = list;
            result.Count.Cnumber = await _entityRepository.CountAsync(v => v.DocumentId == input.DocumentId && v.ApplyInfoId == input.ApplyInfoId && v.RevisionType == GYEnums.RevisionType.新增);
            result.Count.Unumber = await _entityRepository.CountAsync(v => v.DocumentId == input.DocumentId && v.ApplyInfoId == input.ApplyInfoId && v.RevisionType == GYEnums.RevisionType.修订);
            result.Count.Dnumber = await _entityRepository.CountAsync(v => v.DocumentId == input.DocumentId && v.ApplyInfoId == input.ApplyInfoId && v.RevisionType == GYEnums.RevisionType.删除);
            result.Count.Total = result.Count.Cnumber + result.Count.Unumber + result.Count.Dnumber;
            return result;
        }

        /// <summary>
        /// 新增or修订条款
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> CreateRevisionAsync(ApplyInput input)
        {
            ClauseRevisionEditDto entity = new ClauseRevisionEditDto();
            var user = await GetCurrentUserAsync();
            entity.Title = input.Entity.Title;
            entity.Content = input.Entity.Content;
            entity.ClauseNo = input.Entity.ClauseNo;
            entity.Status = GYEnums.RevisionStatus.待审核;
            entity.RevisionType = input.RevisionType;
            entity.ParentId = input.Entity.ParentId;
            entity.ApplyInfoId = input.ApplyId;
            entity.DocumentId = input.Entity.DocumentId;
            entity.EmployeeName = user.EmployeeName;
            entity.EmployeeId = user.EmployeeId;
            if (input.Entity.Id.HasValue)
            {
                entity.ClauseId = input.Entity.Id.Value;
            }
            entity.HasAttchment = input.Entity.HasAttchment;
            var result = entity.MapTo<ClauseRevision>();
            await _entityRepository.InsertAsync(result);
            return new APIResultDto() { Code = 0, Msg = "保存成功" };
        }

        /// <summary>
        /// 新增or修订条款
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> DeleteRevisionAsync(ApplyInput input)
        {
            ClauseRevisionEditDto entity = new ClauseRevisionEditDto();
            var user = await GetCurrentUserAsync();
            entity.Title = input.Entity.Title;
            entity.Content = input.Entity.Content;
            entity.ClauseNo = input.Entity.ClauseNo;
            entity.Status = GYEnums.RevisionStatus.待审核;
            entity.RevisionType = input.RevisionType;
            entity.ParentId = input.Entity.ParentId;
            entity.ApplyInfoId = input.ApplyId;
            entity.DocumentId = input.Entity.DocumentId;
            entity.EmployeeName = user.EmployeeName;
            entity.EmployeeId = user.EmployeeId;
            if (input.Entity.Id.HasValue)
            {
                entity.ClauseId = input.Entity.Id.Value;
            }
            entity.HasAttchment = input.Entity.HasAttchment;
            var result = entity.MapTo<ClauseRevision>();
            await _entityRepository.InsertAsync(result);
            return new APIResultDto() { Code = 0, Msg = "保存成功" };
        }

        /// <summary>
        /// 生成删除条款记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task CreateDeleteRevisionAsync(ApplyDeleteInput input)
        {
            ClauseRevisionEditDto entity = new ClauseRevisionEditDto();
            var clause = await _clauseRepository.FirstOrDefaultAsync(v => v.Id == input.Id);
            var user = await GetCurrentUserAsync();
            entity.Status = GYEnums.RevisionStatus.待审核;
            entity.RevisionType = GYEnums.RevisionType.删除;
            entity.ClauseId = input.Id;
            entity.Title = clause.Title;
            entity.Content = clause.Content;
            entity.ClauseNo = clause.ClauseNo;
            entity.ParentId = clause.ParentId;
            entity.ApplyInfoId = input.ApplyInfoId;
            entity.DocumentId = input.DocumentId;
            entity.EmployeeName = user.EmployeeName;
            entity.EmployeeId = user.EmployeeId;
            var result = entity.MapTo<ClauseRevision>();
            await _entityRepository.InsertAsync(result);
            //return new APIResultDto() { Code = 0, Msg = "保存成功" };
        }

        /// <summary>
        /// 申请删除前判断是否已删除子项（1 存在子条款，2 已删除，3 已修改无法删除）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applyInfoId"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ClauseRevisionRemoveById(ApplyDeleteInput input)
        {
            int dCount = await _entityRepository.CountAsync(v => v.ClauseId == input.Id && v.ApplyInfoId == input.ApplyInfoId && v.RevisionType == GYEnums.RevisionType.删除);
            if (dCount != 0)
            {
                return new APIResultDto() { Code = 2, Msg = "已删除过条款" };
            }
            int cCount = await _entityRepository.CountAsync(v => v.ClauseId == input.Id && v.ApplyInfoId == input.ApplyInfoId && v.RevisionType == GYEnums.RevisionType.修订);
            if (cCount != 0)
            {
                return new APIResultDto() { Code = 3, Msg = "已修改过条款，无法删除" };
            }
            Guid[] entityIdsList = await _clauseRepository.GetAll().Where(v => v.ParentId == input.Id).Select(v => v.Id).ToArrayAsync();
            if(entityIdsList.Count() != 0)
            {
                foreach (var item in entityIdsList)
                {
                    int itemCount = await _entityRepository.CountAsync(v => v.ClauseId == item && v.ApplyInfoId == input.ApplyInfoId);
                    if (itemCount == 0)
                    {
                        return new APIResultDto() { Code = 1, Msg = "存在子条款" };
                    }
                }
            }
            await CreateDeleteRevisionAsync(input);
            return new APIResultDto() { Code = 0, Msg = "删除申请成功" };
        }

        /// <summary>
        /// 修订删除申请条款
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ClauseRevisionDeleteById(EntityDto<Guid> id)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.Id == id.Id);
            int childCount = await _entityRepository.GetAll().Where(v => (v.ParentId == entity.ClauseId ||v.ParentId == entity.Id) && v.RevisionType == entity.RevisionType).CountAsync();
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
        /// 制定删除条款
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ClauseDraftRemoveById(EntityDto<Guid> id)
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
        private List<ClauseRevisionTreeNodeDto> GetDraftChildren(Guid? id, List<ClauseRevisionTreeListDto> clauseList)
        {
            var list = clauseList.Where(c => c.ParentId == id).Select(c => new ClauseRevisionTreeNodeDto()
            {
                Id = c.Id,
                ClauseNo = c.ClauseNo,
                Title = c.Title,
                Content = c.Content,
                ParentId = c.ParentId,
                Children = GetDraftChildren(c.Id, clauseList)
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取制定标准的条款树形表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ClauseRevisionTreeNodeDto>> GetDraftClauseTreeWithCheckedAsync(Guid documentId)
        {
            var user = await GetCurrentUserAsync();
            var clause = await _entityRepository.GetAll().Where(v => v.DocumentId == documentId && v.EmployeeId == user.EmployeeId).Select(v => new ClauseRevisionTreeListDto()
            {
                Id = v.Id,
                ClauseNo = v.ClauseNo,
                Title = v.Title,
                Content = v.Content,
                ParentId = v.ParentId
            }).OrderBy(v => v.ClauseNo).ToListAsync();
            return GetDraftChildren(null, clause);
        }
    }
}