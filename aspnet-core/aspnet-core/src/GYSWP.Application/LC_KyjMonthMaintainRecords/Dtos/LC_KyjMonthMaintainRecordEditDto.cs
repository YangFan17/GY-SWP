
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_KyjMonthMaintainRecords;

namespace  GYSWP.LC_KyjMonthMaintainRecords.Dtos
{
    public class LC_KyjMonthMaintainRecordEditDto
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




    }
}