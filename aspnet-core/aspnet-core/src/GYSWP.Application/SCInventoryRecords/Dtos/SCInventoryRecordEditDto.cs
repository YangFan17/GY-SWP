
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.SCInventoryRecords;

namespace  GYSWP.SCInventoryRecords.Dtos
{
    public class SCInventoryRecordEditDto : EntityDto<long?>, IHasCreationTime
    {
        
		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }



		/// <summary>
		/// CurrentStock
		/// </summary>
		public int? CurrentStock { get; set; }



		/// <summary>
		/// TotalInventory
		/// </summary>
		public int? TotalInventory { get; set; }



		/// <summary>
		/// InventoryRealNumber
		/// </summary>
		public int? InventoryRealNumber { get; set; }



		/// <summary>
		/// ShortOriginal
		/// </summary>
		public int? ShortOriginal { get; set; }



		/// <summary>
		/// Damaged
		/// </summary>
		public int? Damaged { get; set; }



		/// <summary>
		/// Remarks
		/// </summary>
		public string Remarks { get; set; }



        /// <summary>
        /// EmployeeName
        /// </summary>
        [Required(ErrorMessage = "EmployeeName不能为空")]
        public string EmployeeName { get; set; }



        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage = "EmployeeId不能为空")]
        public string EmployeeId { get; set; }



        /// <summary>
        /// TimeLogId
        /// </summary>
        [Required(ErrorMessage = "TimeLogId不能为空")]
        public Guid TimeLogId { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        //[Required(ErrorMessage="CreationTime不能为空")]
        public DateTime CreationTime { get; set; }




    }
}