

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_ScanRecords;

namespace GYSWP.LC_ScanRecords.Dtos
{
    public class CreateOrUpdateLC_ScanRecordInput
    {
        [Required]
        public LC_ScanRecordEditDto LC_ScanRecord { get; set; }

    }
}