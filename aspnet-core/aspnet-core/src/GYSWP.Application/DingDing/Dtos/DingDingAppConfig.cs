using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDing.Dtos
{
    public class DingDingAppConfig
    {
        public string CorpId { get; set; }

        public string Appkey { get; set; }

        public string Appsecret { get; set; }

        public int AgentID { get; set; }
    }

    public class DingDingConfigCode
    {
        public static string CorpId = "CorpId";

        public static string Appkey = "Appkey";

        public static string Appsecret = "Appsecret";

        public static string AgentID = "AgentID";
    }

    public enum DingDingAppEnum
    {
        标准化工作平台 = 1,
    }
}
