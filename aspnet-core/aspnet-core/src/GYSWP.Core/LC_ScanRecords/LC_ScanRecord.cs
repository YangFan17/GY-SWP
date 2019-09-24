using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_ScanRecords
{
    /// <summary>
    /// 扫码起止记录表
    /// </summary>
    [Table("LC_ScanRecords")]
    public class LC_ScanRecord : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 对应业务外键
        /// </summary>
        public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 类型（入库扫码、出库扫码）
        /// </summary>
        [Required]
        public virtual LC_ScanRecordType Type { get; set; }

        /// <summary>
        /// 状态（开始、结束）
        /// </summary>
        [Required]
        public virtual LC_TimeStatus Status { get; set; }

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
