

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_WarningReports;

namespace GYSWP.LC_WarningReports.Dtos
{
    public class LC_WarningReportListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// TimeLogId
		/// </summary>
		public Guid? TimeLogId { get; set; }



		/// <summary>
		/// Factory
		/// </summary>
		[Required(ErrorMessage="Factory不能为空")]
		public string Factory { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// Quantity
		/// </summary>
		[Required(ErrorMessage="Quantity不能为空")]
		public int Quantity { get; set; }



		/// <summary>
		/// InStorageTime
		/// </summary>
		[Required(ErrorMessage="InStorageTime不能为空")]
		public DateTime InStorageTime { get; set; }



		/// <summary>
		/// OnStorageTime
		/// </summary>
		[Required(ErrorMessage="OnStorageTime不能为空")]
		public DateTime OnStorageTime { get; set; }



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