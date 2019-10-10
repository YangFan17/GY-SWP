using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.Advises
{
    [Table("Advises")]
    public class Advise : Entity<Guid>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 建议名称
        /// </summary>
        [StringLength(100)]
        public virtual string AdviseName { get; set; }
        /// <summary>
        /// 现状描述
        /// </summary>
        [StringLength(5000)]
        public virtual string CurrentSituation { get; set; }
        /// <summary>
        /// 对策建议
        /// </summary>
        [StringLength(5000)]
        public virtual string Solution { get; set; }
        /// <summary>
        /// 是否采纳
        /// </summary>
        public virtual bool? IsAdoption { get; set; }
        /// <summary>
        /// 员工Id
        /// </summary>
        [Required]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 姓名快照
        /// </summary>
        public virtual string EmployeeName { get; set; }
        /// <summary>
        /// 申报日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        [Required]
        public virtual long DeptId { get; set; }
        /// <summary>
        /// 部门快照
        /// </summary>
        public virtual string DeptName { get; set; }

        /// <summary>
        /// 联合建议人快照
        /// </summary>
        public virtual string UnionEmpName { get; set; }

        /// <summary>
        /// 是否公示
        /// </summary>
        public virtual bool IsPublicity { get; set; }

        /// <summary>
        /// 申请Id
        /// </summary>
        [StringLength(50)]
        public virtual string ProcessInstanceId { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public virtual DateTime? ProcessingHandleTime { get; set; }
    }
}
