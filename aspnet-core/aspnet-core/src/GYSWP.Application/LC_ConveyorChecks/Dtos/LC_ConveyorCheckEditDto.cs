
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_ConveyorChecks;

namespace  GYSWP.LC_ConveyorChecks.Dtos
{
    public class LC_ConveyorCheckEditDto
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
		/// IsEquiFaceClean
		/// </summary>
		public bool? IsEquiFaceClean { get; set; }



		/// <summary>
		/// IsSteadyOk
		/// </summary>
		public bool? IsSteadyOk { get; set; }



		/// <summary>
		/// IsScrewOk
		/// </summary>
		public bool? IsScrewOk { get; set; }



		/// <summary>
		/// IsButtonOk
		/// </summary>
		public bool? IsButtonOk { get; set; }



		/// <summary>
		/// IsElcLineBad
		/// </summary>
		public bool? IsElcLineBad { get; set; }



		/// <summary>
		/// IsBeltSlant
		/// </summary>
		public bool? IsBeltSlant { get; set; }



		/// <summary>
		/// IsBearingOk
		/// </summary>
		public bool? IsBearingOk { get; set; }



		/// <summary>
		/// IsSoundOk
		/// </summary>
		public bool? IsSoundOk { get; set; }



		/// <summary>
		/// IsMotor
		/// </summary>
		public bool? IsMotor { get; set; }



		/// <summary>
		/// IsShutPower
		/// </summary>
		public bool? IsShutPower { get; set; }



		/// <summary>
		/// IsBeltBad
		/// </summary>
		public bool? IsBeltBad { get; set; }



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