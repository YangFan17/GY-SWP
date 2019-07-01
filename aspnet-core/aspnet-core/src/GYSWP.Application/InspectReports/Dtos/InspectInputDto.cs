using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.InspectReports.Dtos
{
    public class InspectInputDto
    {
        public DateTime Month { get; set; }

        public string[] Depts { get; set; }

        public string UserName { get; set; }

        public DateTime BeginTime
        {
            get
            {
                return new DateTime(Month.Year, Month.Month, 1);
            }
        }

        public DateTime EndTime
        {
            get
            {
                return new DateTime(Month.Year, Month.Month + 1, 1);
            }
        }
    }
}
