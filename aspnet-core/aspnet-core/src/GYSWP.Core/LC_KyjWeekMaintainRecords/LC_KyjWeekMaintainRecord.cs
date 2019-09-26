using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_KyjWeekMaintainRecords
{
    /// <summary>
    /// 空压机周维护保养记录
    /// </summary>
    [Table("LC_KyjWeekMaintainRecords")]
    public class LC_KyjWeekMaintainRecord : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 设备编号
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string DeviceID { get; set; }

        /// <summary>
        /// 维保人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string ResponsibleName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 监督人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string SupervisorName { get; set; }

        /// <summary>
        /// 各部位螺丝紧固
        /// </summary>
        public virtual bool? IsScrewFastening { get; set; }

        /// <summary>
        /// 机油压力是否在正常标准范围内
        /// </summary>
        public virtual bool? IsOilPressureOk { get; set; }

        /// <summary>
        /// 油、气是否存在跑、冒、滴、漏
        /// </summary>
        public virtual bool? IsOilAndGasBad { get; set; }

        /// <summary>
        /// 储气罐压力表指针是否摆动灵活
        /// </summary>
        public virtual bool? IsPressureGaugePointerOk { get; set; }

        /// <summary>
        /// 润滑油位是否低于最下端红线
        /// </summary>
        public virtual bool? IsTheOilLevelOk { get; set; }

        /// <summary>
        /// 空气滤清器是否有灰尘、油污
        /// </summary>
        public virtual bool? IsAirFilterOk { get; set; }

        /// <summary>
        /// 设备各部位清洁、润滑
        /// </summary>
        public virtual bool? IsDeviceClean { get; set; }

        /// <summary>
        /// 储气罐压力表是否定期检验，铅封完好
        /// </summary>
        public virtual bool? IsPressureGaugeOk { get; set; }

        /// <summary>
        /// 储气罐安全阀是否定期检验，铅封完好
        /// </summary>
        public virtual bool? IsSafetyValveOk { get; set; }

        /// <summary>
        /// 清洗压缩机散热板翘片
        /// </summary>
        public virtual bool? IsCoolingPlateClean { get; set; }

        /// <summary>
        /// 储气罐排污阀是否畅通
        /// </summary>
        public virtual bool? IsDrainValveOpen { get; set; }

        /// <summary>
        /// 发现问题
        /// </summary>
        [StringLength(500)]
        public virtual string DiscoverProblems { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [StringLength(500)]
        public virtual string ProcessingResult { get; set; }

        /// <summary>
        /// 员工id
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        ///  员工快照
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string EmployeeName { get; set; }
    }
}
