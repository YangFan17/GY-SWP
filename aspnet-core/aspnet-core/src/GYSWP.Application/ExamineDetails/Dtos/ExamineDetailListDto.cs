

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.ExamineDetails;
using GYSWP.GYEnums;

namespace GYSWP.ExamineDetails.Dtos
{
    public class ExamineDetailListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// CriterionExamineId
		/// </summary>
		[Required(ErrorMessage="CriterionExamineId不能为空")]
		public Guid CriterionExamineId { get; set; }


        /// <summary>
        /// 所属标准Id
        /// </summary>
        [Required]
        public Guid DocumentId { get; set; }
        /// <summary>
        /// ClauseId
        /// </summary>
        [Required(ErrorMessage="ClauseId不能为空")]
		public Guid ClauseId { get; set; }



		/// <summary>
		/// Status
		/// </summary>
		[Required(ErrorMessage="Status不能为空")]
		public ResultStatus Status { get; set; }
        /// <summary>
        /// Result
        /// </summary>
        [Required(ErrorMessage="Result不能为空")]
		public ExamineStatus Result { get; set; }

		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
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
		/// CreatorEmpeeId
		/// </summary>
		[Required(ErrorMessage="CreatorEmpeeId不能为空")]
		public string CreatorEmpeeId { get; set; }



		/// <summary>
		/// CreatorEmpName
		/// </summary>
		public string CreatorEmpName { get; set; }
    }
    public class ExamineRecordDto : EntityDto<Guid>
    {
        /// <summary>
        /// 所属标准
        /// </summary>
        [Required]
        public string DocumentName { get; set; }
        /// <summary>
        /// 条款内容
        /// </summary>
        [Required(ErrorMessage = "ClauseId不能为空")]
        public string ClauseInfo { get; set; }

        /// <summary>
        /// 考核对象
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [Required(ErrorMessage = "Status不能为空")]
        public ResultStatus Status { get; set; }
        public ExamineStatus Result { get; set; }
    }

    public class ExamineListDto : EntityDto<Guid>
    {
        /// <summary>
        /// 所属标准
        /// </summary>
        [Required]
        public string DocumentName { get; set; }

        /// <summary>
        /// 考核对象
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [Required(ErrorMessage = "Status不能为空")]
        public ResultStatus Status { get; set; }
        public ExamineStatus Result { get; set; }
        /// Title
		/// </summary>
		[Required(ErrorMessage = "Title不能为空")]
        public string Title { get; set; }
        public string DeptName { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [Required(ErrorMessage = "Type不能为空")]
        public CriterionExamineType Type { get; set; }
        public string TypeName
        {
            get
            {
                return Type.ToString();
            }
        }
        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage = "CreationTime不能为空")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// CreatorDeptName
        /// </summary>
        public string CreatorDeptName { get; set; }
    }
}