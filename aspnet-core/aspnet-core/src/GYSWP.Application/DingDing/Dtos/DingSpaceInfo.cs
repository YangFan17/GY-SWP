using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDing.Dtos
{
    /// <summary>
    /// 审批钉盘返回结果
    /// </summary>
    public class DingSpaceInfo
    {
        public Space_Id result { get; set; }
        public long code { get; set; }
        public bool success { get; set; }
        public string errmsg { get; set; }
    }

    public class Space_Id
    {
        public string space_id { get; set; }
    }
}
