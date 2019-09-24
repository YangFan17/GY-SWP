
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


using GYSWP.ApplyInfos;
using GYSWP.ApplyInfos.Dtos;
using GYSWP.ApplyInfos.DomainService;
using GYSWP.DingDingApproval;
using GYSWP.Dtos;
using GYSWP.ClauseRevisions;
using GYSWP.Clauses;
using GYSWP.Documents;
using GYSWP.Categorys;
using GYSWP.DocRevisions;
using GYSWP.Categorys.DomainService;

namespace GYSWP.ApplyInfos
{
    /// <summary>
    /// ApplyInfo应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ApplyInfoAppService : GYSWPAppServiceBase, IApplyInfoAppService
    {
        private readonly IRepository<ApplyInfo, Guid> _entityRepository;
        private readonly IApplyInfoManager _entityManager;
        private readonly IApprovalAppService _approvalAppService;
        private readonly IRepository<ClauseRevision, Guid> _clauseRevisionRepository;
        private readonly IRepository<Clause, Guid> _clauseRepository;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<DocRevision, Guid> _docRevisionRepository;
        private readonly ICategoryManager _categoryManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ApplyInfoAppService(
        IRepository<ApplyInfo, Guid> entityRepository
        , IApplyInfoManager entityManager
        , IApprovalAppService approvalAppService
        , IRepository<ClauseRevision, Guid> clauseRevisionRepository
        , IRepository<Clause, Guid> clauseRepository
        , IRepository<Document, Guid> documentRepository
        , IRepository<Category> categoryRepository
        , IRepository<DocRevision, Guid> docRevisionRepository
        , ICategoryManager categoryManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _approvalAppService = approvalAppService;
            _clauseRevisionRepository = clauseRevisionRepository;
            _clauseRepository = clauseRepository;
            _documentRepository = documentRepository;
            _categoryRepository = categoryRepository;
            _docRevisionRepository = docRevisionRepository;
            _categoryManager = categoryManager;
        }


        /// <summary>
        /// 获取ApplyInfo的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<ApplyInfoListDto>> GetPaged(GetApplyInfosInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ApplyInfoListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ApplyInfoListDto>>();

            return new PagedResultDto<ApplyInfoListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取ApplyInfoListDto信息
        /// </summary>

        public async Task<ApplyInfoListDto> GetById(EntityDto<Guid> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<ApplyInfoListDto>();
        }

        /// <summary>
        /// 获取编辑 ApplyInfo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetApplyInfoForEditOutput> GetForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetApplyInfoForEditOutput();
            ApplyInfoEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<ApplyInfoEditDto>();

                //applyInfoEditDto = ObjectMapper.Map<List<applyInfoEditDto>>(entity);
            }
            else
            {
                editDto = new ApplyInfoEditDto();
            }

