using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.IndicatorLibrarys
{
    [Table("IndicatorLibrary")]
    public class IndicatorLibrary : FullAuditedEntity<Guid> //注意修改主键Id数据类型
    {
        /// <summary>
        /// 检查指标
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 指标释义
        /// </summary>
        [Required]
        [StringLength(2000)]
        public virtual string Paraphrase { get; set; }

        /// <summary>
        /// 测量方式
        /// </summary>
        [Required]
        [StringLength(2000)]
        public virtual string MeasuringWay { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        [Required]
        public virtual CycleTime CycleTime { get; set; }

        /// <summary>
        /// 来源标准Id
        /// </summary>
        [Required]
        public virtual Guid SourceDocId { get; set; }

        /// <summary>
        /// 被考核部门
        /// </summary>
        [Required]
        public virtual string DeptIds { get; set; }

        /// <summary>
        /// 被考核部门快照
        /// </summary>
        public virtual string DeptNames { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string Department { get; set; }
    }
}
