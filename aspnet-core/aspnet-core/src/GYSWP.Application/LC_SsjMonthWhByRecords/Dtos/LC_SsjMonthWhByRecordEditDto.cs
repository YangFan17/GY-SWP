
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_SsjMonthWhByRecords;

namespace  GYSWP.LC_SsjMonthWhByRecords.Dtos
{
    public class LC_SsjMonthWhByRecordEditDto
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
        public virtual string EquiNo { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// IsPartLubrication
		/// </summary>
		public bool? IsPartLubrication { get; set; }



		/// <summary>
		/// IsShapeBad
		/// </summary>
		public bool? IsShapeBad { get; set; }



		/// <summary>
		/// IsInsulationBad
		/// </summary>
		public bool? IsInsulationBad { get; set; }



		/// <summary>
		/// IsButtonBad
		/// </summary>
		public bool? IsButtonBad { get; set; }



		/// <summary>
		/// IsBoltBad
		/// </summary>
		public bool? IsBoltBad { get; set; }



		/// <summary>
		/// IsLineBad
		/// </summary>
		public bool? IsLineBad { get; set; }



		/// <summary>
		/// IsPowerCircuitBad
		/// </summary>
		public bool? IsPowerCircuitBad { get; set; }



		/// <summary>
		/// IsChainTensionBad
		/// </summary>
		public bool? IsChainTensionBad { get; set; }



		/// <summary>
		/// IsBearingRunningOk
		/// </summary>
		public bool? IsBearingRunningOk { get; set; }



		/// <summary>
		/// IsEviceBad
		/// </summary>
		public bool? IsEviceBad { get; set; }



		/// <summary>
		/// IsSwitchOk
		/// </summary>
		public bool? IsSwitchOk { get; set; }



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