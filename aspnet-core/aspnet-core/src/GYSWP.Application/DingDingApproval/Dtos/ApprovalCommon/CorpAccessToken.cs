using System.Runtime.Serialization;

namespace GYSWP.DingDingApproval.Dtos.ApprovalCommon
{
    public class CorpAccessToken
    {
        [DataMember(Order = 0)]
        public string access_token { get; set; }
        [DataMember(Order = 1)]
        public int expires_in { get; set; }
    }
}
