using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_LPFunctionRecords
{
    /// <summary>
    /// 桅柱式升降平台运行记录
    /// </summary>
    [Table("LC_LPFunctionRecords")]
    public class LC_LPFunctionRecord : Entity<Guid>, IHasCreationTime
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
        /// 监管人
        /// </summary>
        [Required]
        [StringLength(50)]
        public virtual string SupervisorName { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime? UseTime { get; set; }

        /// <summary>
        /// 停用时间
        /// </summary>
        public virtual DateTime? DownTime { get; set; }

        /// <summary>
        /// 急停开关是否灵敏有效
        /// </summary>
        public virtual bool? IsSwitchOk { get; set; }

        /// <summary>
        /// 升降是否正常
        /// </summary>
        public virtual bool? IsLiftingOk { get; set; }

        /// <summary>
        /// 平台护栏是否完好
        /// </summary>
        public virtual bool? IsGuardrailOk { get; set; }

        /// <summary>
        /// 外伸支腿是否完好
        /// </summary>
        public virtual bool? IsOutriggerLegOk { get; set; }

        /// <summary>
        /// 地面是否平整
        /// </summary>
        public virtual bool? IsGroundSmooth { get; set; }

        /// <summary>
        /// 外伸支腿是否调整平稳
        /// </summary>
        public virtual bool? IsOutriggerLegStable { get; set; }

        /// <summary>
        /// “O”型水平仪是否调平
        /// </summary>
        public virtual bool? IsLevelLeveling { get; set; }

        /// <summary>
        /// 举高机周围有无阻挡物
        /// </summary>
        public virtual bool? IsLiftingMachineBad { get; set; }

        /// <summary>
        /// 载荷是否超标
        /// </summary>
        public virtual bool? IsLoadExceeding { get; set; }

        /// <summary>
        /// 平台护栏是否降至规定位置
        /// </summary>
        public virtual bool? IsGuardrailPositionOk { get; set; }

        /// <summary>
        /// 外伸支腿是否收拢
        /// </summary>
        public virtual bool? IsOutriggerCloseUp { get; set; }

        /// <summary>
        /// 设备是否进行清洁
        /// </summary>
        public virtual bool? IsDeviceClean { get; set; }

        /// <summary>
        /// 故障描述及处理
        /// </summary>
        public virtual string Desc { get; set; }

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
