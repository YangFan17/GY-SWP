
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.ClauseRevisions;
using GYSWP.GYEnums;

namespace  GYSWP.ClauseRevisions.Dtos
{
    public class ClauseRevisionEditDto : FullAuditedEntity<Guid?>
    {
        
		/// <summary>
		/// ClauseId
		/// </summary>
		public Guid? ClauseId { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }
        /// <summary>
        /// 员工Id
        /// </summary>
        [StringLength(100)]
        public string EmployeeId { get; set; }



        /// <summary>
        /// ApplyInfoId
        /// </summary>
        [Required(ErrorMessage="ApplyInfoId不能为空")]
		public Guid ApplyInfoId { get; set; }



		/// <summary>
		/// Status
		/// </summary>
		[Required(ErrorMessage="Status不能为空")]
		public RevisionStatus Status { get; set; }



		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }


        /// <summary>
        /// ParentId
        /// </summary>
        public Guid? ParentId { get; set; }



		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }



		/// <summary>
		/// ClauseNo
		/// </summary>
		public string ClauseNo { get; set; }


		/// <summary>
		/// DocumentId
		/// </summary>
		public Guid? DocumentId { get; set; }


        /// <summary>
        /// 类型（新增、修改、删除）
        /// </summary>
        public RevisionType RevisionType { get; set; }
    }
}