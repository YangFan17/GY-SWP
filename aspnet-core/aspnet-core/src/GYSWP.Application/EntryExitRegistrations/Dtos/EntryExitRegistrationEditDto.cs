
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
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



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
		/// CreationTime
		/// </summary>
		//[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }




    }
}