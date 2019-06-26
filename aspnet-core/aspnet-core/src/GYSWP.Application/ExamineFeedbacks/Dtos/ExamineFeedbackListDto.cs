

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.ExamineFeedbacks;
using GYSWP.GYEnums;

namespace GYSWP.ExamineFeedbacks.Dtos
{
    public class ExamineFeedbackListDto : EntityDto<Guid>,IHasCreationTime 
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
		[Required(ErrorMessage="CourseType不能为空")]
		public FactorType CourseType { get; set; }



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
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }



		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }




    }
}