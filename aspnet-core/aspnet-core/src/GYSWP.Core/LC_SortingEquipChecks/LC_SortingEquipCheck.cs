using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_SortingEquipChecks
{
    /// <summary>
    /// 分拣设备点检
    /// </summary>
    [Table("LC_SortingEquipChecks")]
    public class LC_SortingEquipCheck : Entity<Guid>, IHasCreationTime
    {

        /// <summary>
        /// 分拣外键
        /// </summary>
             public virtual Guid? TimeLogId { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        [StringLength(100)]
        public virtual string ResponsibleName { get; set; }

        /// <summary>
        /// 监管人
        /// </summary>
        [StringLength(50)]
        public virtual string SupervisorName { get; set; }

        /// <summary>
        /// 链板、推头是否正常
        /// </summary>
        [Required]
        public virtual bool IsChainPlateOk { get; set; }

        /// <summary>
        /// 控制开关是否灵敏有效
        /// </summary>
        [Required]
        public virtual bool IsControlSwitchOk { get; set; }

        /// <summary>
        /// 电、气线路有无裸露、脱落
        /// </summary>
        [Required]
        public virtual bool IsElcOrGasBad { get; set; }

        /// <summary>
        /// 检查所有的升降舌头处于上升状态
        /// </summary>
        [Required]
        public virtual bool IsLiftUp { get; set; }

        /// <summary>
        /// 分拣系统是否正常启动
        /// </summary>
        [Required]
        public virtual bool IsSortSysOk { get; set; }

        /// <summary>
        /// 传动皮带、链板上有无异物
        /// </summary>
        [Required]
        public virtual bool IsChanDirty { get; set; }

        /// <summary>
        /// 进料口、封切处有无异物
        /// </summary>
        [Required]
        public virtual bool IsCutSealDirty { get; set; }

        /// <summary>
        /// 控制开关是否灵敏有效
        /// </summary>
        [Required]
        public virtual bool IsBZJControlSwitchOk { get; set; }

        /// <summary>
        /// 电、气线路有无裸露、脱落
        /// </summary>
        [Required]
        public virtual bool IsBZJElcOrGasBad { get; set; }

        /// <summary>
        /// 温控仪工作是否正常
        /// </summary>
        [Required]
        public virtual bool IsTempOk { get; set; }

        /// <summary>
        /// 系统是否正常启动
        /// </summary>
        [Required]
        public virtual bool IsBZJSysOk { get; set; }

        /// <summary>
        /// 收缩炉、封切刀工作是否正常
        /// </summary>
        [Required]
        public virtual bool IsStoveOk { get; set; }

        /// <summary>
        /// 贴标系统能否正常工作
        /// </summary>
        [Required]
        public virtual bool IsLabelingOk { get; set; }

        /// <summary>
        /// 电、气线路有无裸露、脱落
        /// </summary>
        [Required]
        public virtual bool IsTBJElcOrGasBad { get; set; }

        /// <summary>
        /// 激光防护罩是否完好
        /// </summary>
        [Required]
        public virtual bool IsLaserShieldOk { get; set; }

        /// <summary>
        /// 检查电源线、激光机是否正常
        /// </summary>
        [Required]
        public virtual bool IsLineOrMachineOk { get; set; }

        /// <summary>
        /// 分拣设备烟仓下烟是否正常
        /// </summary>
        [Required]
        public virtual bool IsCigaretteHouseOk { get; set; }

        /// <summary>
        /// 分拣设备合单机构工作是否正常
        /// </summary>
        [Required]
        public virtual bool IsSingleOk { get; set; }

        /// <summary>
        /// 主电源线是否发热
        /// </summary>
        [Required]
        public virtual bool IsMainLineOk { get; set; }

        /// <summary>
        /// 打码机光学感应器有无偏移、有无残码现象
        /// </summary>
        [Required]
        public virtual bool IsCoderOk { get; set; }

        /// <summary>
        /// 包装机叠烟翻板工作是否正常
        /// </summary>
        public virtual bool? IsBZJWorkOk { get; set; }

        /// <summary>
        /// 分拣设备皮带是否跑偏，机械运转有无异响
        /// </summary>
        [Required]
        public virtual bool IsBeltDeviation { get; set; }

        /// <summary>
        /// 塑封包装是否完好
        /// </summary>
        [Required]
        public virtual bool IsFBJOk { get; set; }

        /// <summary>
        /// 贴标机工作是否正常
        /// </summary>
        [Required]
        public virtual bool IsTBJOk { get; set; }

        /// <summary>
        /// 软件系统是否正常退出
        /// </summary>
        [Required]
        public virtual bool IsSysOutOk { get; set; }

        /// <summary>
        /// 电、气是否关闭
        /// </summary>
        [Required]
        public virtual bool IsShutElcOrGas { get; set; }

        /// <summary>
        /// 打码数据是否回传
        /// </summary>
        [Required]
        public virtual bool IsDataCallback { get; set; }

        /// <summary>
        /// 设备是否进行清洁保养
        /// </summary>
        [Required]
        public virtual bool IsMachineClean { get; set; }

        /// <summary>
        /// 故障描述和处理
        /// </summary>
        [StringLength(2000)]
        public virtual string Troubleshooting { get; set; }

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
