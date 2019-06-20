

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.EmployeeClauses;
using GYSWP.GYEnums;

namespace GYSWP.EmployeeClauses.Dtos
{
    public class EmployeeClauseListDto : EntityDto<Guid>, IHasCreationTime
    {
        /// <summary>
        /// ClauseId
        /// </summary>
        [Required(ErrorMessage = "ClauseId不能为空")]
        public Guid ClauseId { get; set; }

        /// <summary>
        /// 标准Id
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// 姓名快照
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage = "EmployeeId不能为空")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    public class DocUserInfo
    {
        /// <summary>
        /// 是否已确认条款
        /// </summary>
        public bool IsConfirm { get; set; }
        /// <summary>
        /// 是否可申请制修订
        /// </summary>
        public bool IsApply { get; set; }
        /// <summary>
        /// 是否可制修订操作
        /// </summary>
        public bool IsRevision { get; set; }

        /// <summary>
        /// 是否为制修订模式
        /// </summary>
        public bool EditModel { get; set; }

        /// <summary>
        /// 是否可提交保存
        /// </summary>
        //public bool IsSave { get; set; }

        /// <summary>
        /// 是否为审批提交后等待阶段
        /// </summary>
        public bool IsRevisionWaitTime { get; set; }
        /// <summary>
        /// 申请Id
        /// </summary>
        public Guid? ApplyId { get; set; }

        /// <summary>
        /// 是否制修订审批提交（流程结束）
        /// </summary>
        public bool IsRevisionOver { get; set; }
    }
}