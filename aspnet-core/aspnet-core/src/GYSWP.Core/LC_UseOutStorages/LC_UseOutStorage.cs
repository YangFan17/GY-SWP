using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GYSWP.LC_UseOutStorages
{
    /// <summary>
    /// 分拣领用出库单
    /// </summary>
    [Table("LC_UseOutStorages")]
    public class LC_UseOutStorage : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 分拣外键
        /// </summary>
             public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 分拣线名称
        /// </summary>
        [StringLength(200)]
        public virtual string SortLineName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(100)]
        public virtual string ProductName { get; set; }

        /// <summary>
        /// 差异(预出库数量)
        /// </summary>
        public virtual int? PreDiffNum { get; set; }

        /// <summary>
        /// 零条(预出库数量)
        /// </summary>
        public virtual int? PreAloneNum { get; set; }

        /// <summary>
        /// 整盘(补库量)
        /// </summary>
        public virtual int? SupWholeNum { get; set; }

        /// <summary>
        /// 总件(补库量)
        /// </summary>
        public virtual int? SupAllPieceNum { get; set; }

        /// <summary>
        /// 总条(补库量)
        /// </summary>
        public virtual int? SupAllItemNum { get; set; }

        /// <summary>
        /// 实际订单量
        /// </summary>
        public virtual int? AcutalOrderNum { get; set; }

        /// <summary>
        /// 盘点(库存盘点)
        /// </summary>
        public virtual int? CheckNum { get; set; }

        /// <summary>
        /// 零条(库存盘点)
        /// </summary>
        public virtual int? CheckAloneNum { get; set; }

        /// <summary>
        /// 保管员
        /// </summary>
        [StringLength(50)]
        public virtual string ClerkName { get; set; }

        /// <summary>
        /// 分拣员
        /// </summary>
        [StringLength(50)]
        public virtual string SortorName { get; set; }

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
