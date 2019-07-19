using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.PositionInfos
{
    /// <summary>
    /// 岗位职责表
    /// </summary>
    [Table("PositionInfos")]
    public class PositionInfo : FullAuditedEntity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string Position { get; set; }

        /// <summary>
        /// 工作职责
        /// </summary>
        [StringLength(500)]
        public virtual string Duties { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 姓名快照
        /// </summary>
        [StringLength(20)]
        public virtual string EmployeeName { get; set; }
    }
}
