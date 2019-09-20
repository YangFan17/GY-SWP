using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_MildewSummers
{
    /// <summary>
    /// 防霉度夏
    /// </summary>
    [Table("LC_MildewSummers")]
    public class LC_MildewSummer : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 流程外键
        /// </summary>
        public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 上午开机时间
        /// </summary>
        public virtual DateTime? AMBootTime { get; set; }

        /// <summary>
        /// 上午开机前温度
        /// </summary>
        public virtual decimal? AMBootBeforeTmp { get; set; }

        /// <summary>
        /// 上午开机前湿度
        /// </summary>
        public virtual decimal? AMBootBeforeHum { get; set; }

        /// <summary>
        /// 上午观察时间
        /// </summary>
        public virtual DateTime? AMObservedTime { get; set; }

        /// <summary>
        /// 上午开机中温度
        /// </summary>
        public virtual decimal? AMBootingTmp { get; set; }

        /// <summary>
        /// 上午开机中湿度
        /// </summary>
        public virtual decimal? AMBootingHum { get; set; }

        /// <summary>
        /// 上午停机时间
        /// </summary>
        public virtual DateTime? AMBootAfterTime { get; set; }

        /// <summary>
        /// 上午停机后温度
        /// </summary>
        public virtual decimal? AMBootAfterTmp { get; set; }

        /// <summary>
        /// 上午停机后湿度
        /// </summary>
        public virtual decimal? AMBootAfterHum { get; set; }

        /// <summary>
        /// 下午开机时间
        /// </summary>
        public virtual DateTime? PMBootingTime { get; set; }

        /// <summary>
        /// 下午开机前温度
        /// </summary>
        public virtual decimal? PMBootBeforeTmp { get; set; }

        /// <summary>
        /// 下午开机前湿度
        /// </summary>
        public virtual decimal? PMBootBeforeHum { get; set; }

        /// <summary>
        /// 下午观察时间
        /// </summary>
        public virtual DateTime? PMObservedTime { get; set; }

        /// <summary>
        /// 下午开机中温度
        /// </summary>
        public virtual decimal? PMBootingTmp { get; set; }

        /// <summary>
        /// 下午开机中湿度
        /// </summary>
        public virtual decimal? PMBootingHum { get; set; }

        /// <summary>
        /// 下午停机时间
        /// </summary>
        public virtual DateTime? PMBootAfterTime { get; set; }

        /// <summary>
        /// 下午停机后温度
        /// </summary>
        public virtual decimal? PMBootAfterTmp { get; set; }

        /// <summary>
        /// 下午停机后湿度
        /// </summary>
        public virtual decimal? PMBootAfterHum { get; set; }

        /// <summary>
        /// 员工id
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 员工快照
        /// </summary>
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }
}
