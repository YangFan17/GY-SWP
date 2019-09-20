
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_ScanRecords;

namespace  GYSWP.LC_ScanRecords.Dtos
{
    public class LC_ScanRecordEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// TimeLogId
		/// </summary>
		//[Required(ErrorMessage="TimeLogId不能为空")]
		public Guid? TimeLogId { get; set; }



		/// <summary>
		/// Type
		/// </summary>
		//[Required(ErrorMessage="Type不能为空")]
		public int Type { get; set; }



		/// <summary>
		/// Status
		/// </summary>
		//[Required(ErrorMessage="Status不能为空")]
		public int Status { get; set; }



		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }



		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		//[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }




    }
}