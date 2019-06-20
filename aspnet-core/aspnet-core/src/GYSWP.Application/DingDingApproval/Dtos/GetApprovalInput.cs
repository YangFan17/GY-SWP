using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDingApproval.Dtos
{
    public class GetApprovalInput
    {
        public string Reason { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
