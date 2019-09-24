
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_KyjFunctionRecords;

namespace  GYSWP.LC_KyjFunctionRecords.Dtos
{
    public class LC_KyjFunctionRecordEditDto
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
		/// IsDeviceComplete
		/// </summary>
		public bool? IsDeviceComplete { get; set; }



		/// <summary>
		/// IsPipelineOk
		/// </summary>
		public bool? IsPipelineOk { get; set; }



		/// <summary>
		/// IsLubricatingOilOk
		/// </summary>
		public bool? IsLubricatingOilOk { get; set; }



		/// <summary>
		/// IsVentilatingFanOpen
		/// </summary>
		public bool? IsVentilatingFanOpen { get; set; }



		/// <summary>
		/// IsSafetyValveOk
		/// </summary>
		public bool? IsSafetyValveOk { get; set; }



		/// <summary>
		/// IsPressureNormal
		/// </summary>
		public bool? IsPressureNormal { get; set; }



		/// <summary>
		/// IsPCShowNormal
		/// </summary>
		public bool? IsPCShowNormal { get; set; }



		/// <summary>
		/// IsRunningSoundBad
		/// </summary>
		public bool? IsRunningSoundBad { get; set; }



		/// <summary>
		/// IsLsLqLyOk
		/// </summary>
		public bool? IsLsLqLyOk { get; set; }



		/// <summary>
		/// IsDrainValveOk
		/// </summary>
		public bool? IsDrainValveOk { get; set; }



		/// <summary>
		/// IsPowerSupplyClose
		/// </summary>
		public bool? IsPowerSupplyClose { get; set; }



		/// <summary>
		/// IsDeviceClean
		/// </summary>
		public bool? IsDeviceClean { get; set; }



		/// <summary>
		/// Desc
		/// </summary>
		public string Desc { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }




    }
}