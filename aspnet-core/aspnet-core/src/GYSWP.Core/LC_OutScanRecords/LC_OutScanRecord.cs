using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_OutScanRecords
{
    /// <summary>
    /// 出库扫码记录表
    /// </summary>
    [Table("LC_OutScanRecords")]
    public class LC_OutScanRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 出库外键
        /// </summary>
        [Required]
        public virtual Guid TimeLogId { get; set; }

        /// <summary>
        /// 出库订单数
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 应扫码数
        /// </summary>
        public virtual int? ExpectedScanNum { get; set; }

        /// <summary>
        /// 实际扫码数
        /// </summary>
        public virtual int? AcutalScanNum { get; set; }

        /// <summary>
        /// 零条未扫码数
        /// </summary>
        public virtual int? AloneNotScanNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        public virtual string Remark { get; set; }

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
