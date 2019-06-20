
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.ApplyInfos;
using GYSWP.GYEnums;

namespace  GYSWP.ApplyInfos.Dtos
{
    public class ApplyInfoEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// DocumentId
		/// </summary>
		public Guid? DocumentId { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "Type不能为空")]
        public ApplyType Type { get; set; }


        /// <summary>
        /// 操作类型制修订、合理化建议）
        /// </summary>
        [Required(ErrorMessage = "OperateType")]
        public OperateType OperateType { get; set; }



        /// <summary>
        /// EmployeeId
        /// </summary>
        public string EmployeeId { get; set; }



		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// Status
		/// </summary>
		public ApplyStatus Status { get; set; }



		/// <summary>
		/// HandleTime
		/// </summary>
		public DateTime? HandleTime { get; set; }



		/// <summary>
		/// Reason
		/// </summary>
		public string Reason { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }



		/// <summary>
		/// ProcessInstanceId
		/// </summary>
		public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 审批发起时间
        /// </summary>
        public DateTime? ProcessingCreationTime { get; set; }
        /// <summary>
        /// 审批发起时间
        /// </summary>
        public DateTime? ProcessingHandleTime { get; set; }
        /// <summary>
        /// 审批Id
        /// </summary>
        [StringLength(50)]
        public string RevisionPId { get; set; }
        /// <summary>
        /// 审批状态（已同意/已拒绝）
        /// </summary>
        public RevisionStatus ProcessingStatus { get; set; }
    }
}