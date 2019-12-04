using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.ExamineResults
{
    [Table("ExamineResults")]
    public class ExamineResult : Entity<Guid>, IHasCreationTime  //注意修改主键Id数据类型
    {
        /// <summary>
        /// 检查表外键
        /// </summary>
        [Required]
        public virtual Guid ExamineDetailId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(500)]
        [Required]
        public virtual string Content { get; set; }
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
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 判定依据
        /// </summary>
        [StringLength(200)]
        public virtual string FailReason { get; set; }
    }

}
