using GYSWP.DocAttachments.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GYSWP.LC_KyjFunctionRecords.Dtos
{
    public class DDCreateOrUpdateLC_KyjFunctionRecordInput
    {
        [Required]
        public LC_KyjFunctionRecordEditDto LC_KyjFunctionRecord { get; set; }

        [Required]
        public DingDingAttachmentEditDto DDAttachmentEditDto { get; set; }
    }
}
