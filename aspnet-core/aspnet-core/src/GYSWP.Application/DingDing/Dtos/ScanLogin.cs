using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDing.Dtos
{
    public class ScanLogin
    {
        public string tmp_auth_code { get; set; }
    }
    public class ScanLoginInfo
    {
        public string errcode { get; set; }
        public First user_info { get; set; }
    }

    public class First {
        public string unionId { get; set; }
    }
}
