

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.CriterionExamines;
using GYSWP.GYEnums;

namespace GYSWP.CriterionExamines.Dtos
{
    public class CriterionExamineListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// Title
		/// </summary>
		[Required(ErrorMessage="Title不能为空")]
		public string Title { get; set; }



		/// <summary>
		/// Type
		/// </summary>
		[Required(ErrorMessage="Type不能为空")]
		public CriterionExamineType Type { get; set; }



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



		/// <summary>
		/// CreatorDeptId
		/// </summary>
		[Required(ErrorMessage="CreatorDeptId不能为空")]
		public long CreatorDeptId { get; set; }



		/// <summary>
		/// DeptId
		/// </summary>
		[Required(ErrorMessage="DeptId不能为空")]
		public long DeptId { get; set; }



		/// <summary>
		/// CreatorDeptName
		/// </summary>
		public string CreatorDeptName { get; set; }



		/// <summary>
		/// DeptName
		/// </summary>
		public string DeptName { get; set; }




    }
}