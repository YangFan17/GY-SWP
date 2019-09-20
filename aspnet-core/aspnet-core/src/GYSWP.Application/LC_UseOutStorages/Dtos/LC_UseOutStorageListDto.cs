

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_UseOutStorages;

namespace GYSWP.LC_UseOutStorages.Dtos
{
    public class LC_UseOutStorageListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// TimeLogId
		/// </summary>
		public Guid? TimeLogId { get; set; }



		/// <summary>
		/// SortLineName
		/// </summary>
		public string SortLineName { get; set; }



		/// <summary>
		/// ProductName
		/// </summary>
		public string ProductName { get; set; }



		/// <summary>
		/// PreDiffNum
		/// </summary>
		public int? PreDiffNum { get; set; }



		/// <summary>
		/// PreAloneNum
		/// </summary>
		public int? PreAloneNum { get; set; }



		/// <summary>
		/// SupWholeNum
		/// </summary>
		public int? SupWholeNum { get; set; }



		/// <summary>
		/// SupAllPieceNum
		/// </summary>
		public int? SupAllPieceNum { get; set; }



		/// <summary>
		/// SupAllItemNum
		/// </summary>
		public int? SupAllItemNum { get; set; }



		/// <summary>
		/// AcutalOrderNum
		/// </summary>
		public int? AcutalOrderNum { get; set; }



		/// <summary>
		/// CheckNum
		/// </summary>
		public int? CheckNum { get; set; }



		/// <summary>
		/// CheckAloneNum
		/// </summary>
		public int? CheckAloneNum { get; set; }



		/// <summary>
		/// ClerkName
		/// </summary>
		public string ClerkName { get; set; }



		/// <summary>
		/// SortorName
		/// </summary>
		public string SortorName { get; set; }



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