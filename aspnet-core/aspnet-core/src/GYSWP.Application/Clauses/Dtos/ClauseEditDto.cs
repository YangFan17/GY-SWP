
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.Clauses;

namespace  GYSWP.Clauses.Dtos
{
    public class ClauseEditDto : FullAuditedEntityDto<Guid?>
    {

        /// <summary>
        /// Id
        /// </summary>
        //public Guid? Id { get; set; }         


        
		/// <summary>
		/// ParentId
		/// </summary>
		public Guid? ParentId { get; set; }

        public string Title { get; set; }
        [StringLength(2000)]
        public string Content { get; set; }
        /// <summary>
        /// 父Id（root 为 空）
        /// </summary>
        [Required]
        public string ClauseNo { get; set; }
        /// <summary>
        /// 类型（文本、表格）是否含有附件
        /// </summary>
        [Required]
        public bool HasAttchment { get; set; }


        /// <summary>
        /// DocumentId
        /// </summary>
        public Guid? DocumentId { get; set; }
    }
}