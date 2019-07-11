using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GYSWP.SCInventoryRecords
{
    [Table("LC_SCInventoryRecords")]
    public class SCInventoryRecord : Entity<long>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(200)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 现库存量
        /// </summary>
        public virtual int? CurrentStock { get; set; }
        /// <summary>
        /// 库存合计
        /// </summary>
        public virtual int? TotalInventory { get; set; }
        /// <summary>
        /// 库存实数
        /// </summary>
        public virtual int? InventoryRealNumber { get; set; }
        /// <summary>
        /// 原件短少
        /// </summary>
        public virtual int? ShortOriginal { get; set; }
        /// <summary>
        /// 残损
        /// </summary>
        public virtual int? Damaged { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [StringLength(50)]
        [Required]
        public virtual string EmployeeName { get; set; }
        /// <summary>
        /// 员工id
        /// </summary>
        [StringLength(400)]
        [Required]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 在库保管Id
        /// </summary>
        [Required]
        public virtual Guid TimeLogId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }

}
