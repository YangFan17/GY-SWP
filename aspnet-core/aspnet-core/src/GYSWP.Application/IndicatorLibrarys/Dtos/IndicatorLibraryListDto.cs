

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.IndicatorLibrarys;
using GYSWP.GYEnums;

namespace GYSWP.IndicatorLibrarys.Dtos
{
    public class IndicatorLibraryListDto : FullAuditedEntityDto<Guid> 
    {
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
		/// CycleTime
		/// </summary>
		[Required(ErrorMessage="CycleTime不能为空")]
		public CycleTime CycleTime { get; set; }



		/// <summary>
		/// SourceDocId
		/// </summary>
		[Required(ErrorMessage="SourceDocId不能为空")]
		public Guid SourceDocId { get; set; }



		/// <summary>
		/// DeptIds
		/// </summary>
		[Required(ErrorMessage="DeptIds不能为空")]
		public string DeptIds { get; set; }



		/// <summary>
		/// DeptNames
		/// </summary>
		public string DeptNames { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Department { get; set; }
        public string SourceDocName { get; set; }
        public string CycleTimeName { get { return CycleTime.ToString(); } }
    }
}