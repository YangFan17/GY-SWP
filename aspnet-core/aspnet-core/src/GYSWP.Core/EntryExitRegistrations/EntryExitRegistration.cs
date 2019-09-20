using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GYSWP.EntryExitRegistrations
{

    [Table("LC_EntryExitRegistrations")]
    public class EntryExitRegistration : Entity<long>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 员工姓名
        /// </summary>
        [StringLength(50)]
        [Required]
        public virtual string EmployeeName { get; set; }
        /// <summary>
        /// 员工id             public virtual Guid? TimeLogId { get; set; }
        /// </summary>
        [StringLength(400)]
        [Required]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public virtual DateTime? EntryTime { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public virtual DateTime? ExitTime { get; set; }
        /// <summary>
        /// 入库事由
        /// </summary>
        [StringLength(500)]
        public virtual string ReasonsForWarehousing { get; set; }
        /// <summary>
        /// 库内有无异常
        /// </summary>
        public virtual bool? IsAbnormal { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 在库保管Id
        /// </summary>
        public virtual Guid? TimeLogId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }
}
