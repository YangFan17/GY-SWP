

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_LPFunctionRecords;

namespace GYSWP.LC_LPFunctionRecords.Dtos
{
    public class CreateOrUpdateLC_LPFunctionRecordInput
    {
        [Required]
        public LC_LPFunctionRecordEditDto LC_LPFunctionRecord { get; set; }

    }
}