

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_ForkliftWeekWhRecords;

namespace GYSWP.LC_ForkliftWeekWhRecords.Dtos
{
    public class CreateOrUpdateLC_ForkliftWeekWhRecordInput
    {
        [Required]
        public LC_ForkliftWeekWhRecordEditDto LC_ForkliftWeekWhRecord { get; set; }

    }
    public class InsertLC_ForkliftWeekWhRecordInput
    {
        [Required]
        public LC_ForkliftWeekWhRecordDto LC_ForkliftWeekWhRecord { get; set; }
    }
}