

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_KyjFunctionRecords;

namespace GYSWP.LC_KyjFunctionRecords.Dtos
{
    public class CreateOrUpdateLC_KyjFunctionRecordInput
    {
        [Required]
        public LC_KyjFunctionRecordEditDto LC_KyjFunctionRecord { get; set; }

    }
}