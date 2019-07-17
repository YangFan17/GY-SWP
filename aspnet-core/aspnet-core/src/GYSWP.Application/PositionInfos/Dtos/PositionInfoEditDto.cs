
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.PositionInfos;

namespace  GYSWP.PositionInfos.Dtos
{
    public class PositionInfoEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// Position
		/// </summary>
		[Required(ErrorMessage="Position不能为空")]
		public string Position { get; set; }



		/// <summary>
		/// Duties
		/// </summary>
		public string Duties { get; set; }



		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }



		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }




    }
}