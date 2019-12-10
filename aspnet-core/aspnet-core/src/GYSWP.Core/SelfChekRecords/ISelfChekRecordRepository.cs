using Abp.Domain.Repositories;
using GYSWP.SelfChekRecords;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GYSWP.SelfChekRecords.Dtos;

namespace GYSWP.SelfChekRecordss
{
    public interface ISelfChekRecordRepository : IRepository<SelfChekRecord, Guid>
    {
        Task<List<InspectDto>> GetSearchInspectReports(InspectInputDto input);
        Task<InspectDto> GetTotalInspectReports(InspectInputDto input);
    }
}
