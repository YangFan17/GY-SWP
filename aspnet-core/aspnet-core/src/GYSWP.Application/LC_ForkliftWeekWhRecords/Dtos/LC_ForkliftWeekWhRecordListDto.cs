

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_ForkliftWeekWhRecords;

namespace GYSWP.LC_ForkliftWeekWhRecords.Dtos
{
    public class LC_ForkliftWeekWhRecordListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }



		/// <summary>
		/// SuperintendentId
		/// </summary>
		[Required(ErrorMessage="SuperintendentId不能为空")]
		public string SuperintendentId { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// IsSpareParts
		/// </summary>
		public bool? IsSpareParts { get; set; }



		/// <summary>
		/// IsInspectQcEtc
		/// </summary>
		public bool? IsInspectQcEtc { get; set; }



		/// <summary>
		/// IsLimitDeviceBad
		/// </summary>
		public bool? IsLimitDeviceBad { get; set; }



		/// <summary>
		/// IsInspectRhQcEtc
		/// </summary>
		public bool? IsInspectRhQcEtc { get; set; }



		/// <summary>
		/// IsDjyDczBad
		/// </summary>
		public bool? IsDjyDczBad { get; set; }



		/// <summary>
		/// IsTerminalBad
		/// </summary>
		public bool? IsTerminalBad { get; set; }



		/// <summary>
		/// IsScrewBad
		/// </summary>
		public bool? IsScrewBad { get; set; }



		/// <summary>
		/// IsTubingJackBad
		/// </summary>
		public bool? IsTubingJackBad { get; set; }



		/// <summary>
		/// IsFilterBad
		/// </summary>
		public bool? IsFilterBad { get; set; }



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