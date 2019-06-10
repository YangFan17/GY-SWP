

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.SelfChekRecords;

namespace GYSWP.SelfChekRecords.Dtos
{
    public class SelfChekRecordListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// ClauseId
		/// </summary>
		[Required(ErrorMessage="ClauseId不能为空")]
		public Guid ClauseId { get; set; }



		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }



		/// <summary>
		/// IsTodayFirst
		/// </summary>
		public bool? IsTodayFirst { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }

        /// <summary>
        /// 标准Id
        /// </summary>
        [Required]
        public Guid DocumentId { get; set; }
    }
}