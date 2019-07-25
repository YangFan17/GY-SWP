using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.ClauseRevisions
{
    [Table("ClauseRevisions")]
    public class ClauseRevision : FullAuditedEntity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 条款Id
        /// </summary>
        public virtual Guid? ClauseId { get; set; }

        /// <summary>
        /// 修订人
        /// </summary>
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 申请记录Id
        /// </summary>
        [Required]
        public virtual Guid ApplyInfoId { get; set; }

        /// <summary>
        /// 通过状态
        /// </summary>
        [Required]
        public virtual RevisionStatus Status { get; set; }

        /// <summary>
        /// 父Id（root 为 空）
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(500)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [StringLength(100)]
        [Required]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(2000)]
        public virtual string Content { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public virtual string ClauseNo { get; set; }

        /// <summary>
        /// 所属标准
        /// </summary>
        public virtual Guid? DocumentId { get; set; }


        /// <summary>
        /// 类型（新增、修改、删除、标准制定）
        /// </summary>
        [Required]
        public virtual RevisionType RevisionType { get; set; }
    }
}
