

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_CigaretExchanges;

namespace GYSWP.LC_CigaretExchanges.Dtos
{
    public class LC_CigaretExchangeListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// TimeLogId
		/// </summary>
		[Required(ErrorMessage="TimeLogId不能为空")]
		public Guid TimeLogId { get; set; }



		/// <summary>
		/// OriginPlace
		/// </summary>
		public string OriginPlace { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }



		/// <summary>
		/// Unit
		/// </summary>
		public string Unit { get; set; }



		/// <summary>
		/// Num
		/// </summary>
		public int? Num { get; set; }



		/// <summary>
		/// Reason
		/// </summary>
		public string Reason { get; set; }



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