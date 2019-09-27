using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_KyjFunctionRecords
{
    /// <summary>
    /// 空压机运行记录
    /// </summary>
    [Table("LC_KyjFunctionRecords")]
    public class LC_KyjFunctionRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 维保人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string ResponsibleName{ get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 监管人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string SupervisorName { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime? UseTime { get; set; }

        /// <summary>
        /// 停用时间
        /// </summary>
        public virtual DateTime? DownTime { get; set; }

        /// <summary>
        /// 电气系统装置是否完整
        /// </summary>
        public virtual bool? IsDeviceComplete { get; set; }

        /// <summary>
        /// 输气管线是否完好
        /// </summary>
        public virtual bool? IsPipelineOk { get; set; }

        /// <summary>
        /// 润滑油位是否正常
        /// </summary>
        public virtual bool? IsLubricatingOilOk { get; set; }

        /// <summary>
        /// 通风扇是否打开
        /// </summary>
        public virtual bool? IsVentilatingFanOpen { get; set; }

        /// <summary>
        /// 安全阀是否有效
        /// </summary>
        public virtual bool? IsSafetyValveOk { get; set; }

        /// <summary>
        /// 压力指示是否正常
        /// </summary>
        public virtual bool? IsPressureNormal { get; set; }

        /// <summary>
        /// PC板显示是否正常
        /// </summary>
        public virtual bool? IsPCShowNormal { get; set; }

        /// <summary>
        /// 运转声音有无异响
        /// </summary>
        public virtual bool? IsRunningSoundBad { get; set; }

        /// <summary>
        /// 有无漏气、漏水、漏油
        /// </summary>
        public virtual bool? IsLsLqLyOk { get; set; }

        /// <summary>
        /// 自动排水阀是否正常
        /// </summary>
        public virtual bool? IsDrainValveOk { get; set; }

        /// <summary>
        /// 电源是否关闭
        /// </summary>
        public virtual bool? IsPowerSupplyClose { get; set; }

        /// <summary>
        /// 设备是否进行清洁
        /// </summary>
        public virtual bool? IsDeviceClean { get; set; }

        /// <summary>
        /// 故障描述及处理
        /// </summary>
        [StringLength(500)]
        public virtual string Desc { get; set; }

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
