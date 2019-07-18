using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GYSWP.Categorys
{
    /// <summary>
    /// 资料类别
    /// </summary>
    [Table("Categorys")]

    public class Category : FullAuditedEntity
    {

        /// <summary>
        /// 类别名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 父Id（root 为 0）
        /// </summary>
        public virtual int? ParentId { get; set; }

        /// <summary>
        /// 类别描述
        /// </summary>
        [StringLength(500)]
        public virtual string Desc { get; set; }

        /// <summary>
        /// 维护部门Id
        /// </summary>
        [Required]
        public virtual long DeptId { get; set; }
    }
}
