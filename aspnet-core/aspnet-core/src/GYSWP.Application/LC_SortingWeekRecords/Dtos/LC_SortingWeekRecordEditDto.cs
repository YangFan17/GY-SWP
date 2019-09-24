
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_SortingWeekRecords;

namespace  GYSWP.LC_SortingWeekRecords.Dtos
{
    public class LC_SortingWeekRecordEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
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
		/// IsInspectZjd
		/// </summary>
		public bool? IsInspectZjd { get; set; }



		/// <summary>
		/// IsBearingEtcBad
		/// </summary>
		public bool? IsBearingEtcBad { get; set; }



		/// <summary>
		/// IsLinePipeBad
		/// </summary>
		public bool? IsLinePipeBad { get; set; }



		/// <summary>
		/// IsNetworkDataLineBad
		/// </summary>
		public bool? IsNetworkDataLineBad { get; set; }



		/// <summary>
		/// IsSbTtBad
		/// </summary>
		public bool? IsSbTtBad { get; set; }



		/// <summary>
		/// IsBeltSurfaceClean
		/// </summary>
		public bool? IsBeltSurfaceClean { get; set; }



		/// <summary>
		/// IsKzxlQJOk
		/// </summary>
		public bool? IsKzxlQJOk { get; set; }



		/// <summary>
		/// IsControlBtnOk
		/// </summary>
		public bool? IsControlBtnOk { get; set; }



		/// <summary>
		/// IsBzjBearingEtcBad
		/// </summary>
		public bool? IsBzjBearingEtcBad { get; set; }



		/// <summary>
		/// IsTracheaBad
		/// </summary>
		public bool? IsTracheaBad { get; set; }



		/// <summary>
		/// IsBzjNetworkDataLineBad
		/// </summary>
		public bool? IsBzjNetworkDataLineBad { get; set; }



		/// <summary>
		/// IsFqdJrsGwjBad
		/// </summary>
		public bool? IsFqdJrsGwjBad { get; set; }



		/// <summary>
		/// IsCylinderOk
		/// </summary>
		public bool? IsCylinderOk { get; set; }



		/// <summary>
		/// IsLiftingGuideBarBad
		/// </summary>
		public bool? IsLiftingGuideBarBad { get; set; }



		/// <summary>
		/// IsPrintHeadBad
		/// </summary>
		public bool? IsPrintHeadBad { get; set; }



		/// <summary>
		/// IsPlacementPositionOk
		/// </summary>
		public bool? IsPlacementPositionOk { get; set; }



		/// <summary>
		/// IsParameterSettingOk
		/// </summary>
		public bool? IsParameterSettingOk { get; set; }



		/// <summary>
		/// IsWipeLens
		/// </summary>
		public bool? IsWipeLens { get; set; }



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