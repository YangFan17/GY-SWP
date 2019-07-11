

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_InStorageRecords;

namespace GYSWP.LC_InStorageRecords.Dtos
{
    public class CreateOrUpdateLC_InStorageRecordInput
    {
        [Required]
        public LC_InStorageRecordEditDto LC_InStorageRecord { get; set; }

    }
}