

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.ClauseRevisions;
using GYSWP.GYEnums;
using System.Collections.Generic;
using Abp.AutoMapper;

namespace GYSWP.ClauseRevisions.Dtos
{
    public class ClauseRevisionListDto : FullAuditedEntity<Guid>
    {
		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }


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

        public string EmployeeId { get; set; }

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

    public class ClauseRecordResult
    {
        public ClauseRecordResult()
        {
            List = new List<ClauseRecordListDto>();
            Count = new ClauseCountDto();
        }
        public List<ClauseRecordListDto> List { get; set; }
        public ClauseCountDto Count { get; set; }

    }
    public class ClauseCountDto {
        public int Total { get; set; }
        public int Cnumber { get; set; }
        public int Unumber { get; set; }
        public int Dnumber { get; set; }
    }

    public class ClauseRecordListDto : EntityDto<Guid>
    {
        /// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }


        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [StringLength(100)]
        [Required]
        public string EmployeeId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [Required(ErrorMessage = "Status不能为空")]
        public RevisionStatus Status { get; set; }

        public string StatusName
        {
            get
            {
                return Status.ToString();
            }
        }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// ClauseNo
        /// </summary>
        [Required(ErrorMessage = "ClauseNo不能为空")]
        public string ClauseNo { get; set; }


        /// <summary>
        /// 类型（新增、修改、删除）
        /// </summary>
        public RevisionType RevisionType { get; set; }

        public string RevisionTypeName
        {
            get
            {
                return RevisionType.ToString();
            }
        }
    }

    /// <summary>
    /// 条款树形表格
    /// </summary>
    [AutoMapFrom(typeof(ClauseRevision))]
    public class ClauseRevisionTreeNodeDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        public string ClauseNo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<ClauseRevisionTreeNodeDto> Children = new List<ClauseRevisionTreeNodeDto>();
    }
    [AutoMapFrom(typeof(ClauseRevision))]
    public class ClauseRevisionTreeListDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        public Guid? ParentId { get; set; }


        public string Title { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 父Id（root 为 空）
        /// </summary>
        public string ClauseNo { get; set; }
    }
}