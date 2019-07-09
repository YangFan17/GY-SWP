
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using GYSWP.IndicatorsDetails;

namespace  GYSWP.IndicatorsDetails.Dtos
{
    public class IndicatorsDetailEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// IndicatorsId
		/// </summary>
		[Required(ErrorMessage="IndicatorsId不能为空")]
		public Guid IndicatorsId { get; set; }



		/// <summary>
		/// ClauseId
		/// </summary>
		[Required(ErrorMessage="ClauseId不能为空")]
		public decimal? ActualValue { get; set; }



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
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }
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

    public class IndicatorsDetailUpDateDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ClauseId
        /// </summary>
        [Required(ErrorMessage = "ClauseId不能为空")]
        public decimal? ActualValue { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [Required(ErrorMessage = "Status不能为空")]
        public IndicatorStatus Status { get; set; }
    }
}