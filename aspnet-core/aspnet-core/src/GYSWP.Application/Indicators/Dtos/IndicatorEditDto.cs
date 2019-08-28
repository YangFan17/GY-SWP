
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.GYEnums;
using GYSWP.Indicators;

namespace  GYSWP.Indicators.Dtos
{
    public class IndicatorEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// Title
		/// </summary>
		[Required(ErrorMessage="Title不能为空")]
		public string Title { get; set; }



		/// <summary>
		/// Paraphrase
		/// </summary>
		[Required(ErrorMessage="Paraphrase不能为空")]
		public string Paraphrase { get; set; }



		/// <summary>
		/// MeasuringWay
		/// </summary>
		[Required(ErrorMessage="MeasuringWay不能为空")]
		public string MeasuringWay { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// CreatorEmpeeId
		/// </summary>
		//[Required(ErrorMessage="CreatorEmpeeId不能为空")]
		public string CreatorEmpeeId { get; set; }



		/// <summary>
		/// CreatorEmpName
		/// </summary>
		public string CreatorEmpName { get; set; }



		/// <summary>
		/// CreatorDeptId
		/// </summary>
		//[Required(ErrorMessage="CreatorDeptId不能为空")]
		public long CreatorDeptId { get; set; }



		/// <summary>
		/// CreatorDeptName
		/// </summary>
		public string CreatorDeptName { get; set; }



		/// <summary>
		/// DeptId
		/// </summary>
		//[Required(ErrorMessage="DeptId不能为空")]
		public string DeptIds { get; set; }



		/// <summary>
		/// DeptName
		/// </summary>
		public string DeptNames { get; set; }

        /// <summary>
        /// 预期值
        /// </summary>
        [Required]
        public decimal ExpectedValue { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        [Required]
        public CycleTime CycleTime { get; set; }

        /// <summary>
        /// 达成条件
        /// </summary>
        [Required]
        public AchieveType AchieveType { get; set; }

        /// <summary>
        /// 来源标准Id
        /// </summary>
        [Required]
        public Guid SourceDocId { get; set; }
        /// <summary>
        /// 截止时间 当天23:59:59
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}