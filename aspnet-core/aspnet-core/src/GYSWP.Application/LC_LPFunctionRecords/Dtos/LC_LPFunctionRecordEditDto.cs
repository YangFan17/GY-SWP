
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_LPFunctionRecords;

namespace  GYSWP.LC_LPFunctionRecords.Dtos
{
    public class LC_LPFunctionRecordEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// DeviceID
		/// </summary>
		[Required(ErrorMessage="DeviceID不能为空")]
		public string DeviceID { get; set; }



		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// SupervisorId
		/// </summary>
		[Required(ErrorMessage="SupervisorId不能为空")]
		public string SupervisorId { get; set; }



		/// <summary>
		/// RunningTime
		/// </summary>
		public DateTime? RunningTime { get; set; }



		/// <summary>
		/// UseTime
		/// </summary>
		public DateTime? UseTime { get; set; }



		/// <summary>
		/// DownTime
		/// </summary>
		public DateTime? DownTime { get; set; }



		/// <summary>
		/// IsSwitchOk
		/// </summary>
		public bool? IsSwitchOk { get; set; }



		/// <summary>
		/// IsLiftingOk
		/// </summary>
		public bool? IsLiftingOk { get; set; }



		/// <summary>
		/// IsGuardrailOk
		/// </summary>
		public bool? IsGuardrailOk { get; set; }



		/// <summary>
		/// IsOutriggerLegOk
		/// </summary>
		public bool? IsOutriggerLegOk { get; set; }



		/// <summary>
		/// IsGroundSmooth
		/// </summary>
		public bool? IsGroundSmooth { get; set; }



		/// <summary>
		/// IsOutriggerLegStable
		/// </summary>
		public bool? IsOutriggerLegStable { get; set; }



		/// <summary>
		/// IsLevelLeveling
		/// </summary>
		public bool? IsLevelLeveling { get; set; }



		/// <summary>
		/// IsLiftingMachineBad
		/// </summary>
		public bool? IsLiftingMachineBad { get; set; }



		/// <summary>
		/// IsLoadExceeding
		/// </summary>
		public bool? IsLoadExceeding { get; set; }



		/// <summary>
		/// IsGuardrailPositionOk
		/// </summary>
		public bool? IsGuardrailPositionOk { get; set; }



		/// <summary>
		/// IsOutriggerCloseUp
		/// </summary>
		public bool? IsOutriggerCloseUp { get; set; }



		/// <summary>
		/// IsDeviceClean
		/// </summary>
		public bool? IsDeviceClean { get; set; }



		/// <summary>
		/// Desc
		/// </summary>
		public string Desc { get; set; }




    }
}