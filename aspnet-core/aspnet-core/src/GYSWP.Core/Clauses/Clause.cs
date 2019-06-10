using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.Clauses
{
    [Table("Clauses")]
    public class Clause : FullAuditedEntity<Guid> //注意修改主键Id数据类型
    {
        public virtual Guid? ParentId { get; set; }
        [StringLength(500)]
        public virtual string Title { get; set; }
        [StringLength(2000)]
        public virtual string Content { get; set; }
        /// <summary>
        /// 父Id（root 为 空）
        /// </summary>
        [Required]
        public virtual string ClauseNo { get; set; }
        /// <summary>
        /// 类型（文本、表格）是否含有附件
        /// </summary>
        [Required]
        public virtual bool HasAttchment { get; set; }
        /// <summary>
        /// 所属标准
        /// </summary>
        public virtual Guid? DocumentId { get; set; }
    }
}
