

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.IndicatorsDetails;
using GYSWP.GYEnums;

namespace GYSWP.IndicatorsDetails.Dtos
{
    public class IndicatorsDetailListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// IndicatorsId
		/// </summary>
		[Required(ErrorMessage="IndicatorsId不能为空")]
		public Guid IndicatorsId { get; set; }



		/// <summary>
		/// ClauseId
		/// </summary>
		[Required(ErrorMessage="ClauseId不能为空")]
		public decimal ActualValue { get; set; }



		/// <summary>
		/// Status
		/// </summary>
		[Required(ErrorMessage="Status不能为空")]
		public IndicatorStatus Status { get; set; }



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

        public string StatusName
        {
            get
            {
                return Status.ToString();
            }
        }

        /// <summary>
        /// 被考核部门
        /// </summary>
        [Required]
        public long DeptId { get; set; }

        /// <summary>
        /// 被考核部门快照
        /// </summary>
        [StringLength(100)]
        public string DeptName { get; set; }
    }
}