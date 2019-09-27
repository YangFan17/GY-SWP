

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.Clauses;
using System.Collections.Generic;
using Abp.AutoMapper;
using GYSWP.GYEnums;

namespace GYSWP.Clauses.Dtos
{
    [AutoMapFrom(typeof(Clause))]
    public class ClauseListDto : FullAuditedEntityDto<Guid>
    {
        //public Guid Id { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        public Guid? ParentId { get; set; }


        public string Title { get; set; }
        [StringLength(3000)]
        public string Content { get; set; }

        public string ContentFormat
        {
            get
            {
                if (string.IsNullOrEmpty(Content))
                {
                    return string.Empty;
                }

                if (Content.Length <= 50)
                {
                    return Content;
                }
                else
                {
                    return Content.Substring(0, 50) + "....";
                }
            }
        }

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

        public bool IsConfirm { get; set; }

        public bool IsNew
        {
            get
            {
                if (LastModificationTime.HasValue && (DateTime.Today - LastModificationTime.Value.Date).Days < 90)
                {
                    return true;
                }
                return false;
            }
        }
    }

    /// <summary>
    /// 条款树形表格
    /// </summary>
    [AutoMapFrom(typeof(Clause))]
    public class ClauseTreeNodeDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        public string ClauseNo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Checked { get; set; }
        public Guid? BLLId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public RevisionType? RevisionType { get; set; }
        public long? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        //public int Level { get; set; } = 0;
        public List<ClauseTreeNodeDto> Children = new List<ClauseTreeNodeDto>();
    }
    [AutoMapFrom(typeof(Clause))]
    public class ClauseTreeListDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        public Guid? ParentId { get; set; }


        public string Title { get; set; }
        public string Content { get; set; }
        public bool Checked { get; set; }
        /// <summary>
        /// 父Id（root 为 空）
        /// </summary>
        public string ClauseNo { get; set; }
        public Guid? BLLId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}