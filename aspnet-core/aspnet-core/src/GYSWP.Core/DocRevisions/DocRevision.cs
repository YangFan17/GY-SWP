using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.DocRevisions
{
    [Table("DocRevisions")]
    public class DocRevision : FullAuditedEntity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 标准名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 标准分类Id
        /// </summary>
        [Required]
        public virtual int CategoryId { get; set; }
        /// <summary>
        /// 申请Id
        /// </summary>
        [Required]
        public virtual Guid ApplyInfoId { get; set; }
        /// <summary>
        /// 员工Id
        /// </summary>
        [StringLength(100)]
        [Required]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }
        /// <summary>
        /// 审批结果
        /// </summary>
        [Required]
        public virtual RevisionType RevisionType { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public virtual RevisionStatus Status { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public virtual string DeptId { get; set; }
    }
}
