using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.SuperviseReports.Dtos
{
    public class SuperviseDto
    {
        public string UserName { get; set; }

        public string Position { get; set; }

        public string ExamineTitle { get; set; }

        public DateTime ExamineTime { get; set; }

        public int ExamineNum { get; set; }

        public int NotUpNum { get; set; }

        public int OkNum { get; set; }

        public int NotFinished { get; set; }

        public string ImplementRate
        {
            get
            {
                if (ExamineNum == 0)
                {
                    return "0.00%";
                }

                return Math.Round((ExamineNum - NotFinished) / (ExamineNum * 1.0m)*100, 2) + "%";
            }
        }

        public string ReachRate
        {
            get
            {
                if (ExamineNum == 0)
                {
                    return "0.00%";
                }

                return Math.Round(OkNum / (ExamineNum * 1.0m) * 100, 2) + "%";
            }
        }
    }
}
