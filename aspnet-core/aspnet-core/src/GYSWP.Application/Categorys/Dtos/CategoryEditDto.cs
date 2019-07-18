
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using GYSWP.Categorys;

namespace  GYSWP.Categorys.Dtos
{
    [AutoMapTo(typeof(Category))]

    public class CategoryEditDto : FullAuditedEntityDto<int?>
    {

        /// <summary>
        /// Id
        /// </summary>
        //public int? Id { get; set; }         


        
		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// ParentId
		/// </summary>
		public int? ParentId { get; set; }



		/// <summary>
		/// Desc
		/// </summary>
		public string Desc { get; set; }

        /// <summary>
        /// 维护部门Id
        /// </summary>
        [Required(ErrorMessage = "DeptId不能为空")]
        public long DeptId { get; set; }
    }
}