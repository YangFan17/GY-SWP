using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDingApproval.Dtos.ApprovalCommon
{
    public class ApprovalCallbackTestModel
    {
        public string Signature { get; set; }

        public string timestamp { get; set; }

        public string nonce { get; set; }
    }
}