            output.ApplyInfo = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改ApplyInfo的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateApplyInfoInput input)
        {

            if (input.ApplyInfo.Id.HasValue)
            {
                await Update(input.ApplyInfo);
            }
            else
            {
                await Create(input.ApplyInfo);
            }
        }


        /// <summary>
        /// 新增ApplyInfo
        /// </summary>

        protected virtual async Task<ApplyInfoEditDto> Create(ApplyInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <ApplyInfo>(input);
            var entity = input.MapTo<ApplyInfo>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<ApplyInfoEditDto>();
        }

        /// <summary>
        /// 编辑ApplyInfo
        /// </summary>

        protected virtual async Task Update(ApplyInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除ApplyInfo信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除ApplyInfo的方法
        /// </summary>

        public async Task BatchDelete(List<Guid> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 发起标准修订申请并生成记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ApplyDocAsync(ApplyInfoEditDto input)
        {
            var user = await GetCurrentUserAsync();
            input.EmployeeId = user.EmployeeId;
            input.EmployeeName = user.EmployeeName;
            input.Type = GYEnums.ApplyType.制修订申请;
            input.Status = GYEnums.ApplyStatus.待审批;
            input.CreationTime = DateTime.Now;
            try
            {
                string docName = "";
                if (input.OperateType == GYEnums.OperateType.制定标准)
                {
                    docName = "暂无";
                }
                else
                {
                    docName = await _documentRepository.GetAll().Where(v => v.Id == input.DocumentId).Select(v => v.Name).FirstOrDefaultAsync();
                }
                var result = await _approvalAppService.SubmitDocApproval(input.Reason, input.Content, input.CreationTime, input.OperateType, docName);
                if (result.Code != 0)
                {
                    Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0}", result.Data);
                    return new APIResultDto() { Code = 901, Msg = "标准制修订申请失败" };
                }
                input.ProcessInstanceId = result.Data.ToString();
                await Create(input);
                return new APIResultDto() { Code = 0, Msg = "标准制修订申请成功" };
            }
            catch (Exception ex)
            {

                Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "标准制修订申请失败" };
            }
        }

        /// <summary>
        /// 发起标准修订审核并生成记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ApplyRevisionAsync(ApplyRevisionInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.Id == input.ApplyInfoId && v.DocumentId == input.DocumentId);
            if (entity == null)
            {
                Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg:未找到申请单实例");
                return new APIResultDto() { Code = 903, Msg = "标准制修订审批失败" };
            }
            try
            {
                var result = await _approvalAppService.SubmitRevisionApproval(input.ApplyInfoId, input.DocumentId);
                if (result.Code != 0)
                {
                    Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0}", result.Data);
                    return new APIResultDto() { Code = 901, Msg = "标准制修订审批失败" };
                }
                entity.RevisionPId = result.Data.ToString();
                entity.ProcessingCreationTime = DateTime.Now;
                entity.ProcessingStatus = GYEnums.RevisionStatus.待审核;
                return new APIResultDto() { Code = 0, Msg = "标准制修订审批成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "标准制修订审批失败" };
            }
        }

        /// <summary>
        /// 发起标准制定审核并生成记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<APIResultDto> ApplyDraftDocAsync(ApplyRevisionInput input)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.Id == input.ApplyInfoId);
            if (entity == null)
            {
                Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg:未找到申请单实例");
                return new APIResultDto() { Code = 903, Msg = "标准制修订审批失败" };
            }
            try
            {
                var result = await _approvalAppService.SubmitDraftDocApproval(input.ApplyInfoId, input.DocumentId);
                if (result.Code != 0)
                {
                    Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0}", result.Data);
                    return new APIResultDto() { Code = 901, Msg = "标准制修订审批失败" };
                }
                entity.RevisionPId = result.Data.ToString();
                entity.ProcessingCreationTime = DateTime.Now;
                entity.ProcessingStatus = GYEnums.RevisionStatus.待审核;
                return new APIResultDto() { Code = 0, Msg = "标准制修订审批成功" };
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SendMessageToEmployeeAsync errormsg{0} Exception{1}", ex.Message, ex);
                return new APIResultDto() { Code = 901, Msg = "标准制修订审批失败" };
            }
        }

        /// <summary>
        /// 【作废】制修订申请回调后修改状态
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task UpdateApplyInfoByPIIdAsync(string pIId, string result)
        {

            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.ProcessInstanceId == pIId);
            entity.HandleTime = DateTime.Now;
            if (result == "agree")
            {
                entity.Status = GYEnums.ApplyStatus.审批通过;
                if (entity.OperateType == GYEnums.OperateType.修订标准)
                {
                    entity.ProcessingStatus = GYEnums.RevisionStatus.等待提交;
                }
                else if (entity.OperateType == GYEnums.OperateType.废止标准)
                {
                    entity.ProcessingStatus = GYEnums.RevisionStatus.审核通过;
                    entity.ProcessingHandleTime = entity.HandleTime;
                    entity.ProcessingCreationTime = entity.HandleTime;
                    entity.RevisionPId = pIId;
                    var doc = await _documentRepository.FirstOrDefaultAsync(v => v.Id == entity.DocumentId);
                    //寻找现行当前分类，分配给作废相同分类，无则放在作废标准一级分类下
                    var curCate = await _categoryRepository.GetAll().Where(v => v.Id == doc.CategoryId).Select(v => new { v.Name, v.DeptId, v.ParentId }).FirstOrDefaultAsync();
                    var zuofeiCate = await _categoryRepository.GetAll().Where(v => v.DeptId == curCate.DeptId && v.ParentId != curCate.ParentId && v.Name == curCate.Name).Select(v => v.Id).FirstOrDefaultAsync();
                    if (zuofeiCate == 0)
                    {
                        zuofeiCate = await _categoryRepository.GetAll().Where(v => v.DeptId == curCate.DeptId && v.Name == "作废标准库").Select(v => v.Id).FirstOrDefaultAsync();
                    }
                    var categoryList = await _categoryManager.GetHierarchyCategories(zuofeiCate);
                    //int categoyrId = await _categoryRepository.GetAll().Where(v => v.DeptId == long.Parse(doc.DeptIds) && v.Name == "作废标准库").Select(v => v.Id).FirstOrDefaultAsync();
                    doc.CategoryId = zuofeiCate;
                    doc.CategoryCode = string.Join(',', categoryList.Select(c => c.Id).ToArray());
                    doc.CategoryDesc = string.Join(',', categoryList.Select(c => c.Name).ToArray());
                    doc.IsAction = false;
                    doc.InvalidTime = entity.HandleTime;
                    doc.Stamps = "1,3";
                }
                else
                {
                    entity.ProcessingStatus = GYEnums.RevisionStatus.等待提交;
                }
            }
            else
            {
                entity.Status = GYEnums.ApplyStatus.审批拒绝;
                if (entity.OperateType == GYEnums.OperateType.修订标准)
                {
                    entity.ProcessingStatus = GYEnums.RevisionStatus.前置拒绝;
                }
                else if (entity.OperateType == GYEnums.OperateType.废止标准)
                {
                    entity.ProcessingStatus = GYEnums.RevisionStatus.审核拒绝;
                    entity.ProcessingHandleTime = entity.HandleTime;
                    entity.ProcessingCreationTime = entity.HandleTime;
                    entity.RevisionPId = pIId;
                }
                else
                {

                }
            }
            //await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 【修订】修订审批回调后修改内容、状态
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task UpdateDocClauseByPIIdAsync(string pIId, string result)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.RevisionPId == pIId);
            entity.ProcessingHandleTime = DateTime.Now;
            var list = await _clauseRevisionRepository.GetAll().Where(v => v.ApplyInfoId == entity.Id).OrderBy(v => v.ClauseNo).ThenBy(v => v.RevisionType).ThenBy(v => v.CreationTime).ToListAsync();
            if (result == "agree")
            {
                entity.ProcessingStatus = GYEnums.RevisionStatus.审核通过;
                foreach (var item in list)
                {
                    item.Status = GYEnums.RevisionStatus.审核通过;
                    if (item.RevisionType == GYEnums.RevisionType.新增)
                    {
                        Clause clause = new Clause();
                        clause.ClauseNo = item.ClauseNo;
                        clause.Title = item.Title;
                        clause.Content = item.Content;
                        clause.DocumentId = item.DocumentId;
                        clause.BLLId = item.Id;
                        clause.CreatorUserId = item.CreatorUserId;
                        if (item.ParentId.HasValue)
                        {
                            Guid? clauseId = await _clauseRepository.GetAll().Where(v => v.Id == item.ParentId).Select(v => v.Id).FirstOrDefaultAsync();
                            if (clauseId.HasValue && clauseId != Guid.Empty) //现有子级新增
                            {
                                clause.ParentId = clauseId;
                            }
                            else //子级新增子级
                            {
                                Guid newId = await _clauseRepository.GetAll().Where(v => v.BLLId == item.ParentId).Select(v => v.Id).FirstOrDefaultAsync();
                                clause.ParentId = newId;
                            }
                        }

                        await _clauseRepository.InsertAsync(clause);
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                    else if (item.RevisionType == GYEnums.RevisionType.修订)
                    {
                        var origin = await _clauseRepository.FirstOrDefaultAsync(v => v.Id == item.ClauseId);
                        origin.ClauseNo = item.ClauseNo;
                        origin.Title = item.Title;
                        origin.Content = item.Content;
                        origin.BLLId = item.Id;
                        origin.LastModifierUserId = item.LastModifierUserId;
                    }
                    else
                    {
                        var delete = await _clauseRepository.FirstOrDefaultAsync(v => v.Id == item.ClauseId);
                        delete.IsDeleted = true;
                        delete.BLLId = item.Id;
                        delete.DeleterUserId = item.CreatorUserId;
                        delete.DeletionTime = DateTime.Now;
                        //await _clauseRepository.DeleteAsync(v => v.Id == item.ClauseId);
                    }
                }

                //发送申请人（标准化管理员）修改文档通知
                string docName = await _documentRepository.GetAll().Where(v => v.Id == entity.DocumentId).Select(v => v.Name).FirstOrDefaultAsync();
                _approvalAppService.SendMessageToStandardAdminAsync(docName, entity.EmployeeId);
            }
            else
            {
                entity.ProcessingStatus = GYEnums.RevisionStatus.审核拒绝;
                foreach (var item in list)
                {
                    item.Status = GYEnums.RevisionStatus.审核拒绝;
                }
            }
        }


        /// <summary>
        /// 【制定】审批通过后创建制定标准
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task CreateDraDocByPIIdAsync(string pIId, string result)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(v => v.RevisionPId == pIId);
            entity.ProcessingHandleTime = DateTime.Now;
            var docRevision = await _docRevisionRepository.FirstOrDefaultAsync(v => v.ApplyInfoId == entity.Id);
            var clauseRevisionList = await _clauseRevisionRepository.GetAll().Where(v => v.ApplyInfoId == entity.Id && v.RevisionType == GYEnums.RevisionType.标准制定).OrderBy(v => v.ClauseNo).ThenBy(v => v.CreationTime).ToListAsync();
            if (result == "agree")
            {
                docRevision.Status = GYEnums.RevisionStatus.审核通过;
                entity.ProcessingStatus = GYEnums.RevisionStatus.审核通过;
                string categoryName = await _categoryRepository.GetAll().Where(v => v.Id == docRevision.CategoryId).Select(v => v.Name).FirstOrDefaultAsync();
                //先创建标准实体
                Document doc = new Document();
                doc.Name = docRevision.Name;
                doc.CategoryId = docRevision.CategoryId;
                doc.CategoryDesc = categoryName;
                doc.IsAction = false;
                doc.CategoryId = docRevision.CategoryId;
                var categoryList = await _categoryManager.GetHierarchyCategories(docRevision.CategoryId);
                doc.CategoryCode = string.Join(',', categoryList.Select(c => c.Id).ToArray());
                doc.CategoryDesc = string.Join(',', categoryList.Select(c => c.Name).ToArray());
                doc.CreatorUserId = docRevision.CreatorUserId;
                doc.BLLId = docRevision.Id;
                Guid docId = await _documentRepository.InsertAndGetIdAsync(doc);
                await CurrentUnitOfWork.SaveChangesAsync();
                foreach (var item in clauseRevisionList)
                {
                    item.Status = GYEnums.RevisionStatus.审核通过;
                    Clause clause = new Clause();
                    clause.DocumentId = docId;
                    clause.ClauseNo = item.ClauseNo;
                    clause.Title = item.Title;
                    clause.Content = item.Content;
                    clause.BLLId = item.Id;
                    clause.CreatorUserId = item.CreatorUserId;
                    if (item.ParentId.HasValue)
                    {
                        Guid newId = await _clauseRepository.GetAll().Where(v => v.BLLId == item.ParentId).Select(v => v.Id).FirstOrDefaultAsync();
                        clause.ParentId = newId;
                    }
                    await _clauseRepository.InsertAsync(clause);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
                //发送企管科通知
                _approvalAppService.SendMessageToQGAdminAsync(docRevision.Name, docId);
            }
            else
            {
                entity.ProcessingStatus = GYEnums.RevisionStatus.审核拒绝;
                docRevision.Status = GYEnums.RevisionStatus.审核拒绝;
                foreach (var item in clauseRevisionList)
                {
                    item.Status = GYEnums.RevisionStatus.审核拒绝;
                }
            }
        }
    }
}