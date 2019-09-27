using Abp.AutoMapper;
using GYSWP.GYEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.LC_ForkliftChecks.Dtos
{
    [AutoMapFrom(typeof(LC_ForkliftCheck))]
    public class LC_ForkliftCheckDto: LC_ForkliftCheckEditDto
    {
        public LC_AttachmentType Type { get; set; }
        public string[] Path { get; set; }
        public string Remark { get; set; }

        public Guid? BLL
        {
            get; set;
        }

        public string StartTimeFormat { get; set; }

        public string EndTimeFormat { get; set; }


    }
}
