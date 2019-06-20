using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDingApproval.Dtos
{
    public class CallBackRequest
    {
        public string url { get; set; }
        public string aes_key { get; set; }
        public string token { get; set; }
        public List<string> call_back_tag { get; set; }
    }
}
