using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.Advises.Dtos
{
     public class GetAdviseReportInputDto
    {
        public DateTime Month { get; set; }
        public long DeptId { get; set; }

        public DateTime? StartTime
        {
            get
            {
                return new DateTime(Month.Year, Month.Month, 1);
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return new DateTime(Month.Year, Month.Month + 1, 1);
            }
        }
    }
}
