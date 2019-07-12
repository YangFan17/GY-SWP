

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_OutScanRecords;

namespace GYSWP.LC_OutScanRecords.Dtos
{
    public class CreateOrUpdateLC_OutScanRecordInput
    {
        [Required]
        public LC_OutScanRecordEditDto LC_OutScanRecord { get; set; }

    }
}