

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_SortingMonthRecords;

namespace GYSWP.LC_SortingMonthRecords.Dtos
{
    public class CreateOrUpdateLC_SortingMonthRecordInput
    {
        [Required]
        public LC_SortingMonthRecordEditDto LC_SortingMonthRecord { get; set; }

    }

    public class InsertLC_SortingMonthRecordInput
    {
        [Required]
        public LC_SortingMonthRecordDto LC_SortingMonthRecord { get; set; }
    }
}