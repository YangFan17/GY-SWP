using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_TeamSafetyActivitys
{
    /// <summary>
    /// 班组安全活动表
    /// </summary>
    [Table("LC_TeamSafetyActivitys")]
    public class LC_TeamSafetyActivity : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 分拣外键
        /// </summary>
             public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 岗前安全例会
        /// </summary>
        [StringLength(500)]
        public virtual string SafetyMeeting { get; set; }

        /// <summary>
        /// 劳动保护用品穿戴是否整齐
        /// </summary>
        [Required]
        public virtual bool IsSafeEquipOk { get; set; }

        /// <summary>
        /// 人员身体状态有无异常
        /// </summary>
        [Required]
        public virtual bool IsEmpHealth { get; set; }

        /// <summary>
        /// 通道机、立式机各烟仓条烟处于位置
        /// </summary>
        [Required]
        public virtual bool IsTdjOrLsjOk { get; set; }

        /// <summary>
        /// 叉车通道是否畅通
        /// </summary>
        [Required]
        public virtual bool IsAisleOk { get; set; }

        /// <summary>
        /// 安全出口是否堵塞
        /// </summary>
        [Required]
        public virtual bool IsExitBad { get; set; }

        /// <summary>
        /// 消防设施是否堵塞
        /// </summary>
        [Required]
        public virtual bool IsFireEquipBad { get; set; }

        /// <summary>
        /// 安全标识是否清晰
        /// </summary>
        [Required]
        public virtual bool IsSafeMarkClean { get; set; }

        /// <summary>
        /// 安全标识有无脱落
        /// </summary>
        [Required]
        public virtual bool IsSafeMarkFall { get; set; }

        /// <summary>
        /// 员工安全建议
        /// </summary>
        public virtual string EmpSafeAdvice { get; set; }

        /// <summary>
        /// 常规烟分拣量：条
        /// </summary>
        public virtual int? CommonCigaretNum { get; set; }

        /// <summary>
        /// 异型烟分拣量：条
        /// </summary>
        public virtual int? ShapedCigaretNum { get; set; }

        /// <summary>
        /// 分拣开始时间
        /// </summary>
        public virtual DateTime? BeginSortTime { get; set; }

        /// <summary>
        /// 分拣结束时间
        /// </summary>
        public virtual DateTime? EndSortTime { get; set; }

        /// <summary>
        /// 正常停机时间
        /// </summary>
        public virtual DateTime? NormalStopTime { get; set; }

        /// <summary>
        /// 故障停机时间
        /// </summary>
        public virtual DateTime? AbnormalStopTime { get; set; }

        /// <summary>
        /// 有无危险源、风险点
        /// </summary>
        [Required]
        public virtual bool IsNotDanger { get; set; }

        /// <summary>
        /// 有无非工作人员出入场
        /// </summary>
        [Required]
        public virtual bool IsOtherAdmittance { get; set; }

        /// <summary>
        /// 有无违章作业现象
        /// </summary>
        [Required]
        public virtual bool IsViolation { get; set; }

        /// <summary>
        /// 分拣结束，电源、气源是否关闭
        /// </summary>
        [Required]
        public virtual bool IsElcOrGasShut { get; set; }

        /// <summary>
        /// 车间门窗是否关闭
        /// </summary>
        [Required]
        public virtual bool IsCloseWindow { get; set; }

        /// <summary>
        /// 现场安全监管
        /// </summary>
        [StringLength(500)]
        public virtual string SafeSupervision { get; set; }

        /// <summary>
        /// 岗位安全责任人
        /// </summary>
        [StringLength(500)]
        public virtual string ResponsibleName { get; set; }

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
