
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.DocAttachments;

namespace  GYSWP.DocAttachments.Dtos
{
    public class DocAttachmentEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// Type
		/// </summary>
		[Required(ErrorMessage="Type不能为空")]
		public int Type { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// FileType
		/// </summary>
		public int? FileType { get; set; }



		/// <summary>
		/// FileSize
		/// </summary>
		public decimal? FileSize { get; set; }



		/// <summary>
		/// Path
		/// </summary>
		[Required(ErrorMessage="Path不能为空")]
		public string Path { get; set; }



		/// <summary>
		/// BLL
		/// </summary>
		[Required(ErrorMessage="BLL不能为空")]
		public Guid BLL { get; set; }



		/// <summary>
		/// BackUpId
		/// </summary>
		public Guid? BackUpId { get; set; }




    }
}