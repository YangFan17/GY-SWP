

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.ApplyInfos;
using GYSWP.GYEnums;

namespace GYSWP.ApplyInfos.Dtos
{
    public class ApplyInfoListDto : EntityDto<Guid> 
    {
		/// <summary>
		/// DocumentId
		/// </summary>
		public Guid? DocumentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage="Type不能为空")]
		public ApplyType Type { get; set; }

        /// <summary>
        /// 操作类型制修订、合理化建议）
        /// </summary>
        [Required(ErrorMessage = "OperateType")]
        public OperateType OperateType { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }

		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }

		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime? CreationTime { get; set; }

		/// <summary>
		/// Status
		/// </summary>
		[Required(ErrorMessage="Status不能为空")]
		public int Status { get; set; }

		/// <summary>
		/// HandleTime
		/// </summary>
		public DateTime? HandleTime { get; set; }

		/// <summary>
		/// Reason
		/// </summary>
		[Required(ErrorMessage="Reason不能为空")]
		public string Reason { get; set; }

		/// <summary>
		/// Content
		/// </summary>
		[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }

		/// <summary>
		/// ProcessInstanceId
		/// </summary>
		public string ProcessInstanceId { get; set; }
    }
}