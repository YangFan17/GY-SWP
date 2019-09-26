
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
        /// ResponsibleName  责任人
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "ResponsibleName不能为空")]
        public string ResponsibleName { get; set; }

        /// <summary>
        /// SupervisorName  监管人
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "SupervisorName不能为空")]
        public string SupervisorName { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        [Required]
        [StringLength(200)]
        public string EmployeeId { get; set; }

        /// <summary>
        /// 员工快照    
        /// </summary>
        [StringLength(50)]
        public string EmployeeName { get; set; }

        /// <summary>
        /// 分拣设备的名字
        /// </summary>
        [StringLength(50)]
        public string EquiNo { get; set; }

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