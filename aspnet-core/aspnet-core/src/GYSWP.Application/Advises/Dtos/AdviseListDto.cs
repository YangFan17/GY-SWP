

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.Advises;

namespace GYSWP.Advises.Dtos
{
    public class AdviseListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// AdviseName
		/// </summary>
		public string AdviseName { get; set; }



		/// <summary>
		/// CurrentSituation
		/// </summary>
		public string CurrentSituation { get; set; }



		/// <summary>
		/// Solution
		/// </summary>
		public string Solution { get; set; }



		/// <summary>
		/// IsAdoption
		/// </summary>
		[Required(ErrorMessage="IsAdoption不能为空")]
		public bool IsAdoption { get; set; }



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
		/// DeptId
		/// </summary>
		[Required(ErrorMessage="DeptId不能为空")]
		public long DeptId { get; set; }



		/// <summary>
		/// DeptName
		/// </summary>
		public string DeptName { get; set; }



		/// <summary>
		/// ReviewOpinion
		/// </summary>
		public string ReviewOpinion { get; set; }



		/// <summary>
		/// ApprovalComments
		/// </summary>
		public string ApprovalComments { get; set; }




    }
}