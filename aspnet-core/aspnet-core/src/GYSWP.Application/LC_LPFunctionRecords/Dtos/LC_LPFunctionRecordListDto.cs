

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_LPFunctionRecords;
using Abp.AutoMapper;

namespace GYSWP.LC_LPFunctionRecords.Dtos
{
    [AutoMapFrom(typeof(LC_LPFunctionRecord))]
    public class LC_LPFunctionRecordListDto : EntityDto<Guid>,IHasCreationTime 
    {

        /// <summary>
        /// ResponsibleName
        /// </summary>
        [Required(ErrorMessage= "ResponsibleName不能为空")]
		public string ResponsibleName { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }



        /// <summary>
        /// SupervisorName
        /// </summary>
        [Required(ErrorMessage= "SupervisorName不能为空")]
		public string SupervisorName { get; set; }



		/// <summary>
		/// UseTime
		/// </summary>
		public DateTime? UseTime { get; set; }



		/// <summary>
		/// DownTime
		/// </summary>
		public DateTime? DownTime { get; set; }



		/// <summary>
		/// IsSwitchOk
		/// </summary>
		public bool? IsSwitchOk { get; set; }



		/// <summary>
		/// IsLiftingOk
		/// </summary>
		public bool? IsLiftingOk { get; set; }



		/// <summary>
		/// IsGuardrailOk
		/// </summary>
		public bool? IsGuardrailOk { get; set; }



		/// <summary>
		/// IsOutriggerLegOk
		/// </summary>
		public bool? IsOutriggerLegOk { get; set; }



		/// <summary>
		/// IsGroundSmooth
		/// </summary>
		public bool? IsGroundSmooth { get; set; }



		/// <summary>
		/// IsOutriggerLegStable
		/// </summary>
		public bool? IsOutriggerLegStable { get; set; }



		/// <summary>
		/// IsLevelLeveling
		/// </summary>
		public bool? IsLevelLeveling { get; set; }



		/// <summary>
		/// IsLiftingMachineBad
		/// </summary>
		public bool? IsLiftingMachineBad { get; set; }



		/// <summary>
		/// IsLoadExceeding
		/// </summary>
		public bool? IsLoadExceeding { get; set; }



		/// <summary>
		/// IsGuardrailPositionOk
		/// </summary>
		public bool? IsGuardrailPositionOk { get; set; }



		/// <summary>
		/// IsOutriggerCloseUp
		/// </summary>
		public bool? IsOutriggerCloseUp { get; set; }



		/// <summary>
		/// IsDeviceClean
		/// </summary>
		public bool? IsDeviceClean { get; set; }



		/// <summary>
		/// Desc
		/// </summary>
		public string Desc { get; set; }



        /// <summary>
        /// EmployeeId
        /// </summary>
        [Required(ErrorMessage = "EmployeeId不能为空")]
        [StringLength(200)]
        public string EmployeeId { get; set; }



        /// <summary>
        ///  EmployeeName
        /// </summary>
        [Required(ErrorMessage = "EmployeeName不能为空")]
        [StringLength(50)]
        public string EmployeeName { get; set; }



        /// <summary>
        /// UseTimeFormat
        /// </summary>
        public string UseTimeFormat
        {
            get
            {
                if (UseTime.HasValue)
                { return UseTime.Value.ToString("yyyy-MM-dd HH:mm"); }
                else { return null; }
            }
        }



        /// <summary>
        /// DownTimeFormat
        /// </summary>
        public string DownTimeFormat
        {
            get
            {
                if (DownTime.HasValue)
                {
                    return DownTime.Value.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// 获取图片路径
        /// </summary>
        public string[] Path
        {
            get; set;
        }




    }
}