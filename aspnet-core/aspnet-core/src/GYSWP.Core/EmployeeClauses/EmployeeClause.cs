using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.EmployeeClauses
{
    /// <summary>
    /// 确认条款表
    /// </summary>
    [Table("EmployeeClauses")]
    public class EmployeeClause : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// DetpClause外键
        /// </summary>
        [Required]
        public virtual Guid ClauseId { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [Required]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 标准Id
        /// </summary>
        [Required]
        public virtual Guid DocumentId { get; set; }

        /// <summary>
        /// 姓名快照
        /// </summary>
        public virtual string EmployeeName { get; set; }

        public virtual DateTime CreationTime { get; set; }
    }
}
