

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_SsjMonthWhByRecords;

namespace GYSWP.LC_SsjMonthWhByRecords.Dtos
{
    public class CreateOrUpdateLC_SsjMonthWhByRecordInput
    {
        [Required]
        public LC_SsjMonthWhByRecordEditDto LC_SsjMonthWhByRecord { get; set; }

    }
    public class InsertLC_SsjMonthWhByRecordInput
    {
        [Required]
        public LC_SsjMonthWhByRecordDto LC_SsjMonthWhByRecord { get; set; }
    }
}