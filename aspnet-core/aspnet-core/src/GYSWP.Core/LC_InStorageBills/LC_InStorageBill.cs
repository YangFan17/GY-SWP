using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_InStorageBills
{
    /// <summary>
    /// 到货物流单证表
    /// </summary>
    [Table("LC_InStorageBills")]
    public class LC_InStorageBill : Entity<Guid>, IHasCreationTime
    {
        public virtual string BillNo { get; set; }

        /// <summary>
        /// 到货外键
        /// </summary>
        public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 《烟草专卖品准运证》
        /// </summary>
        [Required]
        public virtual bool IsYczmBill { get; set; }

        /// <summary>
        /// 《电子交易专用合同（卷烟）》
        /// </summary>
        [Required]
        public virtual bool IsJyhtBill { get; set; }

        /// <summary>
        /// 增值税发票
        /// </summary>
        [Required]
        public virtual bool IsZzsBill { get; set; }

        /// <summary>
        /// 货车辆密封装置
        /// </summary>
        [Required]
        public virtual bool IsCarSeal { get; set; }

        /// <summary>
        /// 车载电子锁
        /// </summary>
        [Required]
        public virtual bool IsCarElcLock { get; set; }

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
