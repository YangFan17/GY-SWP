

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_KyjWeekMaintainRecords;

namespace GYSWP.LC_KyjWeekMaintainRecords.Dtos
{
    public class LC_KyjWeekMaintainRecordListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// DeviceID
		/// </summary>
		[Required(ErrorMessage="DeviceID不能为空")]
		public string DeviceID { get; set; }



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
		/// IsScrewFastening
		/// </summary>
		public bool? IsScrewFastening { get; set; }



		/// <summary>
		/// IsOilPressureOk
		/// </summary>
		public bool? IsOilPressureOk { get; set; }



		/// <summary>
		/// IsOilAndGasBad
		/// </summary>
		public bool? IsOilAndGasBad { get; set; }



		/// <summary>
		/// IsPressureGaugePointerOk
		/// </summary>
		public bool? IsPressureGaugePointerOk { get; set; }



		/// <summary>
		/// IsTheOilLevelOk
		/// </summary>
		public bool? IsTheOilLevelOk { get; set; }



		/// <summary>
		/// IsAirFilterOk
		/// </summary>
		public bool? IsAirFilterOk { get; set; }



		/// <summary>
		/// IsDeviceClean
		/// </summary>
		public bool? IsDeviceClean { get; set; }



		/// <summary>
		/// IsPressureGaugeOk
		/// </summary>
		public bool? IsPressureGaugeOk { get; set; }



		/// <summary>
		/// IsSafetyValveOk
		/// </summary>
		public bool? IsSafetyValveOk { get; set; }



		/// <summary>
		/// IsCoolingPlateClean
		/// </summary>
		public bool? IsCoolingPlateClean { get; set; }



		/// <summary>
		/// IsDrainValveOpen
		/// </summary>
		public bool? IsDrainValveOpen { get; set; }



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