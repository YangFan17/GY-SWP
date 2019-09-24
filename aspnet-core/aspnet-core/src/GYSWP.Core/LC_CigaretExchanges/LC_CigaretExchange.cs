using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_CigaretExchanges
{
    /// <summary>
    /// 残损卷烟调换表
    /// </summary>
    [Table("LC_CigaretExchanges")]
    public class LC_CigaretExchange : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 分拣外键
        /// </summary>
             public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 产地
        /// </summary>
        [StringLength(100)]
        public virtual string OriginPlace { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(100)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int? Num { get; set; }

        /// <summary>
        /// 机损原因
        /// </summary>
        [StringLength(2000)]
        public virtual string Reason { get; set; }

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
