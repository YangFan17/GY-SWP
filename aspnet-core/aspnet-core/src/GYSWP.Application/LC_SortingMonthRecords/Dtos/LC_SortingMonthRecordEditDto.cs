
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_SortingMonthRecords;

namespace  GYSWP.LC_SortingMonthRecords.Dtos
{
    public class LC_SortingMonthRecordEditDto
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
		/// IsFjxButtonOk
		/// </summary>
		public bool? IsFjxButtonOk { get; set; }



		/// <summary>
		/// IsTensioningModerate
		/// </summary>
		public bool? IsTensioningModerate { get; set; }



		/// <summary>
		/// IsFjxDeviceBad
		/// </summary>
		public bool? IsFjxDeviceBad { get; set; }



		/// <summary>
		/// IsFjxBoltBad
		/// </summary>
		public bool? IsFjxBoltBad { get; set; }



		/// <summary>
		/// IsFjxProtectiveDeviceBad
		/// </summary>
		public bool? IsFjxProtectiveDeviceBad { get; set; }



		/// <summary>
		/// IsFjxNetworkDataLineBad
		/// </summary>
		public bool? IsFjxNetworkDataLineBad { get; set; }



		/// <summary>
		/// IsFjxDlbhJdxOk
		/// </summary>
		public bool? IsFjxDlbhJdxOk { get; set; }



		/// <summary>
		/// IsSbTtBad
		/// </summary>
		public bool? IsSbTtBad { get; set; }



		/// <summary>
		/// IsChainLubeOk
		/// </summary>
		public bool? IsChainLubeOk { get; set; }



		/// <summary>
		/// IsBzjButtonOk
		/// </summary>
		public bool? IsBzjButtonOk { get; set; }



		/// <summary>
		/// IsJlkModerate
		/// </summary>
		public bool? IsJlkModerate { get; set; }



		/// <summary>
		/// IsBzjDeviceBad
		/// </summary>
		public bool? IsBzjDeviceBad { get; set; }



		/// <summary>
		/// IsBzjBoltBad
		/// </summary>
		public bool? IsBzjBoltBad { get; set; }



		/// <summary>
		/// IsBzjProtectiveDeviceBad
		/// </summary>
		public bool? IsBzjProtectiveDeviceBad { get; set; }



		/// <summary>
		/// IsBzjNetworkDataLineBad
		/// </summary>
		public bool? IsBzjNetworkDataLineBad { get; set; }



		/// <summary>
		/// IsBzjDlbhJdxOk
		/// </summary>
		public bool? IsBzjDlbhJdxOk { get; set; }



		/// <summary>
		/// IsSslClear
		/// </summary>
		public bool? IsSslClear { get; set; }



		/// <summary>
		/// IsCylinderJointBad
		/// </summary>
		public bool? IsCylinderJointBad { get; set; }



		/// <summary>
		/// IsTbjDeviceBad
		/// </summary>
		public bool? IsTbjDeviceBad { get; set; }



		/// <summary>
		/// IsTbjBoltBad
		/// </summary>
		public bool? IsTbjBoltBad { get; set; }



		/// <summary>
		/// IsOpticalSensorOk
		/// </summary>
		public bool? IsOpticalSensorOk { get; set; }



		/// <summary>
		/// IsDmjProtectiveDeviceBad
		/// </summary>
		public bool? IsDmjProtectiveDeviceBad { get; set; }



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