

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_QualityRecords;

namespace GYSWP.LC_QualityRecords.Dtos
{
    public class CreateOrUpdateLC_QualityRecordInput
    {
        [Required]
        public LC_QualityRecordEditDto LC_QualityRecord { get; set; }

    }
}