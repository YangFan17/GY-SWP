using System;
using System.Collections.Generic;
using System.Text;

namespace GYSWP.InspectReports.Dtos
{
    public class InspectDto
    {
        public string DeptName { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeePosition { get; set; }

        public string YearMonth { get; set; }

        /// <summary>
        /// 岗位适用条数
        /// </summary>
        public int PostUseNum { get; set; }
        /// <summary>
        /// 点击率=点击学习天数/20个工作日
        /// </summary>
        public decimal ClickRate { get; set; }
        /// <summary>
        /// 点击面（100%）=年点击学习条数/岗位适用的总条数
        /// </summary>
        public decimal SurfaceRate { get; set; }
        /// <summary>
        /// 点击量=月点击标准条款总数（1天1个条款只统计1次）
        /// </summary>
        public decimal ClickNum { get; set; }
    }
}
