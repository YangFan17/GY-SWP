
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.DocRevisions;
using GYSWP.GYEnums;

namespace GYSWP.DocRevisions.Dtos
{
    public class DocRevisionEditDto : FullAuditedEntity<Guid?>
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
		public string EmployeeId { get; set; }



		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }



		/// <summary>
		/// RevisionType
		/// </summary>
		public RevisionType RevisionType { get; set; }



		/// <summary>
		/// Status
		/// </summary>
		public RevisionStatus Status { get; set; }
        public string DeptId { get; set; }
    }
}