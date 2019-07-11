

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_InStorageBills;

namespace GYSWP.LC_InStorageBills.Dtos
{
    public class LC_InStorageBillListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// TimeLogId
		/// </summary>
		[Required(ErrorMessage="TimeLogId不能为空")]
		public Guid TimeLogId { get; set; }



		/// <summary>
		/// IsYczmBill
		/// </summary>
		[Required(ErrorMessage="IsYczmBill不能为空")]
		public bool IsYczmBill { get; set; }



		/// <summary>
		/// IsJyhtBill
		/// </summary>
		[Required(ErrorMessage="IsJyhtBill不能为空")]
		public bool IsJyhtBill { get; set; }



		/// <summary>
		/// IsZzsBill
		/// </summary>
		[Required(ErrorMessage="IsZzsBill不能为空")]
		public bool IsZzsBill { get; set; }



		/// <summary>
		/// IsCarSeal
		/// </summary>
		[Required(ErrorMessage="IsCarSeal不能为空")]
		public bool IsCarSeal { get; set; }



		/// <summary>
		/// IsCarElcLock
		/// </summary>
		[Required(ErrorMessage="IsCarElcLock不能为空")]
		public bool IsCarElcLock { get; set; }



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