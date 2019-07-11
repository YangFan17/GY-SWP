using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GYSWP.LC_ConveyorChecks
{
    /// <summary>
    /// 伸缩式输送机检查表
    /// </summary>
    [Table("LC_ConveyorChecks")]
    public class LC_ConveyorCheck : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 到货外键
        /// </summary>
        [Required]
        public virtual Guid TimeLogId { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual string EquiNo { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        [StringLength(100)]
        public virtual string ResponsibleName { get; set; }

        /// <summary>
        /// 监管人
        /// </summary>
        [StringLength(50)]
        public virtual string SupervisorName { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public virtual DateTime? RunTime { get; set; }

        /// <summary>
        /// 开机时间
        /// </summary>
        public virtual DateTime? BeginTime { get; set; }

        /// <summary>
        /// 停机时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 设备表面有无异物
        /// </summary>
        [Required]
        public virtual bool IsEquiFaceClean { get; set; }

        /// <summary>
        /// 固定支架是否完好
        /// </summary>
        [Required]
        public virtual bool IsSteadyOk { get; set; }

        /// <summary>
        /// 设备螺栓是否紧固
        /// </summary>
        [Required]
        public virtual bool IsScrewOk { get; set; }

        /// <summary>
        /// 控制按钮是否正常
        /// </summary>
        [Required]
        public virtual bool IsButtonOk { get; set; }

        /// <summary>
        /// 电源线路是否老化、裸露
        /// </summary>
        [Required]
        public virtual bool IsElcLineBad { get; set; }

        /// <summary>
        /// 皮带是否跑偏
        /// </summary>
        [Required]
        public virtual bool IsBeltSlant { get; set; }

        /// <summary>
        /// 轴承运转是否正常
        /// </summary>
        [Required]
        public virtual bool IsBearingOk { get; set; }

        /// <summary>
        /// 运行声音有无异响
        /// </summary>
        [Required]
        public virtual bool IsSoundOk { get; set; }

        /// <summary>
        /// 电机运行是否正常
        /// </summary>
        [Required]
        public virtual bool IsMotor { get; set; }

        /// <summary>
        /// 电源是否断电
        /// </summary>
        [Required]
        public virtual bool IsShutPower { get; set; }

        /// <summary>
        /// 传输皮带有无划伤、断裂
        /// </summary>
        [Required]
        public virtual bool IsBeltBad { get; set; }

        /// <summary>
        /// 设备是否进行清洁
        /// </summary>
        [Required]
        public virtual bool IsClean { get; set; }

        /// <summary>
        /// 故障描述和处理
        /// </summary>
        [StringLength(2000)]
        public virtual string Troubleshooting { get; set; }

        /// <summary>
        /// 员工id
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 员工快照
        /// </summary>
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }
}
