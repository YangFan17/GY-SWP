

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_ForkliftMonthWhRecords;

namespace GYSWP.LC_ForkliftMonthWhRecords.Dtos
{
    public class CreateOrUpdateLC_ForkliftMonthWhRecordInput
    {
        [Required]
        public LC_ForkliftMonthWhRecordEditDto LC_ForkliftMonthWhRecord { get; set; }

    }

    public class InsertLC_ForkliftMonthWhRecordInput
    {
        [Required]
        public LC_ForkliftMonthWhRecordDto LC_ForkliftMonthWhRecord { get; set; }
    }
}