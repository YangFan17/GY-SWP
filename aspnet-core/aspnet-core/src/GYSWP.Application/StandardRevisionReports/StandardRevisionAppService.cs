using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using GYSWP.ClauseRevisions;
using GYSWP.DocRevisions;
using GYSWP.Documents;
using GYSWP.Organizations;
using GYSWP.StandardRevisionReports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GYSWP.GYEnums;
using GYSWP.Categorys;
using GYSWP.ApplyInfos;

namespace GYSWP.StandardRevisionReports
{
    [AbpAuthorize]
    public class StandardRevisionAppService : GYSWPAppServiceBase, IStandardRevisionAppService
    {
        private readonly IRepository<Organization, long> _organizationRepository;
        private readonly IRepository<Document, Guid> _documentRepository;
        private readonly IRepository<DocRevision, Guid> _docRevisionRepository;
        private readonly IRepository<ClauseRevision, Guid> _clauseRevisionRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ApplyInfo, Guid> _applyInfoRepository;

        public StandardRevisionAppService(IRepository<Organization, long> organizationRepository
            , IRepository<Document, Guid> documentRepository
            , IRepository<DocRevision, Guid> docRevisionRepository
            , IRepository<ClauseRevision, Guid> clauseRevisionRepository
            , IRepository<Category> categoryRepository
            , IRepository<ApplyInfo, Guid> applyInfoRepository
            )
        {
            _documentRepository = documentRepository;
            _organizationRepository = organizationRepository;
            _docRevisionRepository = docRevisionRepository;
            _clauseRevisionRepository = clauseRevisionRepository;
            _categoryRepository = categoryRepository;
            _applyInfoRepository = applyInfoRepository;
        }

        //public async Task<List<StandardRevisionDto>> GetSearchStandardRevisions(StandardRevisionInputDto input)
        //{
        //    List<StandardRevisionDto> list = new List<StandardRevisionDto>();
        //    StandardRevisionDto standardRevisionDto = new StandardRevisionDto();
        //    //return null;
        //    var dept = await _organizationRepository.GetAll().Where(v => v.Id == input.DeptId).Select(v => new { v.Id, v.DepartmentName }).FirstOrDefaultAsync();
        //    var clauseRevisions = _clauseRevisionRepository.GetAll().Where(aa => aa.CreationTime >= input.StartTime && aa.CreationTime < input.EndTime);
        //    if (input.DeptId != 1)
        //    {
        //        var docRevision = _docRevisionRepository.GetAll().Where(aa => aa.DeptId == input.DeptId.ToString() && aa.CreationTime >= input.StartTime && aa.CreationTime < input.EndTime);
        //        int[] category = await _categoryRepository.GetAll().Where(v => v.DeptId == input.DeptId).Select(v => v.Id).ToArrayAsync();
        //        //var documents = _documentRepository.GetAll().Where(aa => aa.DeptIds.Contains(dept.Id.ToString()));
        //        var documents = _documentRepository.GetAll().Where(aa => category.Contains(aa.CategoryId));
        //        standardRevisionDto.DeptName = dept.DepartmentName;
        //        standardRevisionDto.TotalCurrentStandards = await documents.CountAsync(aa => aa.PublishTime < input.EndTime && aa.IsAction == true);
        //        standardRevisionDto.StandardAbolitionNumber = await documents.CountAsync(aa => aa.IsAction == false && aa.InvalidTime >= input.StartTime && aa.InvalidTime < input.EndTime);
        //        //standardRevisionDto.StandardAbolitionNumber = await documents.CountAsync();
        //        standardRevisionDto.StandardSettingNumber = await docRevision
        //            .CountAsync(aa => aa.RevisionType == RevisionType.标准制定 && aa.Status == RevisionStatus.审核通过);
        //        var standardSettingStripNumber = from clauseRevision in clauseRevisions
        //                                         join document in documents on clauseRevision.DocumentId equals document.Id into bb
        //                                         from aa in bb.DefaultIfEmpty()
        //                                         where clauseRevision.RevisionType == RevisionType.标准制定 && clauseRevision.Status == RevisionStatus.审核通过 && aa.DocNo != null
        //                                         select new
        //                                         {
        //                                             aa.Id
        //                                         };
        //        var StandardRevisionStripNumber = from clauseRevision in clauseRevisions
        //                                          join document in documents on clauseRevision.DocumentId equals document.Id into bb
        //                                          from aa in bb.DefaultIfEmpty()
        //                                          where clauseRevision.RevisionType != RevisionType.标准制定 && clauseRevision.Status == RevisionStatus.审核通过 && aa.DocNo != null
        //                                          select new
        //                                          {
        //                                              aa.Id
        //                                          };

