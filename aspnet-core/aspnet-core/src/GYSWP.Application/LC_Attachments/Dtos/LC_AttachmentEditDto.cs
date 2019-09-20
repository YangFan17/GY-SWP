
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.DocAttachments;
using GYSWP.GYEnums;

namespace  GYSWP.DocAttachments.Dtos
{
    public class LC_AttachmentEditDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         
		/// <summary>
		/// Type
		/// </summary>
		[Required(ErrorMessage="Type不能为空")]
		public LC_AttachmentType Type { get; set; }
		/// <summary>
		/// Name
		/// </summary>
		/// <summary>
		/// Path
		/// </summary>
		[Required(ErrorMessage="Path不能为空")]
		public string Path { get; set; }
		/// <summary>
		/// BLL
		/// </summary>
		public Guid? BLL { get; set; }
		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }
		/// <summary>
		/// Remark
		/// </summary>
		public string Remark { get; set; }
		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// 钉钉提交照片数据
    /// </summary>
    public class DingDingAttachmentEditDto
    {
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Type不能为空")]
        public LC_AttachmentType Type { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        [Required(ErrorMessage = "Path不能为空")]
        public string[] Path { get; set; }
        public string Remark { get; set; }
    }
}