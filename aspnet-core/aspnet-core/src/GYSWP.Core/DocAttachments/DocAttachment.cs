using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.DocAttachments
{
    [Table("DocAttachments")]
    public class DocAttachment : FullAuditedEntity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 类型（标准附件、条款附件、考核附件）
        /// </summary>
        [Required]
        public virtual int Type { get; set; }
        /// <summary>
        /// 附件名
        /// </summary>
        [StringLength(200)]
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 枚举（PDF、Word、Excel）
        /// </summary>
        public virtual int? FileType { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public virtual decimal? FileSize { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [StringLength(500)]
        [Required]
        public virtual string Path { get; set; }
        /// <summary>
        /// 外键 资料表Id
        /// </summary>
        [Required]
        public virtual Guid BLL { get; set; }
        /// <summary>
        /// 备份Id
        /// </summary>
        public virtual Guid? BackUpId { get; set; }
    }
}
