using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_TimeLogs
{
    /// <summary>
    /// 时间日志
    /// </summary>
    [Table("LC_TimeLogs")]
    public class LC_TimeLog : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 登记类型（入库（到货登记）、在库保管、出库分拣、领货出库、入库扫码、出库扫码）
        /// </summary>
        [Required]
        public virtual LC_TimeType Type { get; set; }

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