        //        standardRevisionDto.StandardSettingStripNumber = await standardSettingStripNumber.CountAsync();
        //        //        standardRevisionDto.StandardRevisionNumber = await docRevision
        //        //.CountAsync(aa => aa.RevisionType != RevisionType.标准制定 && aa.Status == RevisionStatus.审核通过);
        //        standardRevisionDto.StandardRevisionNumber = await clauseRevisions.Where(v=>v.id).Select(v => v.DocumentId).Distinct().CountAsync();
        //        standardRevisionDto.StandardRevisionStripNumber = await StandardRevisionStripNumber.CountAsync();
        //        list.Add(standardRevisionDto);
        //        return list;
        //    }
        //    else
        //    {
        //        var docRevision = _docRevisionRepository.GetAll().Where(aa => aa.CreationTime >= input.StartTime && aa.CreationTime < input.EndTime);
        //        var documents = _documentRepository.GetAll();
        //        standardRevisionDto.DeptName = dept.DepartmentName;
        //        standardRevisionDto.TotalCurrentStandards = await documents.CountAsync(aa => aa.PublishTime < input.EndTime && aa.IsAction == true);
        //        standardRevisionDto.StandardAbolitionNumber = await documents.CountAsync(aa => aa.IsAction == false && aa.InvalidTime >= input.StartTime && aa.InvalidTime < input.EndTime);
        //        //standardRevisionDto.StandardAbolitionNumber = await documents.CountAsync();
        //        standardRevisionDto.StandardSettingNumber = await docRevision
        //            .CountAsync(aa => aa.RevisionType == RevisionType.标准制定 && aa.Status == RevisionStatus.审核通过);
        //        var standardSettingStripNumber = from clauseRevision in clauseRevisions
        //                                         join document in documents on clauseRevision.DocumentId equals document.Id into bb
        //                                         from aa in bb.DefaultIfEmpty()
        //                                         where clauseRevision.RevisionType == RevisionType.标准制定 && clauseRevision.Status == RevisionStatus.审核通过 && aa.DocNo != null
        //                                         select new
        //                                         {
        //                                             aa.Id
        //                                         };
        //        var StandardRevisionStripNumber = from clauseRevision in clauseRevisions
        //                                          join document in documents on clauseRevision.DocumentId equals document.Id into bb
        //                                          from aa in bb.DefaultIfEmpty()
        //                                          where clauseRevision.RevisionType != RevisionType.标准制定 && clauseRevision.Status == RevisionStatus.审核通过 && aa.DocNo != null
        //                                          select new
        //                                          {
        //                                              aa.Id
        //                                          };

        //        standardRevisionDto.StandardSettingStripNumber = await standardSettingStripNumber.CountAsync();
        //        //        standardRevisionDto.StandardRevisionNumber = await docRevision
        //        //.CountAsync(aa => aa.RevisionType != RevisionType.标准制定 && aa.Status == RevisionStatus.审核通过);
        //        standardRevisionDto.StandardRevisionNumber = await clauseRevisions.Select(v => v.DocumentId).Distinct().CountAsync();
        //        standardRevisionDto.StandardRevisionStripNumber = await StandardRevisionStripNumber.CountAsync();
        //        list.Add(standardRevisionDto);
        //        return list;
        //    }
        //}

