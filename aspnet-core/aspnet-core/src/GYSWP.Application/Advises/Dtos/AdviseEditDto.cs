
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.Advises;

namespace  GYSWP.Advises.Dtos
{
    public class AdviseEditDto:EntityDto<Guid?>, IHasCreationTime
    {
        /// <summary>
        /// AdviseName
        /// </summary>
        [StringLength(1000)]
        [Required(ErrorMessage = "AdviseName不能为空")]
        public string AdviseName { get; set; }

        /// <summary>
        /// CurrentSituation
        /// </summary>
        [StringLength(1000)]
        [Required(ErrorMessage = "CurrentSituation不能为空")]
        public string CurrentSituation { get; set; }

        /// <summary>
        /// Solution
        /// </summary>
        [StringLength(1000)]
        [Required(ErrorMessage = "Solution不能为空")]
        public string Solution { get; set; }

		/// <summary>
		/// IsAdoption
		/// </summary>
		public bool? IsAdoption { get; set; }

		/// <summary>
		/// EmployeeId
		/// </summary>
		public string EmployeeId { get; set; }

		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }

		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// DeptId
		/// </summary>
		public long DeptId { get; set; }

		/// <summary>
		/// DeptName
		/// </summary>
		public string DeptName { get; set; }

        /// <summary>
        /// 联合建议人快照
        /// </summary>
        public string UnionEmpName { get; set; }

        /// <summary>
        /// 是否公示
        /// </summary>
        public bool IsPublicity { get; set; }

        /// <summary>
        /// 是否需要主要领导审批
        /// </summary>
        [Required]
        public bool IsMainLeader { get; set; }
    }

    public class AdviseDDCreateDto
    {
        /// <summary>
        /// AdviseName
        /// </summary>
        [Required(ErrorMessage = "AdviseName不能为空")]
        public string AdviseName { get; set; }

        /// <summary>
        /// CurrentSituation
        /// </summary>
        [Required(ErrorMessage = "CurrentSituation不能为空")]
        public string CurrentSituation { get; set; }

        /// <summary>
        /// Solution
        /// </summary>
        [Required(ErrorMessage = "Solution不能为空")]
        public string Solution { get; set; }

        /// <summary>
        /// IsAdoption
        /// </summary>
        public bool? IsAdoption { get; set; }

        /// <summary>
        /// EmployeeId
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// EmployeeName
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage = "CreationTime不能为空")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// DeptId
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// DeptName
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 联合建议人快照
        /// </summary>
        public string UnionEmpName { get; set; }

        /// <summary>
        /// 是否需要主要领导审批
        /// </summary>
        [Required]
        public bool IsMainLeader { get; set; }
    }
}