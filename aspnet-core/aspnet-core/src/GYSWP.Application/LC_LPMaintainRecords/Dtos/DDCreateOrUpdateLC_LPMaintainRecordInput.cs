using GYSWP.DocAttachments.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GYSWP.LC_LPMaintainRecords.Dtos
{
    public class DDCreateOrUpdateLC_LPMaintainRecordInput
    {
        [Required]
        public LC_LPMaintainRecordEditDto LC_LPMaintainRecord { get; set; }


        [Required]
        public DingDingAttachmentEditDto DDAttachmentEditDto { get; set; }
    }
}
