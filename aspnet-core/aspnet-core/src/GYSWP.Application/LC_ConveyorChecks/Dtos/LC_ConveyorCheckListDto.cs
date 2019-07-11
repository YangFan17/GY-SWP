

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_ConveyorChecks;

namespace GYSWP.LC_ConveyorChecks.Dtos
{
    public class LC_ConveyorCheckListDto : EntityDto<Guid>,IHasCreationTime 
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
		/// IsEquiFaceClean
		/// </summary>
		[Required(ErrorMessage="IsEquiFaceClean不能为空")]
		public bool IsEquiFaceClean { get; set; }



		/// <summary>
		/// IsSteadyOk
		/// </summary>
		[Required(ErrorMessage="IsSteadyOk不能为空")]
		public bool IsSteadyOk { get; set; }



		/// <summary>
		/// IsScrewOk
		/// </summary>
		[Required(ErrorMessage="IsScrewOk不能为空")]
		public bool IsScrewOk { get; set; }



		/// <summary>
		/// IsButtonOk
		/// </summary>
		[Required(ErrorMessage="IsButtonOk不能为空")]
		public bool IsButtonOk { get; set; }



		/// <summary>
		/// IsElcLineBad
		/// </summary>
		[Required(ErrorMessage="IsElcLineBad不能为空")]
		public bool IsElcLineBad { get; set; }



		/// <summary>
		/// IsBeltSlant
		/// </summary>
		[Required(ErrorMessage="IsBeltSlant不能为空")]
		public bool IsBeltSlant { get; set; }



		/// <summary>
		/// IsBearingOk
		/// </summary>
		[Required(ErrorMessage="IsBearingOk不能为空")]
		public bool IsBearingOk { get; set; }



		/// <summary>
		/// IsSoundOk
		/// </summary>
		[Required(ErrorMessage="IsSoundOk不能为空")]
		public bool IsSoundOk { get; set; }



		/// <summary>
		/// IsMotor
		/// </summary>
		[Required(ErrorMessage="IsMotor不能为空")]
		public bool IsMotor { get; set; }



		/// <summary>
		/// IsShutPower
		/// </summary>
		[Required(ErrorMessage="IsShutPower不能为空")]
		public bool IsShutPower { get; set; }



		/// <summary>
		/// IsBeltBad
		/// </summary>
		[Required(ErrorMessage="IsBeltBad不能为空")]
		public bool IsBeltBad { get; set; }



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