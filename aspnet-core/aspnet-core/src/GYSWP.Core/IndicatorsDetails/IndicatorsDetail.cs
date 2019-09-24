using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GYSWP.GYEnums;

namespace GYSWP.IndicatorsDetails
{
    /// <summary>
    /// 考核详情
    /// </summary>
    [Table("IndicatorsDetails")]
    public class IndicatorsDetail : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 检查指标外键
        /// </summary>
        [Required]
        public virtual Guid IndicatorsId { get; set; }

        /// <summary>
        /// 实际指标
        /// </summary>
        public virtual decimal? ActualValue { get; set; }

        /// <summary>
        /// 考核结果（未填写/已达成/未达成）
        /// </summary>
        [Required]
        public virtual IndicatorStatus Status { get; set; }

        /// <summary>
        /// 填写人id
        /// </summary>
        [StringLength(200)]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 填写人快照
        /// </summary>
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 被考核部门
        /// </summary>
        [Required]
        public virtual long DeptId { get; set; }

        /// <summary>
        /// 被考核部门快照
        /// </summary>
        [StringLength(100)]
        public virtual string DeptName { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime? CompleteTime { get; set; }
        /// <summary>
        /// 截止时间 当天23:59:59
        /// </summary>
        [Required]
        public virtual DateTime EndTime { get; set; }
    }
}
