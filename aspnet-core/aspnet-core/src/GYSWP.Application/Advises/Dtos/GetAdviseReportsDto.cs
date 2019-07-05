using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.Advises.Dtos
{
    public class GetAdviseReportsDto
    {
        /// <summary>
        /// 合理化建议总数
        /// </summary>
        public int AdviseTotal { get; set; }

        /// <summary>
        /// 合理化建议采纳总数
        /// </summary>
        public int AdoptionAdviseTotal { get; set; }

        /// <summary>
        /// 合理化建议采纳比例
        /// </summary>
        public double Percentage { get; set; }
    }
}
