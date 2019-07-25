using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.CriterionExamines
{
    [Table("CriterionExamines")]
    public class CriterionExamine : Entity<Guid>,IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 考核名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Title { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        [Required]
        public virtual bool IsPublish { get; set; }
        /// <summary>
        /// 考核类型(内部考核，外部考核)
        /// </summary>
        [Required]
        public virtual CriterionExamineType Type { get; set; }
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
        /// <summary>
        /// 考核部门
        /// </summary>
        [Required]
        public virtual long CreatorDeptId { get; set; }
        /// <summary>
        /// 被考核部门
        /// </summary>
        [Required]
        public virtual long DeptId { get; set; }
        /// <summary>
        /// 考核部门快照
        /// </summary>
        [StringLength(100)]
        public virtual string CreatorDeptName { get; set; }
        /// <summary>
        /// 被考核部门快照
        /// </summary>
        [StringLength(100)]
        public virtual string DeptName { get; set; }
    }
}
