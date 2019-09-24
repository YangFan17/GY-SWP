using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.StandardRevisionReports
{
    public class StandardRevisionInputDto
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
                return StartTime.Value.AddMonths(1);
            }
        }
    }
}
