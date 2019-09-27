using GYSWP.DocAttachments.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GYSWP.LC_KyjWeekMaintainRecords.Dtos
{
     public class DDCreateOrUpdateLC_KyjWeekMaintainRecordInput
    {
        [Required]
        public LC_KyjWeekMaintainRecordEditDto LC_KyjWeekMaintainRecord { get; set; }

        [Required]
        public DingDingAttachmentEditDto DDAttachmentEditDto { get; set; }
    }
}
