using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.SuperviseReports.Dtos
{
    public class IndicatorSuperviseDto
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 未填写数量
        /// </summary>
        public int NotFillNum { get; set; }

        /// <summary>
        /// 已达成数量
        /// </summary>
        public int OkNum { get; set; }

        /// <summary>
        /// 未达成数量
        /// </summary>
        public int NotFinishedNum { get; set; }

        /// <summary>
        /// 指标总数
        /// </summary>
        public int IndicatorTotal { get; set; }
    }
}