        /// <summary>
        /// 制修订统计报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<StandardRevisionDto>> GetSearchStandardRevisions(StandardRevisionInputDto input)
        {
            List<StandardRevisionDto> list = new List<StandardRevisionDto>();
            StandardRevisionDto standardRevisionDto = new StandardRevisionDto();
            var dept = await _organizationRepository.GetAll().Where(v => v.Id == input.DeptId).Select(v => new { v.Id, v.DepartmentName }).FirstOrDefaultAsync();
            var clauseRevisions = _clauseRevisionRepository.GetAll().Where(aa => aa.CreationTime >= input.StartTime && aa.CreationTime < input.EndTime);
            if (input.DeptId != 1)
            {
                int[] category = await _categoryRepository.GetAll().Where(v => v.DeptId == input.DeptId).Select(v => v.Id).ToArrayAsync();
                var documents = _documentRepository.GetAll().Where(aa => category.Contains(aa.CategoryId));
                standardRevisionDto.DeptName = dept.DepartmentName;
                standardRevisionDto.TotalCurrentStandards = await documents.CountAsync(aa => aa.PublishTime < input.EndTime && aa.IsAction == true);
                //废止个数
                standardRevisionDto.StandardAbolitionNumber = await documents.CountAsync(aa => aa.IsAction == false && aa.InvalidTime >= input.StartTime && aa.InvalidTime < input.EndTime);
                //修订个数
                var revisionList = _applyInfoRepository.GetAll().Where(v => v.OperateType == OperateType.修订标准 && v.Status == ApplyStatus.审批通过 && v.ProcessingStatus == RevisionStatus.审核通过);
                Guid?[] revisionDocIds = await revisionList.Select(v => v.DocumentId).ToArrayAsync();
                standardRevisionDto.StandardRevisionNumber = await documents.CountAsync(v => revisionDocIds.Contains(v.Id));
                //制定个数
                var settingDoc = _docRevisionRepository.GetAll().Where(v => v.DeptId == input.DeptId.ToString() && v.Status == RevisionStatus.审核通过 && v.RevisionType == RevisionType.标准制定&& v.CreationTime >= input.StartTime && v.CreationTime < input.EndTime).Select(v => v.Id);
                standardRevisionDto.StandardSettingNumber = await settingDoc.CountAsync();
                //制定条数
                Guid[] settingDocIds = await settingDoc.ToArrayAsync();
                standardRevisionDto.StandardSettingStripNumber = await clauseRevisions.CountAsync(v => settingDocIds.Contains(v.DocumentId.Value));
                //修订条数
                Guid[] curDeptRevisionIds = await documents.Where(v => revisionDocIds.Contains(v.Id)).Select(v => v.Id).ToArrayAsync();
                standardRevisionDto.StandardRevisionStripNumber = await clauseRevisions.CountAsync(v => curDeptRevisionIds.Contains(v.DocumentId.Value));
            }
            else
            {
                var documents = _documentRepository.GetAll();
                standardRevisionDto.DeptName = dept.DepartmentName;
                standardRevisionDto.TotalCurrentStandards = await documents.CountAsync(aa => aa.PublishTime < input.EndTime && aa.IsAction == true);
                //废止个数
                standardRevisionDto.StandardAbolitionNumber = await documents.CountAsync(aa => aa.IsAction == false && aa.InvalidTime >= input.StartTime && aa.InvalidTime < input.EndTime);
                //修订个数
                var revisionList = _applyInfoRepository.GetAll().Where(v => v.OperateType == OperateType.修订标准 && v.Status == ApplyStatus.审批通过 && v.ProcessingStatus == RevisionStatus.审核通过);
                Guid?[] revisionDocIds = await revisionList.Select(v => v.DocumentId).ToArrayAsync();
                standardRevisionDto.StandardRevisionNumber = await documents.CountAsync(v => revisionDocIds.Contains(v.Id));
                //制定个数
                var settingDoc = _docRevisionRepository.GetAll().Where(v => v.Status == RevisionStatus.审核通过 && v.RevisionType == RevisionType.标准制定 && v.CreationTime >= input.StartTime && v.CreationTime < input.EndTime).Select(v => v.Id);
                standardRevisionDto.StandardSettingNumber = await settingDoc.CountAsync();
                //制定条数
                Guid[] settingDocIds = await settingDoc.ToArrayAsync();
                standardRevisionDto.StandardSettingStripNumber = await clauseRevisions.CountAsync(v => settingDocIds.Contains(v.DocumentId.Value));
                //修订条数
                Guid[] curDeptRevisionIds = await documents.Where(v => revisionDocIds.Contains(v.Id)).Select(v => v.Id).ToArrayAsync();
                standardRevisionDto.StandardRevisionStripNumber = await clauseRevisions.CountAsync(v => curDeptRevisionIds.Contains(v.DocumentId.Value));
            }
            list.Add(standardRevisionDto);
            return list;
        }
    }
}
