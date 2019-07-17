using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.MainPointsRecords
{
    /// <summary>
    /// 要点记录表
    /// </summary>
    [Table("MainPointsRecords")]
    public class MainPointsRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 职责表外键
        /// </summary>
        [Required]
        public virtual Guid PositionInfoId { get; set; }

        /// <summary>
        /// 标准Id
        /// </summary>
        [Required]
        public virtual Guid DocumentId { get; set; }

        /// <summary>
        /// 实施要点及记录
        /// </summary>
        [StringLength(2000)]
        public virtual string MainPoint { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }
}
