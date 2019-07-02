using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.StandardRevisionReports.Dtos
{
     public class StandardRevisionDto
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 现行标准总数
        /// </summary>
        public int? TotalCurrentStandards { get; set; }

        /// <summary>
        /// 标准制定个数
        /// </summary>
        public int? StandardSettingNumber { get; set; }

        /// <summary>
        /// 标准制定条数
        /// </summary>
        public int? StandardSettingStripNumber { get; set; }

        /// <summary>
        /// 标准修订个数
        /// </summary>
        public int? StandardRevisionNumber { get; set; }

        /// <summary>
        /// 标准修订条数
        /// </summary>
        public int? StandardRevisionStripNumber { get; set; }

        /// <summary>
        /// 标准废除条数
        /// </summary>
        public int? StandardAbolitionNumber { get; set; }
    }
}
