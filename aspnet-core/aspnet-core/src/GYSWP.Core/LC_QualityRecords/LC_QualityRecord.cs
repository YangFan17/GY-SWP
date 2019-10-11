using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_QualityRecords
{
    /// <summary>
    /// 入库验收质量登记表
    /// </summary>
    [Table("LC_QualityRecords")]
    public class LC_QualityRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 到货外键
        /// </summary>
        public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 卷烟名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 批发价格
        /// </summary>
        public virtual decimal? WholesaleAmount { get; set; }

        /// <summary>
        /// 出售数量
        /// </summary>
        public virtual int? SaleQuantity { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
        [StringLength(100)]
        public virtual string CarNo { get; set; }

        /// <summary>
        /// 赔偿金额
        /// </summary>
        public virtual decimal? CompensationAmount { get; set; }

        /// <summary>
        /// 承运人
        /// </summary>
        [StringLength(50)]
        public virtual string CarrierName { get; set; }

        /// <summary>
        /// 保管员
        /// </summary>
        [StringLength(50)]
        public virtual string ClerkName { get; set; }

        /// <summary>
        /// 交接日期
        /// </summary>
        public virtual DateTime? HandoverTime { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal? Amount { get; set; }

        /// <summary>
        /// 经手人
        /// </summary>
        [StringLength(50)]
        public virtual string HandManName { get; set; }

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
