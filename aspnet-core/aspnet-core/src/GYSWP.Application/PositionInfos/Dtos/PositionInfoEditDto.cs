
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.PositionInfos;

namespace GYSWP.PositionInfos.Dtos
{
    public class PositionInfoEditDto: FullAuditedEntityDto<Guid?>
    {
        /// <summary>
        /// Position
        /// </summary>
        //[Required(ErrorMessage = "Position不能为空")]
        public string Position { get; set; }



        /// <summary>
        /// Duties
        /// </summary>
        public string Duties { get; set; }



        /// <summary>
        /// EmployeeId
        /// </summary>
        //[Required(ErrorMessage = "EmployeeId不能为空")]
        public string EmployeeId { get; set; }



        /// <summary>
        /// EmployeeName
        /// </summary>
        public string EmployeeName { get; set; }
    }
    public class PosInfoInput
    {
        //summary
        //工作职责
        // summary
        [StringLength(500)]
        public virtual string Duties { get; set; }
    }
}