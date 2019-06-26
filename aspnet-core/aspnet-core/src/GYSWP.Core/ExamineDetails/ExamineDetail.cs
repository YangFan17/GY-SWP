using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.ExamineDetails
{
    [Table("ExamineDetails")]
    public class ExamineDetail : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 考核表外键
        /// </summary>
        [Required]
        public virtual Guid CriterionExamineId { get; set; }
        /// <summary>
        /// 条款Id
        /// </summary>
        [Required]
        public virtual Guid ClauseId { get; set; }
        /// <summary>
        /// 考核状态（未开始/已完成）
        /// </summary>
        [Required]
        public virtual ResultStatus Status { get; set; }
        /// <summary>
        /// 考核结果（未检查/合格/不合格）
        /// </summary>
        [Required]
        public virtual ExamineStatus Result { get; set; }
        /// <summary>
        /// 被考核者id
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 被考核者快照
        /// </summary>
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 考核者人
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string CreatorEmpeeId { get; set; }
        /// <summary>
        /// 考核者快照
        /// </summary>
        [StringLength(50)]
        public virtual string CreatorEmpName { get; set; }
    }

}
