using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;

namespace  GYSWP.ExamineFeedbacks.Dtos
{
    public class ExamineFeedbackEditDto : Entity<Guid?>, IHasCreationTime
    {
		/// <summary>
		/// Type
		/// </summary>
		[Required(ErrorMessage="Type不能为空")]
		public FeedType Type { get; set; }



		/// <summary>
		/// BusinessId
		/// </summary>
		[Required(ErrorMessage="BusinessId不能为空")]
		public Guid BusinessId { get; set; }



		/// <summary>
		/// CourseType
		/// </summary>
		public string CourseType { get; set; }



		/// <summary>
		/// Reason
		/// </summary>
		public string Reason { get; set; }



		/// <summary>
		/// Solution
		/// </summary>
		public string Solution { get; set; }

		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// EmployeeId
		/// </summary>
		public string EmployeeId { get; set; }

		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }
    }
}