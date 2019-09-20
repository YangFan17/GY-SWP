using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_WarningReports
{
    [Table("LC_WarningReports")]
    public class LC_WarningReport : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 对应业务外键
        /// </summary>
        public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 厂家
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string Factory { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public virtual int Quantity { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        [Required]
        public virtual DateTime InStorageTime { get; set; }

        /// <summary>
        /// 在库时间
        /// </summary>
        [Required]
        public virtual DateTime OnStorageTime { get; set; }

        /// <summary>
        /// 员工id
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 员工快照
        /// </summary>
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }
}
