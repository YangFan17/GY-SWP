
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_QualityRecords;

namespace  GYSWP.LC_QualityRecords.Dtos
{
    public class LC_QualityRecordEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// TimeLogId
		/// </summary>
		[Required(ErrorMessage="TimeLogId不能为空")]
		public Guid TimeLogId { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// WholesaleAmount
		/// </summary>
		public decimal? WholesaleAmount { get; set; }



		/// <summary>
		/// SaleQuantity
		/// </summary>
		public int? SaleQuantity { get; set; }



		/// <summary>
		/// CarNo
		/// </summary>
		public string CarNo { get; set; }



		/// <summary>
		/// CompensationAmount
		/// </summary>
		public decimal? CompensationAmount { get; set; }



		/// <summary>
		/// CarrierName
		/// </summary>
		public string CarrierName { get; set; }



		/// <summary>
		/// ClerkName
		/// </summary>
		public string ClerkName { get; set; }



		/// <summary>
		/// HandoverTime
		/// </summary>
		public DateTime? HandoverTime { get; set; }



		/// <summary>
		/// Amount
		/// </summary>
		public decimal? Amount { get; set; }



		/// <summary>
		/// HandManName
		/// </summary>
		public string HandManName { get; set; }



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
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }




    }
}