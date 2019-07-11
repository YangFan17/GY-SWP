
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.InspectionRecords;

namespace  GYSWP.InspectionRecords.Dtos
{
    public class InspectionRecordEditDto : EntityDto<long?>, IHasCreationTime
    {        

		/// <summary>
		/// IsDWLAbnormal
		/// </summary>
		public bool? IsDWLAbnormal { get; set; }



		/// <summary>
		/// IsWallDestruction
		/// </summary>
		public bool? IsWallDestruction { get; set; }



		/// <summary>
		/// IsRoofWallSeepage
		/// </summary>
		public bool? IsRoofWallSeepage { get; set; }



		/// <summary>
		/// IsHumitureExceeding
		/// </summary>
		public bool? IsHumitureExceeding { get; set; }



		/// <summary>
		/// IsFASNormal
		/// </summary>
		public bool? IsFASNormal { get; set; }



		/// <summary>
		/// IsBurglarAlarmNormal
		/// </summary>
		public bool? IsBurglarAlarmNormal { get; set; }



		/// <summary>
		/// IsSASSValid
		/// </summary>
		public bool? IsSASSValid { get; set; }



		/// <summary>
		/// IsCameraShelter
		/// </summary>
		public bool? IsCameraShelter { get; set; }



		/// <summary>
		/// IsFPDStop
		/// </summary>
		public bool? IsFPDStop { get; set; }



		/// <summary>
		/// IsEXITStop
		/// </summary>
		public bool? IsEXITStop { get; set; }



		/// <summary>
		/// SignatureOfPrincipal
		/// </summary>
		public string SignatureOfPrincipal { get; set; }



        /// <summary>
        /// EmployeeName
        /// </summary>
        [Required(ErrorMessage = "EmployeeName不能为空")]
        public string EmployeeName { get; set; }



        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage = "EmployeeId不能为空")]
        public string EmployeeId { get; set; }



        /// <summary>
        /// TimeLogId
        /// </summary>
        [Required(ErrorMessage = "TimeLogId不能为空")]
        public Guid TimeLogId { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        //[Required(ErrorMessage="CreationTime不能为空")]
        public DateTime CreationTime { get; set; }




    }
}