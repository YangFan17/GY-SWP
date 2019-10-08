
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_ForkliftChecks;

namespace  GYSWP.LC_ForkliftChecks.Dtos
{
    public class LC_ForkliftCheckEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// TimeLogId
		/// </summary>
		public Guid? TimeLogId { get; set; }



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
		public bool? IslubricatingOk { get; set; }



		/// <summary>
		/// IsBatteryBad
		/// </summary>
		public bool? IsBatteryBad { get; set; }



		/// <summary>
		/// IsTurnOrBreakOk
		/// </summary>
		public bool? IsTurnOrBreakOk { get; set; }



		/// <summary>
		/// IsLightOrHornOk
		/// </summary>
		public bool? IsLightOrHornOk { get; set; }



		/// <summary>
		/// IsFullCharged
		/// </summary>
		public bool? IsFullCharged { get; set; }



		/// <summary>
		/// IsForkLifhOk
		/// </summary>
		public bool? IsForkLifhOk { get; set; }



		/// <summary>
		/// IsRunFullCharged
		/// </summary>
		public bool? IsRunFullCharged { get; set; }



		/// <summary>
		/// IsRunTurnOrBreakOk
		/// </summary>
		public bool? IsRunTurnOrBreakOk { get; set; }



		/// <summary>
		/// IsRunLightOrHornOk
		/// </summary>
		public bool? IsRunLightOrHornOk { get; set; }



		/// <summary>
		/// IsRunSoundOk
		/// </summary>
		public bool? IsRunSoundOk { get; set; }



		/// <summary>
		/// IsParkStandard
		/// </summary>
		public bool? IsParkStandard { get; set; }



		/// <summary>
		/// IsShutPower
		/// </summary>
		public bool? IsShutPower { get; set; }



		/// <summary>
		/// IsNeedCharge
		/// </summary>
		public bool? IsNeedCharge { get; set; }



		/// <summary>
		/// IsClean
		/// </summary>
		public bool? IsClean { get; set; }



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