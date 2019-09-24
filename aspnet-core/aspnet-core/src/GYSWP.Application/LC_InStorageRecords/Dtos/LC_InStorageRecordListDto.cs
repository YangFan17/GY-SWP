

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_InStorageRecords;

namespace GYSWP.LC_InStorageRecords.Dtos
{
    public class LC_InStorageRecordListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// TimeLogId
		/// </summary>
		public Guid? TimeLogId { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// CarNo
		/// </summary>
		[Required(ErrorMessage="CarNo不能为空")]
		public string CarNo { get; set; }



		/// <summary>
		/// DeliveryUnit
		/// </summary>
		[Required(ErrorMessage="DeliveryUnit不能为空")]
		public string DeliveryUnit { get; set; }



		/// <summary>
		/// BillNo
		/// </summary>
		[Required(ErrorMessage="BillNo不能为空")]
		public string BillNo { get; set; }



		/// <summary>
		/// ReceivableAmount
		/// </summary>
		[Required(ErrorMessage="ReceivableAmount不能为空")]
		public int ReceivableAmount { get; set; }



		/// <summary>
		/// ActualAmount
		/// </summary>
		[Required(ErrorMessage="ActualAmount不能为空")]
		public int ActualAmount { get; set; }



		/// <summary>
		/// DiffContent
		/// </summary>
		[Required(ErrorMessage="DiffContent不能为空")]
		public string DiffContent { get; set; }



		/// <summary>
		/// Quality
		/// </summary>
		[Required(ErrorMessage="Quality不能为空")]
		public string Quality { get; set; }



		/// <summary>
		/// ReceiverName
		/// </summary>
		[Required(ErrorMessage="ReceiverName不能为空")]
		public string ReceiverName { get; set; }



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