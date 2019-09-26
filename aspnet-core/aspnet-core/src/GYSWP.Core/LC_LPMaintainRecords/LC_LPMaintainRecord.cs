using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_LPMaintainRecords
{
    /// <summary>
    /// 桅柱式升降平台维护保养记录
    /// </summary>
    [Table("LC_LPMaintainRecords")]
    public class LC_LPMaintainRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 设备编号
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string DeviceID { get; set; }

        /// <summary>
        /// 维保人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string ResponsibleName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 监督人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string SupervisorName { get; set; }

        /// <summary>
        /// 检查电源线路有无破损
        /// </summary>
        public virtual bool? IsLineDamaged { get; set; }

        /// <summary>
        /// 控制按钮是否灵敏有效
        /// </summary>
        public virtual bool? IsControlButOk { get; set; }

        /// <summary>
        /// 急停开关是否灵敏有效
        /// </summary>
        public virtual bool? IsScramSwitchOk { get; set; }

        /// <summary>
        /// 设备清洁、润滑
        /// </summary>
        public virtual bool? IsDeviceCleaning { get; set; }

        /// <summary>
        /// 检查平台护栏是否完好
        /// </summary>
        public virtual bool? IsGuardrailOk { get; set; }

        /// <summary>
        /// 外伸支腿是否完好
        /// </summary>
        public virtual bool? IsOutriggerLegOk { get; set; }

        /// <summary>
        /// 检查传动链组的松紧度
        /// </summary>
        public virtual bool? IsChainGroupTightness { get; set; }

        /// <summary>
        /// 各部位螺丝是否紧固
        /// </summary>
        public virtual bool? IsScrewFastening { get; set; }

        /// <summary>
        /// 检查液压油位是否满足
        /// </summary>
        public virtual bool? IsOilLevelSatisfy { get; set; }

        /// <summary>
        /// 电机运转是否正常
        /// </summary>
        public virtual bool? IsMotorRunning { get; set; }

        /// <summary>
        /// 检查举高机底座有无渗漏油
        /// </summary>
        public virtual bool? IsLiftingMachineBad { get; set; }

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

        /// <summary>
        /// 员工id
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        ///  员工快照
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }
    }
}
