

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_KyjWeekMaintainRecords;

namespace GYSWP.LC_KyjWeekMaintainRecords.Dtos
{
    public class CreateOrUpdateLC_KyjWeekMaintainRecordInput
    {
        [Required]
        public LC_KyjWeekMaintainRecordEditDto LC_KyjWeekMaintainRecord { get; set; }

    }
}