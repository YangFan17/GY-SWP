

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_ForkliftChecks;

namespace GYSWP.LC_ForkliftChecks.Dtos
{
    public class LC_ForkliftCheckListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// TimeLogId
		/// </summary>
		[Required(ErrorMessage="TimeLogId不能为空")]
		public Guid TimeLogId { get; set; }



		/// <summary>
		/// EquiNo
		/// </summary>
		public string EquiNo { get; set; }



		/// <summary>
		/// ResponsibleName
		/// </summary>
		public string ResponsibleName { get; set; }



		/// <summary>
		/// SupervisorName
		/// </summary>
		public string SupervisorName { get; set; }



		/// <summary>
		/// RunTime
		/// </summary>
		public DateTime? RunTime { get; set; }



		/// <summary>
		/// BeginTime
		/// </summary>
		public DateTime? BeginTime { get; set; }



		/// <summary>
		/// EndTime
		/// </summary>
		public DateTime? EndTime { get; set; }



		/// <summary>
		/// IslubricatingOk
		/// </summary>
		[Required(ErrorMessage="IslubricatingOk不能为空")]
		public bool IslubricatingOk { get; set; }



		/// <summary>
		/// IsBatteryBad
		/// </summary>
		[Required(ErrorMessage="IsBatteryBad不能为空")]
		public bool IsBatteryBad { get; set; }



		/// <summary>
		/// IsTurnOrBreakOk
		/// </summary>
		[Required(ErrorMessage="IsTurnOrBreakOk不能为空")]
		public bool IsTurnOrBreakOk { get; set; }



		/// <summary>
		/// IsLightOrHornOk
		/// </summary>
		[Required(ErrorMessage="IsLightOrHornOk不能为空")]
		public bool IsLightOrHornOk { get; set; }



		/// <summary>
		/// IsFullCharged
		/// </summary>
		[Required(ErrorMessage="IsFullCharged不能为空")]
		public bool IsFullCharged { get; set; }



		/// <summary>
		/// IsForkLifhOk
		/// </summary>
		[Required(ErrorMessage="IsForkLifhOk不能为空")]
		public bool IsForkLifhOk { get; set; }



		/// <summary>
		/// IsRunFullCharged
		/// </summary>
		[Required(ErrorMessage="IsRunFullCharged不能为空")]
		public bool IsRunFullCharged { get; set; }



		/// <summary>
		/// IsRunTurnOrBreakOk
		/// </summary>
		[Required(ErrorMessage="IsRunTurnOrBreakOk不能为空")]
		public bool IsRunTurnOrBreakOk { get; set; }



		/// <summary>
		/// IsRunLightOrHornOk
		/// </summary>
		[Required(ErrorMessage="IsRunLightOrHornOk不能为空")]
		public bool IsRunLightOrHornOk { get; set; }



		/// <summary>
		/// IsRunSoundOk
		/// </summary>
		[Required(ErrorMessage="IsRunSoundOk不能为空")]
		public bool IsRunSoundOk { get; set; }



		/// <summary>
		/// IsParkStandard
		/// </summary>
		[Required(ErrorMessage="IsParkStandard不能为空")]
		public bool IsParkStandard { get; set; }



		/// <summary>
		/// IsShutPower
		/// </summary>
		[Required(ErrorMessage="IsShutPower不能为空")]
		public bool IsShutPower { get; set; }



		/// <summary>
		/// IsNeedCharge
		/// </summary>
		[Required(ErrorMessage="IsNeedCharge不能为空")]
		public bool IsNeedCharge { get; set; }



		/// <summary>
		/// IsClean
		/// </summary>
		[Required(ErrorMessage="IsClean不能为空")]
		public bool IsClean { get; set; }



		/// <summary>
		/// Troubleshooting
		/// </summary>
		public string Troubleshooting { get; set; }



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