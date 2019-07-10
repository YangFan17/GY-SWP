

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.InspectionRecords;

namespace GYSWP.InspectionRecords.Dtos
{
    public class CreateOrUpdateInspectionRecordInput
    {
        [Required]
        public InspectionRecordEditDto InspectionRecord { get; set; }

    }
}