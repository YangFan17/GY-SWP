using GYSWP.GYEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GYSWP.LC_ForkliftWeekWhRecords.Dtos
{
   public class LC_ForkliftWeekWhRecordDto: LC_ForkliftWeekWhRecordEditDto
    {
        //public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Type不能为空")]
        public LC_AttachmentType Type { get; set; }
        public string[] Path { get; set; }
        public string Remark { get; set; }

        public Guid? BLL { get; set; }
    }
}
