
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_LPMaintainRecords;

namespace  GYSWP.LC_LPMaintainRecords.Dtos
{
    public class LC_LPMaintainRecordEditDto
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
		/// IsLineDamaged
		/// </summary>
		public bool? IsLineDamaged { get; set; }



		/// <summary>
		/// IsControlButOk
		/// </summary>
		public bool? IsControlButOk { get; set; }



		/// <summary>
		/// IsScramSwitchOk
		/// </summary>
		public bool? IsScramSwitchOk { get; set; }



		/// <summary>
		/// IsDeviceCleaning
		/// </summary>
		public bool? IsDeviceCleaning { get; set; }



		/// <summary>
		/// IsGuardrailOk
		/// </summary>
		public bool? IsGuardrailOk { get; set; }



		/// <summary>
		/// IsOutriggerLegOk
		/// </summary>
		public bool? IsOutriggerLegOk { get; set; }



		/// <summary>
		/// IsChainGroupTightness
		/// </summary>
		public bool? IsChainGroupTightness { get; set; }



		/// <summary>
		/// IsScrewFastening
		/// </summary>
		public bool? IsScrewFastening { get; set; }



		/// <summary>
		/// IsOilLevelSatisfy
		/// </summary>
		public bool? IsOilLevelSatisfy { get; set; }



		/// <summary>
		/// IsMotorRunning
		/// </summary>
		public bool? IsMotorRunning { get; set; }



		/// <summary>
		/// IsLiftingMachineBad
		/// </summary>
		public bool? IsLiftingMachineBad { get; set; }



		/// <summary>
		/// DiscoverProblems
		/// </summary>
		public string DiscoverProblems { get; set; }



		/// <summary>
		/// ProcessingResult
		/// </summary>
		public string ProcessingResult { get; set; }




    }
}