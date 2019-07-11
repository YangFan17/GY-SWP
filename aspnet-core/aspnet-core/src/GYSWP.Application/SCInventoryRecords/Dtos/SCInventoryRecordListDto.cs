

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.SCInventoryRecords;

namespace GYSWP.SCInventoryRecords.Dtos
{
    public class SCInventoryRecordListDto : EntityDto<long>,IHasCreationTime 
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
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }




    }
}