using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GYSWP.GYEnums;

namespace GYSWP.Indicators
{
    /// <summary>
    /// 检查指标
    /// </summary>
    [Table("Indicators")]
    public class Indicator : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 检查指标
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 指标释义
        /// </summary>
        [Required]
        [StringLength(2000)]
        public virtual string Paraphrase { get; set; }

        /// <summary>
        /// 测量方式
        /// </summary>
        [Required]
        [StringLength(2000)]
        public virtual string MeasuringWay { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 考核者人
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string CreatorEmpeeId { get; set; }

        /// <summary>
        /// 考核者快照
        /// </summary>
        [StringLength(50)]
        public virtual string CreatorEmpName { get; set; }

        /// <summary>
        /// 考核部门
        /// </summary>
        [Required]
        public virtual long CreatorDeptId { get; set; }

        /// <summary>
        /// 考核部门快照
        /// </summary>
        [StringLength(100)]
        public virtual string CreatorDeptName { get; set; }

        /// <summary>
        /// 被考核部门
        /// </summary>
        [Required]
        public virtual string DeptIds { get; set; }

        /// <summary>
        /// 被考核部门快照
        /// </summary>
        public virtual string DeptNames { get; set; }

        /// <summary>
        /// 预期值
        /// </summary>
        [Required]
        public virtual decimal ExpectedValue { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        [Required]
        public virtual CycleTime CycleTime { get; set; }

        /// <summary>
        /// 达成条件
        /// </summary>
        [Required]
        public virtual AchieveType AchieveType { get; set; }

        /// <summary>
        /// 来源标准Id
        /// </summary>
        [Required]
        public virtual Guid SourceDocId { get; set; }
        /// <summary>
        /// 截止时间 当天23:59:59
        /// </summary>
        //[Required]
        //public virtual DateTime EndTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public virtual bool IsAction { get; set; }
}
}
