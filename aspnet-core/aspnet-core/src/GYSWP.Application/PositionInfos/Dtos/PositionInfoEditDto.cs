
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.PositionInfos;

namespace  GYSWP.PositionInfos.Dtos
{
    public class PositionInfoEditDto: FullAuditedEntity<Guid?>
    {


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

    public class PosInfoInput
    {
        /// <summary>
        /// 工作职责
        /// </summary>
        [StringLength(500)]
        public virtual string Duties { get; set; }
    }
}