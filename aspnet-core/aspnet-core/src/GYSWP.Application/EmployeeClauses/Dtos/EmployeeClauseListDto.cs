

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.EmployeeClauses;

namespace GYSWP.EmployeeClauses.Dtos
{
    public class EmployeeClauseListDto : EntityDto<Guid>, IHasCreationTime
    {


        /// <summary>
        /// ClauseId
        /// </summary>
        [Required(ErrorMessage = "ClauseId不能为空")]
        public Guid ClauseId { get; set; }

        /// <summary>
        /// 标准Id
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// 姓名快照
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage = "EmployeeId不能为空")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}