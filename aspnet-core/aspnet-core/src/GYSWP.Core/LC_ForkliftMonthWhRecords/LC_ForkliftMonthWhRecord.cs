using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_ForkliftMonthWhRecords
{
    /// <summary>
    /// 叉车月维护保养记录
    /// </summary>
    [Table("LC_ForkliftMonthWhRecords")]
    public class LC_ForkliftMonthWhRecord : Entity<Guid>, IHasCreationTime
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
        /// 紧固各部位螺丝
        /// </summary>
        public virtual bool? IsScrewBad { get; set; }

        /// <summary>
        /// 叉车轮胎表面无严重裂痕，确保不打滑
        /// </summary>
        public virtual bool? IsTireSurfaceBad { get; set; }

        /// <summary>
        /// 电解液是否充足，电池组无渗漏液
        /// </summary>
        public virtual bool? IsDjyDczBad { get; set; }

        /// <summary>
        /// 叉车各零部件清洁、润滑
        /// </summary>
        public virtual bool? IsSpareParts { get; set; }

        /// <summary>
        /// 检查起重、刹车、灯光、喇叭、转向、照明、行驶等各系统
        /// </summary>
        public virtual bool? IsInspectQcEtc { get; set; }

        /// <summary>
        /// 限位器是否正常
        /// </summary>
        public virtual bool? IsLimitDeviceBad { get; set; }

        /// <summary>
        /// 检查润滑和油路系统，油管清洁无破损，底盘各部无渗漏油现象
        /// </summary>
        public virtual bool? IsInspectRhQcEtc { get; set; }

        /// <summary>
        /// 线路、管路无漏电、漏水现象
        /// </summary>
        public virtual bool? IsCircuitsBad { get; set; }

        /// <summary>
        /// 蓄电池接线柱是否有腐蚀
        /// </summary>
        public virtual bool? IsTerminalBad { get; set; }

        /// <summary>
        /// 检查液压系统的油管顶杆上有无渗漏油现象
        /// </summary>
        public virtual bool? IsTubingJackBad { get; set; }

        /// <summary>
        /// 行车电动机的电源控制是否灵敏有效
        /// </summary>
        public virtual bool? IsPowerControlBad { get; set; }

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
