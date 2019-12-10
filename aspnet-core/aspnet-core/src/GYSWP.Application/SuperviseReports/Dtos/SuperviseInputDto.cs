using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.SuperviseReports.Dtos
{
    public class SuperviseInputDto
    {
        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public string UserName { get; set; }

        public long? DeptId { get; set; }
    }
}
