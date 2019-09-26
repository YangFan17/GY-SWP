
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_ForkliftMonthWhRecords;

namespace  GYSWP.LC_ForkliftMonthWhRecords.Dtos
{
    public class LC_ForkliftMonthWhRecordEditDto
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
        /// 叉车编号
        /// </summary>
        [StringLength(50)]
        public virtual string EquiNo { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// IsScrewBad
		/// </summary>
		public bool? IsScrewBad { get; set; }



		/// <summary>
		/// IsTireSurfaceBad
		/// </summary>
		public bool? IsTireSurfaceBad { get; set; }



		/// <summary>
		/// IsDjyDczBad
		/// </summary>
		public bool? IsDjyDczBad { get; set; }



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
		/// IsCircuitsBad
		/// </summary>
		public bool? IsCircuitsBad { get; set; }



		/// <summary>
		/// IsTerminalBad
		/// </summary>
		public bool? IsTerminalBad { get; set; }



		/// <summary>
		/// IsTubingJackBad
		/// </summary>
		public bool? IsTubingJackBad { get; set; }



		/// <summary>
		/// IsPowerControlBad
		/// </summary>
		public bool? IsPowerControlBad { get; set; }



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