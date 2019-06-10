
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
		/// IsSelfCheck
		/// </summary>
		[Required(ErrorMessage="IsSelfCheck不能为空")]
		public bool IsSelfCheck { get; set; }



		/// <summary>
		/// SelfCheckTime
		/// </summary>
		public DateTime? SelfCheckTime { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }




    }
}