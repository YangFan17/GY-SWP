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

        public StandardRevisionAppService(IRepository<Organization, long> organizationRepository
            , IRepository<Document, Guid> documentRepository
            , IRepository<DocRevision, Guid> docRevisionRepository
            , IRepository<ClauseRevision, Guid> clauseRevisionRepository
            , IRepository<Category> categoryRepository
            )
        {
            _documentRepository = documentRepository;
            _organizationRepository = organizationRepository;
            _docRevisionRepository = docRevisionRepository;
            _clauseRevisionRepository = clauseRevisionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<StandardRevisionDto>> GetSearchStandardRevisions(StandardRevisionInputDto input)
        {
            List<StandardRevisionDto> list = new List<StandardRevisionDto>();
            StandardRevisionDto standardRevisionDto = new StandardRevisionDto();
            //return null;
            var dept = await _organizationRepository.GetAll().Where(v=>v.Id == input.DeptId).Select(v=>new { v.Id, v.DepartmentName }).FirstOrDefaultAsync();
            int[] category = await  _categoryRepository.GetAll().Where(v => v.DeptId == input.DeptId).Select(v=>v.Id).ToArrayAsync();
            var clauseRevisions = _clauseRevisionRepository.GetAll().Where(aa => aa.CreationTime >= input.StartTime && aa.CreationTime < input.EndTime);
            var docRevision = _docRevisionRepository.GetAll().Where(aa => aa.DeptId == input.DeptId.ToString() && aa.CreationTime >= input.StartTime && aa.CreationTime < input.EndTime);
            //var documents = _documentRepository.GetAll().Where(aa => aa.DeptIds.Contains(dept.Id.ToString()));
            var documents = _documentRepository.GetAll().Where(aa => category.Contains(aa.CategoryId));
            standardRevisionDto.DeptName = dept.DepartmentName;
            standardRevisionDto.TotalCurrentStandards = await documents.CountAsync(aa => aa.PublishTime < input.EndTime && aa.IsAction == true);
            standardRevisionDto.StandardAbolitionNumber = await documents.CountAsync(aa => aa.IsAction == false && aa.InvalidTime >= input.StartTime && aa.InvalidTime < input.EndTime);
            //standardRevisionDto.StandardAbolitionNumber = await documents.CountAsync();
            standardRevisionDto.StandardSettingNumber = await docRevision
                .CountAsync(aa => aa.RevisionType == RevisionType.标准制定 && aa.Status == RevisionStatus.审核通过);
            var standardSettingStripNumber = from clauseRevision in clauseRevisions
                                             join document in documents on clauseRevision.DocumentId equals document.Id into bb
                                             from aa in bb.DefaultIfEmpty()
                                             where clauseRevision.RevisionType == RevisionType.标准制定 && clauseRevision.Status == RevisionStatus.审核通过 && aa.DocNo != null
                                             select new
                                             {
                                                 aa.Id
                                             };
            var StandardRevisionStripNumber = from clauseRevision in clauseRevisions
                                              join document in documents on clauseRevision.DocumentId equals document.Id into bb
                                              from aa in bb.DefaultIfEmpty()
                                              where clauseRevision.RevisionType != RevisionType.标准制定 && clauseRevision.Status == RevisionStatus.审核通过 && aa.DocNo != null
                                              select new
                                              {
                                                  aa.Id
                                              };

            standardRevisionDto.StandardSettingStripNumber = await standardSettingStripNumber.CountAsync();
            standardRevisionDto.StandardRevisionNumber = await docRevision
    .CountAsync(aa => aa.RevisionType != RevisionType.标准制定 && aa.Status == RevisionStatus.审核通过);
            standardRevisionDto.StandardRevisionStripNumber = await StandardRevisionStripNumber.CountAsync();
            list.Add(standardRevisionDto);
            return list;
        }
    }
}
