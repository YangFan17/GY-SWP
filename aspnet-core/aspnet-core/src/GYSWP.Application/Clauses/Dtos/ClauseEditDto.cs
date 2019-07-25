
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
        /// DocumentId
        /// </summary>
        public Guid? DocumentId { get; set; }
        /// <summary>
        /// 业务操作Id(修订记录Id)
        /// </summary>
        public Guid? BLLId { get; set; }
    }
}