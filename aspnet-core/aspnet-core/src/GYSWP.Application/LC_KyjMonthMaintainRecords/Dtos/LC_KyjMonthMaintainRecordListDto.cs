

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_KyjMonthMaintainRecords;

namespace GYSWP.LC_KyjMonthMaintainRecords.Dtos
{
    public class LC_KyjMonthMaintainRecordListDto : EntityDto<Guid>,IHasCreationTime 
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
		/// SupervisorId
		/// </summary>
		[Required(ErrorMessage= "SupervisorName不能为空")]
		public string SupervisorName { get; set; }



		/// <summary>
		/// IsDeviceClean
		/// </summary>
		public bool? IsDeviceClean { get; set; }



		/// <summary>
		/// IsSafetyMarkClear
		/// </summary>
		public bool? IsSafetyMarkClear { get; set; }



		/// <summary>
		/// IsScrewFastening
		/// </summary>
		public bool? IsScrewFastening { get; set; }



		/// <summary>
		/// IsTheOilLevelOk
		/// </summary>
		public bool? IsTheOilLevelOk { get; set; }



		/// <summary>
		/// IsOilAndGasBad
		/// </summary>
		public bool? IsOilAndGasBad { get; set; }



		/// <summary>
		/// IsAirFilterOk
		/// </summary>
		public bool? IsAirFilterOk { get; set; }



		/// <summary>
		/// IsPressureGaugeOk
		/// </summary>
		public bool? IsPressureGaugeOk { get; set; }



		/// <summary>
		/// IsPressureGaugePointerOk
		/// </summary>
		public bool? IsPressureGaugePointerOk { get; set; }



		/// <summary>
		/// IsSafetyValveOk
		/// </summary>
		public bool? IsSafetyValveOk { get; set; }



		/// <summary>
		/// IsCoolingPlateClean
		/// </summary>
		public bool? IsCoolingPlateClean { get; set; }



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