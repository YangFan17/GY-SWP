using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GYSWP.InspectionRecords
{
    [Table("LC_InspectionRecords")]
    public class InspectionRecord : Entity<long>, IHasCreationTime //注意修改主键Id数据类型
    {
        /// <summary>
        /// 门窗、锁有无异常
        /// </summary>
        public virtual bool? IsDWLAbnormal { get; set; }
        /// <summary>
        /// 墙体有无破坏
        /// </summary>
        public virtual bool? IsWallDestruction { get; set; }
        /// <summary>
        /// 屋顶、墙面是否渗水
        /// </summary>
        public virtual bool? IsRoofWallSeepage { get; set; }
        /// <summary>
        /// 温湿度是否超标
        /// </summary>
        public virtual bool? IsHumitureExceeding { get; set; }
        /// <summary>
        /// 消防控制系统工作是否正常
        /// </summary>
        public virtual bool? IsFASNormal { get; set; }
        /// <summary>
        /// 防盗报警器工作是否正常
        /// </summary>
        public virtual bool? IsBurglarAlarmNormal { get; set; }
        /// <summary>
        /// 防盗报警设密是否灵敏有效
        /// </summary>
        public virtual bool? IsSASSValid { get; set; }
        /// <summary>
        /// 监控摄像头有无遮挡
        /// </summary>
        public virtual bool? IsCameraShelter { get; set; }
        /// <summary>
        /// 消防设施有无阻拦
        /// </summary>
        public virtual bool? IsFPDStop { get; set; }
        /// <summary>
        /// 安全出口有无阻拦
        /// </summary>
        public virtual bool? IsEXITStop { get; set; }
        /// <summary>
        /// 负责人签字
        /// </summary>
        [StringLength(50)]
        public virtual string SignatureOfPrincipal { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [StringLength(50)]
        [Required]
        public virtual string EmployeeName { get; set; }
        /// <summary>
        /// 员工id
        /// </summary>
        [StringLength(400)]
        [Required]
        public virtual string EmployeeId { get; set; }
        /// <summary>
        /// 在库保管Id
        /// </summary>
        [Required]
        public virtual Guid TimeLogId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreationTime { get; set; }
    }

}
