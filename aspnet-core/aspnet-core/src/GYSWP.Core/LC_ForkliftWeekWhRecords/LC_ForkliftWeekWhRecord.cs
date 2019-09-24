using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_ForkliftWeekWhRecords
{
    /// <summary>
    /// 叉车周维护保养记录
    /// </summary>
    [Table("LC_ForkliftWeekWhRecords")]
    public class LC_ForkliftWeekWhRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 维保人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 监督人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string SuperintendentId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 叉车各零部件清洁、润滑
        /// </summary>
        public virtual bool? IsSpareParts { get; set; }

        /// <summary>
        /// 检查起重、刹车、灯光、喇叭、转向、行驶等各系
        /// </summary>
        public virtual bool? IsInspectQcEtc { get; set; }

        /// <summary>
        /// 限位器是否正常
        /// </summary>
        public virtual bool? IsLimitDeviceBad { get; set; }

        /// <summary>
        /// 检查润滑和油路系统，油管清洁无破损，底盘各部
        /// </summary>
        public virtual bool? IsInspectRhQcEtc { get; set; }

        /// <summary>
        /// 电解液是否充足，电池组无渗漏液
        /// </summary>
        public virtual bool? IsDjyDczBad { get; set; }

        /// <summary>
        /// 蓄电池接线柱是否有腐蚀
        /// </summary>
        public virtual bool? IsTerminalBad { get; set; }

        /// <summary>
        /// 各部位螺丝是否紧固
        /// </summary>
        public virtual bool? IsScrewBad { get; set; }

        /// <summary>
        /// 检查液压系统的油管顶杆上有无渗漏油现象
        /// </summary>
        public virtual bool? IsTubingJackBad { get; set; }

        /// <summary>
        /// 空气滤清器是否清洁
        /// </summary>
        public virtual bool? IsFilterBad { get; set; }

        /// <summary>
        /// 发现问题
        /// </summary>
        [StringLength(500)]
        public virtual string DiscoverProblems { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [StringLength(500)]
        public virtual string ProcessingResult { get; set; }
    }
}
