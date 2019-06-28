

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.GYEnums;


namespace GYSWP.DocRevisions.Dtos
{
    public class DocRevisionListDto : FullAuditedEntityDto<Guid> 
    {
		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// CategoryId
		/// </summary>
		[Required(ErrorMessage="CategoryId不能为空")]
		public int CategoryId { get; set; }



		/// <summary>
		/// ApplyInfoId
		/// </summary>
		[Required(ErrorMessage="ApplyInfoId不能为空")]
		public Guid ApplyInfoId { get; set; }



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
		/// RevisionType
		/// </summary>
		[Required(ErrorMessage="RevisionType不能为空")]
		public RevisionType RevisionType { get; set; }



		/// <summary>
		/// Status
		/// </summary>
		[Required(ErrorMessage="Status不能为空")]
		public RevisionStatus Status { get; set; }
        public string DeptId { get; set; }
    }
}