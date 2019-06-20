using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.ApplyInfos
{
    [Table("ApplyInfos")]
    public class ApplyInfo : Entity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 所属标准
        /// </summary>
        public virtual Guid? DocumentId { get; set; }
        /// <summary>
        /// 类型（制修订、合理化建议）
        /// </summary>
        [Required]
        public virtual OperateType OperateType { get; set; }
        /// <summary>
        /// 操作类型（标准制修订）
        /// </summary>
        [Required]
        public virtual ApplyType Type { get; set; }
        /// <summary>
        /// 员工Id
        /// </summary>
        [StringLength(100)]
        [Required]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [StringLength(100)]
        public virtual string EmployeeName { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 审批状态（已同意/已拒绝）
        /// </summary>
        [Required]
        public virtual ApplyStatus Status { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime? HandleTime { get; set; }
        /// <summary>
        /// 申请原因
        /// </summary>
        [StringLength(1000)]
        [Required]
        public virtual string Reason { get; set; }
        /// <summary>
        /// 申请内容
        /// </summary>
        [StringLength(2000)]
        [Required]
        public virtual string Content { get; set; }
        /// <summary>
        /// 审批Id
        /// </summary>
        [StringLength(50)]
        public virtual string ProcessInstanceId { get; set; }
    }

}
