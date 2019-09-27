using GYSWP.DocAttachments.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GYSWP.LC_KyjMonthMaintainRecords.Dtos
{
    public class DDCreateOrUpdateLC_KyjMonthMaintainRecordInput
    {
        [Required]
        public LC_KyjMonthMaintainRecordEditDto LC_KyjMonthMaintainRecord { get; set; }

        [Required]
        public DingDingAttachmentEditDto DDAttachmentEditDto { get; set; }
    }
}
