using GYSWP.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.DingDing.Dtos
{
    public class DingUserInfoDto : DingBase
    {
        public string userid { get; set; }

        public int sys_level { get; set; }

        public bool is_sys { get; set; }

        public string deviceId { get; set; }

    }
}
