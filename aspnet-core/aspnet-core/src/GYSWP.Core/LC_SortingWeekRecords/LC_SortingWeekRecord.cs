using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYSWP.LC_SortingWeekRecords
{
    /// <summary>
    /// 分拣设备周维护记录
    /// </summary>
    [Table("LC_SortingWeekRecords")]
    public class LC_SortingWeekRecord : Entity<Guid>, IHasCreationTime
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
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 检查传动链条、链板、皮带张紧度
        /// </summary>
        public virtual bool? IsInspectZjd { get; set; }

        /// <summary>
        /// 辊筒、轴承等部位是否清洁润滑
        /// </summary>
        public virtual bool? IsBearingEtcBad { get; set; }

        /// <summary>
        /// 线路管道是否漏电、漏水、漏油
        /// </summary>
        public virtual bool? IsLinePipeBad { get; set; }

        /// <summary>
        /// 网络数据线是否松动、脱落
        /// </summary>
        public virtual bool? IsNetworkDataLineBad { get; set; }

        /// <summary>
        /// 舌板、推头位置是否异常
        /// </summary>
        public virtual bool? IsSbTtBad { get; set; }

        /// <summary>
        /// 是否对皮带面进行清洁
        /// </summary>
        public virtual bool? IsBeltSurfaceClean { get; set; }

        /// <summary>
        /// 控制线路、器件是否正常
        /// </summary>
        public virtual bool? IsKzxlQJOk { get; set; }

        /// <summary>
        /// 各控制开关、按钮是否有效
        /// </summary>
        public virtual bool? IsControlBtnOk { get; set; }

        /// <summary>
        /// 辊筒、轴承等部位是否清洁润滑
        /// </summary>
        public virtual bool? IsBzjBearingEtcBad { get; set; }

        /// <summary>
        /// 气管是否完好，有无 “跑、冒、滴、漏”现象
        /// </summary>
        public virtual bool? IsTracheaBad { get; set; }

        /// <summary>
        /// 网络数据线是否松动、脱落
        /// </summary>
        public virtual bool? IsBzjNetworkDataLineBad { get; set; }

        /// <summary>
        /// 封切刀加热丝及高温胶是否完好
        /// </summary>
        public virtual bool? IsFqdJrsGwjBad { get; set; }

        /// <summary>
        /// 气缸升降动作是否顺畅
        /// </summary>
        public virtual bool? IsCylinderOk { get; set; }

        /// <summary>
        /// 举升导杆是否加润滑油
        /// </summary>
        public virtual bool? IsLiftingGuideBarBad { get; set; }

        /// <summary>
        /// 贴标机打印头是否清洗
        /// </summary>
        public virtual bool? IsPrintHeadBad { get; set; }

        /// <summary>
        /// 校正打印机标签、色带安放位置
        /// </summary>
        public virtual bool? IsPlacementPositionOk { get; set; }

        /// <summary>
        /// 检查程序参数设置有无改动
        /// </summary>
        public virtual bool? IsParameterSettingOk { get; set; }

        /// <summary>
        /// 用脱脂棉蘸无水酒精，擦拭镜头
        /// </summary>
        public virtual bool? IsWipeLens { get; set; }

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
