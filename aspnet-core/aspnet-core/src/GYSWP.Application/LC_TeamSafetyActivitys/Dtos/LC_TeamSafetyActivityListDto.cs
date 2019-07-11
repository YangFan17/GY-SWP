

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_TeamSafetyActivitys;

namespace GYSWP.LC_TeamSafetyActivitys.Dtos
{
    public class LC_TeamSafetyActivityListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// TimeLogId
		/// </summary>
		[Required(ErrorMessage="TimeLogId不能为空")]
		public Guid TimeLogId { get; set; }



		/// <summary>
		/// SafetyMeeting
		/// </summary>
		public string SafetyMeeting { get; set; }



		/// <summary>
		/// IsSafeEquipOk
		/// </summary>
		[Required(ErrorMessage="IsSafeEquipOk不能为空")]
		public bool IsSafeEquipOk { get; set; }



		/// <summary>
		/// IsEmpHealth
		/// </summary>
		[Required(ErrorMessage="IsEmpHealth不能为空")]
		public bool IsEmpHealth { get; set; }



		/// <summary>
		/// IsTdjOrLsjOk
		/// </summary>
		[Required(ErrorMessage="IsTdjOrLsjOk不能为空")]
		public bool IsTdjOrLsjOk { get; set; }



		/// <summary>
		/// IsAisleOk
		/// </summary>
		[Required(ErrorMessage="IsAisleOk不能为空")]
		public bool IsAisleOk { get; set; }



		/// <summary>
		/// IsExitBad
		/// </summary>
		[Required(ErrorMessage="IsExitBad不能为空")]
		public bool IsExitBad { get; set; }



		/// <summary>
		/// IsFireEquipBad
		/// </summary>
		[Required(ErrorMessage="IsFireEquipBad不能为空")]
		public bool IsFireEquipBad { get; set; }



		/// <summary>
		/// IsSafeMarkClean
		/// </summary>
		[Required(ErrorMessage="IsSafeMarkClean不能为空")]
		public bool IsSafeMarkClean { get; set; }



		/// <summary>
		/// IsSafeMarkFall
		/// </summary>
		[Required(ErrorMessage="IsSafeMarkFall不能为空")]
		public bool IsSafeMarkFall { get; set; }



		/// <summary>
		/// EmpSafeAdvice
		/// </summary>
		public string EmpSafeAdvice { get; set; }



		/// <summary>
		/// CommonCigaretNum
		/// </summary>
		public int? CommonCigaretNum { get; set; }



		/// <summary>
		/// ShapedCigaretNum
		/// </summary>
		public int? ShapedCigaretNum { get; set; }



		/// <summary>
		/// BeginSortTime
		/// </summary>
		public DateTime? BeginSortTime { get; set; }



		/// <summary>
		/// EndSortTime
		/// </summary>
		public DateTime? EndSortTime { get; set; }



		/// <summary>
		/// NormalStopTime
		/// </summary>
		public DateTime? NormalStopTime { get; set; }



		/// <summary>
		/// AbnormalStopTime
		/// </summary>
		public DateTime? AbnormalStopTime { get; set; }



		/// <summary>
		/// IsNotDanger
		/// </summary>
		[Required(ErrorMessage="IsNotDanger不能为空")]
		public bool IsNotDanger { get; set; }



		/// <summary>
		/// IsOtherAdmittance
		/// </summary>
		[Required(ErrorMessage="IsOtherAdmittance不能为空")]
		public bool IsOtherAdmittance { get; set; }



		/// <summary>
		/// IsViolation
		/// </summary>
		[Required(ErrorMessage="IsViolation不能为空")]
		public bool IsViolation { get; set; }



		/// <summary>
		/// IsElcOrGasShut
		/// </summary>
		[Required(ErrorMessage="IsElcOrGasShut不能为空")]
		public bool IsElcOrGasShut { get; set; }



		/// <summary>
		/// IsCloseWindow
		/// </summary>
		[Required(ErrorMessage="IsCloseWindow不能为空")]
		public bool IsCloseWindow { get; set; }



		/// <summary>
		/// SafeSupervision
		/// </summary>
		public string SafeSupervision { get; set; }



		/// <summary>
		/// ResponsibleName
		/// </summary>
		public string ResponsibleName { get; set; }



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




    }
}