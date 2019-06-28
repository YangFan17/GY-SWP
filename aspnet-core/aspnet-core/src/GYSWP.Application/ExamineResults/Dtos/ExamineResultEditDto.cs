
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.ExamineResults;

namespace  GYSWP.ExamineResults.Dtos
{
    public class ExamineResultEditDto: EntityDto<Guid?>, IHasCreationTime
    {       
		/// <summary>
		/// ExamineDetailId
		/// </summary>
		[Required(ErrorMessage="ExamineDetailId不能为空")]
		public Guid ExamineDetailId { get; set; }

		/// <summary>
		/// Content
		/// </summary>
		[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }

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
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }
    }
}