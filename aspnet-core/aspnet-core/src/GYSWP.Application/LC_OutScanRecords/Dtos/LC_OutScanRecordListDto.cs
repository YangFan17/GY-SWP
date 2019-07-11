

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_OutScanRecords;

namespace GYSWP.LC_OutScanRecords.Dtos
{
    public class LC_OutScanRecordListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// TimeLogId
		/// </summary>
		[Required(ErrorMessage="TimeLogId不能为空")]
		public Guid TimeLogId { get; set; }



		/// <summary>
		/// OrderNum
		/// </summary>
		public int? OrderNum { get; set; }



		/// <summary>
		/// ExpectedScanNum
		/// </summary>
		public int? ExpectedScanNum { get; set; }



		/// <summary>
		/// AcutalScanNum
		/// </summary>
		public int? AcutalScanNum { get; set; }



		/// <summary>
		/// AloneNotScanNum
		/// </summary>
		public int? AloneNotScanNum { get; set; }



		/// <summary>
		/// Remark
		/// </summary>
		public string Remark { get; set; }



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