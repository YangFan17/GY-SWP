
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.EntryExitRegistrations;

namespace  GYSWP.EntryExitRegistrations.Dtos
{
    public class EntryExitRegistrationEditDto:EntityDto<long?>, IHasCreationTime
    {

        /// <summary>
        /// EmployeeName
        /// </summary>
        [Required(ErrorMessage= "EmployeeName不能为空")]
		public string EmployeeName { get; set; }



        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage = "EmployeeId不能为空")]
        public string EmployeeId { get; set; }



        /// <summary>
        /// EntryTime
        /// </summary>
        public DateTime? EntryTime { get; set; }



		/// <summary>
		/// ExitTime
		/// </summary>
		public DateTime? ExitTime { get; set; }



		/// <summary>
		/// ReasonsForWarehousing
		/// </summary>
		public string ReasonsForWarehousing { get; set; }



		/// <summary>
		/// IsAbnormal
		/// </summary>
		public bool? IsAbnormal { get; set; }



		/// <summary>
		/// Remarks
		/// </summary>
		public string Remarks { get; set; }



        /// <summary>
        /// TimeLogId
        /// </summary>
        [Required(ErrorMessage = "TimeLogId不能为空")]
        public Guid TimeLogId { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage = "CreationTime不能为空")]
        public DateTime CreationTime { get; set; }




    }
}