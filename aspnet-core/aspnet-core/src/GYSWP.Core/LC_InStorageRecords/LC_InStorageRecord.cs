using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_InStorageRecords
{
    /// <summary>
    /// 卷烟入库记录表
    /// </summary>
    [Table("LC_InStorageRecords")]
    public class LC_InStorageRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 到货外键
        /// </summary>
        public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 品名规格
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string CarNo { get; set; }

        /// <summary>
        /// 发货单位
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string DeliveryUnit { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string BillNo { get; set; }

        /// <summary>
        /// 应收数量
        /// </summary>
        [Required]
        public virtual int ReceivableAmount { get; set; }

        /// <summary>
        /// 实收数量
        /// </summary>
        [Required]
        public virtual int ActualAmount { get; set; }

        /// <summary>
        /// 差损情况
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string DiffContent { get; set; }

        /// <summary>
        /// 质量
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string Quality { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string ReceiverName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
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
