using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_SsjWeekWhByRecords
{
    /// <summary>
    /// 伸缩式输送机周维护保养记录
    /// </summary>
    [Table("LC_SsjWeekWhByRecords")]
    public class LC_SsjWeekWhByRecord : Entity<Guid>, IHasCreationTime
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
        /// 传输机各传动部位润滑
        /// </summary>
        public virtual bool? IsPartLubrication { get; set; }

        /// <summary>
        /// 检查传输机机架承重部件有无变形
        /// </summary>
        public virtual bool? IsShapeBad { get; set; }

        /// <summary>
        /// 检查设备螺栓有无松动
        /// </summary>
        public virtual bool? IsBoltBad { get; set; }

        /// <summary>
        /// 检查设备控制按钮是否正常
        /// </summary>
        public virtual bool? IsButtonBad { get; set; }

        /// <summary>
        /// 电源线路是否老化、裸露
        /// </summary>
        public virtual bool? IsPowerCircuitBad { get; set; }

        /// <summary>
        /// 网络传输线有无松动
        /// </summary>
        public virtual bool? IsLineBad { get; set; }

        /// <summary>
        /// 检查轴承运转是否正常
        /// </summary>
        public virtual bool? IsBearingRunningOk { get; set; }

        /// <summary>
        /// 调整传送带、传动链条张紧度
        /// </summary>
        public virtual bool? IsChainTensionBad { get; set; }

        /// <summary>
        /// 检查传输皮带有无划伤、断裂
        /// </summary>
        public virtual bool? IsBeltBad { get; set; }

        /// <summary>
        /// 检查传输机的电器设备、安全防护装置处于完好状态
        /// </summary>
        public virtual bool? IseviceBad { get; set; }

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
