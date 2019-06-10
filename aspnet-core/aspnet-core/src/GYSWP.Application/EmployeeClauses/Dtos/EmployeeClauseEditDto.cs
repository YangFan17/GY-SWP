
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.EmployeeClauses;

namespace  GYSWP.EmployeeClauses.Dtos
{
    public class EmployeeClauseEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// ClauseId
		/// </summary>
		[Required(ErrorMessage="ClauseId不能为空")]
		public Guid ClauseId { get; set; }



		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }



        /// <summary>
        /// 标准Id
        /// </summary>
        [Required(ErrorMessage = "DocumentId不能为空")]
        public Guid DocumentId { get; set; }

        /// <summary>
        /// 姓名快照
        /// </summary>
        public string EmployeeName { get; set; }


        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}