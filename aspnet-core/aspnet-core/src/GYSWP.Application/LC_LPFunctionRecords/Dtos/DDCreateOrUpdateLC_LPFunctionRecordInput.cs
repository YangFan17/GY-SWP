using GYSWP.DocAttachments.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GYSWP.LC_LPFunctionRecords.Dtos
{
    public class DDCreateOrUpdateLC_LPFunctionRecordInput
    {
        [Required]
        public LC_LPFunctionRecordEditDto LC_LPFunctionRecord { get; set; }

        [Required]
        public DingDingAttachmentEditDto DDAttachmentEditDto { get; set; }
    }
}
