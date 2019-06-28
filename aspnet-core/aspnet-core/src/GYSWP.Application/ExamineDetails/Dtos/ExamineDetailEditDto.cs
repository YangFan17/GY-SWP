using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;

namespace  GYSWP.ExamineDetails.Dtos
{
    public class ExamineDetailEditDto : Entity<Guid?>, IHasCreationTime
    {
		/// <summary>
		/// CriterionExamineId
		/// </summary>
		[Required(ErrorMessage="CriterionExamineId不能为空")]
		public Guid CriterionExamineId { get; set; }



		/// <summary>
		/// ClauseId
		/// </summary>
		[Required(ErrorMessage="ClauseId不能为空")]
		public Guid ClauseId { get; set; }

        /// <summary>
        /// 所属标准Id
        /// </summary>
        [Required]
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [Required(ErrorMessage="Status不能为空")]
		public ResultStatus Status { get; set; }



		/// <summary>
		/// Result
		/// </summary>
		[Required(ErrorMessage="Result不能为空")]
		public ExamineStatus Result { get; set; }



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
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// CreatorEmpeeId
		/// </summary>
		[Required(ErrorMessage="CreatorEmpeeId不能为空")]
		public string CreatorEmpeeId { get; set; }



		/// <summary>
		/// CreatorEmpName
		/// </summary>
		public string CreatorEmpName { get; set; }
    }
}