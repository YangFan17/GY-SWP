using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.DocAttachments
{
    [Table("LC_Attachments")]
    public class LC_Attachment : Entity<Guid>,IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public virtual LC_AttachmentType Type { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [StringLength(500)]
        [Required]
        public virtual string Path { get; set; }
        [Required]
        public virtual Guid? BLL { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [Required]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }
}
