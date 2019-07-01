using Abp.Domain.Repositories;
using GYSWP.SelfChekRecords.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GYSWP.SelfChekRecords
{
    public interface ISelfChekRecordRepository : IRepository<SelfChekRecord, Guid>
    {
        Task<List<InspectDto>> GetSearchInspectReports(InspectInputDto input);
    }
}
