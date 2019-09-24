using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.Documents
{
    [Table("Documents")]
    public class Document : FullAuditedEntity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 标准名称
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 标准编号
        /// </summary>
        [StringLength(100)]
        //[Required]
        public virtual string DocNo { get; set; }
        /// <summary>
        /// 标准分类Id
        /// </summary>
        [Required]
        public virtual int CategoryId { get; set; }
        /// <summary>
        /// 分类Id层级 用逗号分隔
        /// </summary>
        [StringLength(200)]
        public virtual string CategoryCode { get; set; }
        /// <summary>
        /// DeptIds
        /// </summary>
        public virtual string DeptIds { get; set; }
        /// <summary>
        /// DeptDesc
        /// </summary>
        public virtual string DeptDesc { get; set; }
        /// <summary>
        /// 分类名描述（分类名层级以逗号分隔）
        /// </summary>
        [StringLength(500)]
        public virtual string CategoryDesc { get; set; }
        /// <summary>
        /// 摘要说明
        /// </summary>
        [StringLength(2000)]
        public virtual string Summary { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public virtual DateTime? PublishTime { get; set; }

        /// <summary>
        /// 授权员工名称（以逗号分隔）
        /// </summary>
        public virtual string EmployeeDes { get; set; }

        /// <summary>
        /// 是否是全部用户
        /// </summary>
        public virtual bool IsAllUser { get; set; }
        /// <summary>
        /// 员工授权Ids（以逗号分隔）
        /// </summary>
        public virtual string EmployeeIds { get; set; }

        /// <summary>
        /// 是否启用（作废）
        /// </summary>
        [Required]
        public virtual bool IsAction { get; set; }

        /// <summary>
        /// 作废时间
        /// </summary>
        public virtual DateTime? InvalidTime { get; set; }

        /// <summary>
        /// 电子章数组 （以逗号分隔）
        /// </summary>
        [StringLength(50)]
        public virtual string Stamps { get; set; }

        /// <summary>
        /// 适用于（QMS,EMS,OHS）
        /// </summary>
        public virtual string SuitableCode { get; set; }
        /// <summary>
        /// 修订记录表Id
        /// </summary>
        public virtual Guid BLLId { get; set; }
    }
}
