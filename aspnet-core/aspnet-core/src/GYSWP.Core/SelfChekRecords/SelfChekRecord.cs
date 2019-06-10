using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.SelfChekRecords
{
    /// <summary>
    /// 自查记录
    /// </summary>
    [Table("SelfChekRecords")]
    public class SelfChekRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 条款Id
        /// </summary>
        [Required]
        public virtual Guid ClauseId { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [Required]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 当天首次点击
        /// </summary>
        public virtual bool? IsTodayFirst { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 姓名快照
        /// </summary>
        public virtual string EmployeeName { get; set; }
    }
}
