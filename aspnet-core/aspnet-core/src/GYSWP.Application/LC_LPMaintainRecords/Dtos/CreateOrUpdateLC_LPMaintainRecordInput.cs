

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_LPMaintainRecords;

namespace GYSWP.LC_LPMaintainRecords.Dtos
{
    public class CreateOrUpdateLC_LPMaintainRecordInput
    {
        [Required]
        public LC_LPMaintainRecordEditDto LC_LPMaintainRecord { get; set; }

    }
}