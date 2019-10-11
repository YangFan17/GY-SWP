using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_ForkliftChecks
{
    /// <summary>
    /// 叉车检查表
    /// </summary>
    [Table("LC_ForkliftChecks")]
    public class LC_ForkliftCheck : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 到货外键
        /// </summary>
        public virtual Guid? TimeLogId { get; set; }

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
        /// 各部位润滑是否正常
        /// </summary>
        public virtual bool? IslubricatingOk { get; set; }

        /// <summary>
        /// 蓄电池接线有无腐蚀、松动
        /// </summary>
        public virtual bool? IsBatteryBad { get; set; }

        /// <summary>
        /// 转向、制动是否灵活
        /// </summary>
        public virtual bool? IsTurnOrBreakOk { get; set; }

        /// <summary>
        /// 车灯、喇叭是否正常
        /// </summary>
        public virtual bool? IsLightOrHornOk { get; set; }

        /// <summary>
        /// 电量是否充足
        /// </summary>
        public virtual bool? IsFullCharged { get; set; }

        /// <summary>
        /// 货叉升降是否正常
        /// </summary>
        public virtual bool? IsForkLifhOk { get; set; }

        /// <summary>
        /// 电量是否满足
        /// </summary>
        public virtual bool? IsRunFullCharged { get; set; }

        /// <summary>
        /// 刹车、喇叭有无异常
        /// </summary>
        public virtual bool? IsRunTurnOrBreakOk { get; set; }

        /// <summary>
        /// 货叉升降有无异常
        /// </summary>
        public virtual bool? IsRunLightOrHornOk { get; set; }

        /// <summary>
        /// 运行声音有无异响
        /// </summary>
        public virtual bool? IsRunSoundOk { get; set; }

        /// <summary>
        /// 停放是否规范到位
        /// </summary>
        public virtual bool? IsParkStandard { get; set; }

        /// <summary>
        /// 制动是否拉紧、电源是否关闭
        /// </summary>
        public virtual bool? IsShutPower { get; set; }

        /// <summary>
        /// 是否需要补充电量
        /// </summary>
        public virtual bool? IsNeedCharge { get; set; }

        /// <summary>
        /// 各部位是否进行清洁
        /// </summary>
        public virtual bool? IsClean { get; set; }

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
