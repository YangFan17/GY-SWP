

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_SortingWeekRecords;

namespace GYSWP.LC_SortingWeekRecords.Dtos
{
    public class CreateOrUpdateLC_SortingWeekRecordInput
    {
        [Required]
        public LC_SortingWeekRecordEditDto LC_SortingWeekRecord { get; set; }

    }

    public class InsertLC_SortingWeekRecordInput
    {
        [Required]
        public LC_SortingWeekRecordDto LC_SortingWeekRecord { get; set; }
    }
}