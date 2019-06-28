using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GYSWP.GYEnums;

namespace GYSWP.ExamineFeedbacks
{
    [Table("ExamineFeedbacks")]
    public class ExamineFeedback : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 反馈考核类型（标准自查/指标考核）
        /// </summary>
        [Required]
        public virtual FeedType Type { get; set; }
        /// <summary>
        /// 业务Id
        /// </summary>
        [Required]
        public virtual Guid BusinessId { get; set; }
        /// <summary>
        /// 人、机、料、法、环
        /// </summary>
        [Required]
        public virtual string CourseType { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        [StringLength(1000)]
        public virtual string Reason { get; set; }
        /// <summary>
        /// 措施
        /// </summary>
        [StringLength(1000)]
        public virtual string Solution { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 创建快照
        /// </summary>
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }
    }
}
