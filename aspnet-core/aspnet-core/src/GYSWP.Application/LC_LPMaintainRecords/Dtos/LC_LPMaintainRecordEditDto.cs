
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_LPMaintainRecords;

namespace  GYSWP.LC_LPMaintainRecords.Dtos
{
    public class LC_LPMaintainRecordEditDto : EntityDto<Guid?>, IHasCreationTime
    {

        /// <summary>
        /// ResponsibleName
        /// </summary>
        [Required(ErrorMessage= "ResponsibleName不能为空")]
		public string ResponsibleName { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



        /// <summary>
        /// SupervisorName
        /// </summary>
        [Required(ErrorMessage= "SupervisorName不能为空")]
		public string SupervisorName { get; set; }



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



        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage = "EmployeeId不能为空")]
        [StringLength(200)]
        public string EmployeeId { get; set; }



        /// <summary>
        ///  EmployeeName
        /// </summary>
        [Required(ErrorMessage = "EmployeeName不能为空")]
        [StringLength(50)]
        public string EmployeeName { get; set; }




    }
}