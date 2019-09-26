using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_SortingMonthRecords
{
    /// <summary>
    /// 分拣设备月维护记录
    /// </summary>
    [Table("LC_SortingMonthRecords")]
    public class LC_SortingMonthRecord : Entity<Guid>, IHasCreationTime
    {


        /// <summary>
        /// ResponsibleName  责任人
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "ResponsibleName不能为空")]
        public string ResponsibleName { get; set; }

        /// <summary>
        /// SupervisorName  监管人
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "SupervisorName不能为空")]
        public virtual string SupervisorName { get; set; }

        /// <summary>
        /// 员工ID
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
        /// 分拣设备的名字
        /// </summary>
        [StringLength(50)]
        public virtual string EquiNo { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 各部件、急停按钮是否有效
        /// </summary>
        public virtual bool? IsFjxButtonOk { get; set; }

        /// <summary>
        /// 合单机多沟带张紧是否适度
        /// </summary>
        public virtual bool? IsTensioningModerate { get; set; }

        /// <summary>
        /// 控制柜内器件是否异常
        /// </summary>
        public virtual bool? IsFjxDeviceBad { get; set; }

        /// <summary>
        /// 各机械部位螺栓是否松动、脱落
        /// </summary>
        public virtual bool? IsFjxBoltBad { get; set; }

        /// <summary>
        /// 安全防护装置是否齐全有效
        /// </summary>
        public virtual bool? IsFjxProtectiveDeviceBad { get; set; }

        /// <summary>
        /// 网络数据线是否松动、脱落
        /// </summary>
        public virtual bool? IsFjxNetworkDataLineBad { get; set; }

        /// <summary>
        /// 电源漏电保护开关、接地线是否完好
        /// </summary>
        public virtual bool? IsFjxDlbhJdxOk { get; set; }

        /// <summary>
        /// 舌板、推头位置是否正常
        /// </summary>
        public virtual bool? IsSbTtBad { get; set; }

        /// <summary>
        /// 传动链条是否添加润滑油
        /// </summary>
        public virtual bool? IsChainLubeOk { get; set; }

        /// <summary>
        /// 各部件、急停按钮是否有效
        /// </summary>
        public virtual bool? IsBzjButtonOk { get; set; }

        /// <summary>
        /// 进料口多沟带张紧是否适度
        /// </summary>
        public virtual bool? IsJlkModerate { get; set; }

        /// <summary>
        /// 控制柜内器件是否异常
        /// </summary>
        public virtual bool? IsBzjDeviceBad { get; set; }

        /// <summary>
        /// 各机械部位螺栓是否松动、脱落
        /// </summary>
        public virtual bool? IsBzjBoltBad { get; set; }

        /// <summary>
        /// 安全防护装置是否齐全有效
        /// </summary>
        public virtual bool? IsBzjProtectiveDeviceBad { get; set; }

        /// <summary>
        /// 网络数据线是否松动、脱落
        /// </summary>
        public virtual bool? IsBzjNetworkDataLineBad { get; set; }

        /// <summary>
        /// 电源漏电保护开关、接地线是否完好
        /// </summary>
        public virtual bool? IsBzjDlbhJdxOk { get; set; }

        /// <summary>
        /// 收缩炉输送链、门帘残渣是否清理
        /// </summary>
        public virtual bool? IsSslClear { get; set; }

        /// <summary>
        /// 气缸接头是否松动、漏气
        /// </summary>
        public virtual bool? IsCylinderJointBad { get; set; }

        /// <summary>
        /// 控制柜内器件是否异常
        /// </summary>
        public virtual bool? IsTbjDeviceBad { get; set; }

        /// <summary>
        /// 各机械部位螺栓是否松动、脱落
        /// </summary>
        public virtual bool? IsTbjBoltBad { get; set; }

        /// <summary>
        /// 光学感应器位置是否正常
        /// </summary>
        public virtual bool? IsOpticalSensorOk { get; set; }

        /// <summary>
        /// 安全防护装置是否齐全有效
        /// </summary>
        public virtual bool? IsDmjProtectiveDeviceBad { get; set; }

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
    }
}
