

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_SsjWeekWhByRecords;

namespace GYSWP.LC_SsjWeekWhByRecords.Dtos
{
    public class CreateOrUpdateLC_SsjWeekWhByRecordInput
    {
        [Required]
        public LC_SsjWeekWhByRecordEditDto LC_SsjWeekWhByRecord { get; set; }

    }
}