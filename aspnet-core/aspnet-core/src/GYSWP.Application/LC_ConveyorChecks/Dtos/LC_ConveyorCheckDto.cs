using Abp.AutoMapper;
using GYSWP.GYEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.LC_ConveyorChecks.Dtos
{
    [AutoMapFrom(typeof(LC_ConveyorCheck))]
    public class LC_ConveyorCheckDto : LC_ConveyorCheckEditDto
    {
        public LC_AttachmentType Type { get; set; }
        public string[] Path { get; set; }
        public string Remark { get; set; }

        public Guid? BLL
        {
            get; set;
        }
    }
}
