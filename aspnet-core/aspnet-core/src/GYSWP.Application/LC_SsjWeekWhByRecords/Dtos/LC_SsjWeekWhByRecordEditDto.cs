
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_SsjWeekWhByRecords;

namespace  GYSWP.LC_SsjWeekWhByRecords.Dtos
{
    public class LC_SsjWeekWhByRecordEditDto
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
		/// IsPartLubrication
		/// </summary>
		public bool? IsPartLubrication { get; set; }



		/// <summary>
		/// IsShapeBad
		/// </summary>
		public bool? IsShapeBad { get; set; }



		/// <summary>
		/// IsBoltBad
		/// </summary>
		public bool? IsBoltBad { get; set; }



		/// <summary>
		/// IsButtonBad
		/// </summary>
		public bool? IsButtonBad { get; set; }



		/// <summary>
		/// IsPowerCircuitBad
		/// </summary>
		public bool? IsPowerCircuitBad { get; set; }



		/// <summary>
		/// IsLineBad
		/// </summary>
		public bool? IsLineBad { get; set; }



		/// <summary>
		/// IsBearingRunningOk
		/// </summary>
		public bool? IsBearingRunningOk { get; set; }



		/// <summary>
		/// IsChainTensionBad
		/// </summary>
		public bool? IsChainTensionBad { get; set; }



		/// <summary>
		/// IsBeltBad
		/// </summary>
		public bool? IsBeltBad { get; set; }



		/// <summary>
		/// IseviceBad
		/// </summary>
		public bool? IseviceBad { get; set; }



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