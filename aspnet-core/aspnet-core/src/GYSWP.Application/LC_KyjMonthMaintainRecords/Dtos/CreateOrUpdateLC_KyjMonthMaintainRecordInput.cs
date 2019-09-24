

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_KyjMonthMaintainRecords;

namespace GYSWP.LC_KyjMonthMaintainRecords.Dtos
{
    public class CreateOrUpdateLC_KyjMonthMaintainRecordInput
    {
        [Required]
        public LC_KyjMonthMaintainRecordEditDto LC_KyjMonthMaintainRecord { get; set; }

    }
}