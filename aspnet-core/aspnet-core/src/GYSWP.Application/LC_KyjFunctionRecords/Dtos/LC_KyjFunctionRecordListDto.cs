

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_KyjFunctionRecords;
using Abp.AutoMapper;

namespace GYSWP.LC_KyjFunctionRecords.Dtos
{
    [AutoMapFrom(typeof(LC_KyjFunctionRecord))]
    public class LC_KyjFunctionRecordListDto : EntityDto<Guid>, IHasCreationTime
    {

        /// <summary>
        /// ResponsibleName
        /// </summary>
        [Required(ErrorMessage = "ResponsibleName不能为空")]
        public string ResponsibleName { get; set; }



        /// <summary>
        /// SupervisorId
        /// </summary>
        [Required(ErrorMessage = "SupervisorId不能为空")]
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
        /// IsDeviceComplete
        /// </summary>
        public bool? IsDeviceComplete { get; set; }



        /// <summary>
        /// IsPipelineOk
        /// </summary>
        public bool? IsPipelineOk { get; set; }



        /// <summary>
        /// IsLubricatingOilOk
        /// </summary>
        public bool? IsLubricatingOilOk { get; set; }



        /// <summary>
        /// IsVentilatingFanOpen
        /// </summary>
        public bool? IsVentilatingFanOpen { get; set; }



        /// <summary>
        /// IsSafetyValveOk
        /// </summary>
        public bool? IsSafetyValveOk { get; set; }



        /// <summary>
        /// IsPressureNormal
        /// </summary>
        public bool? IsPressureNormal { get; set; }



        /// <summary>
        /// IsPCShowNormal
        /// </summary>
        public bool? IsPCShowNormal { get; set; }



        /// <summary>
        /// IsRunningSoundBad
        /// </summary>
        public bool? IsRunningSoundBad { get; set; }



        /// <summary>
        /// IsLsLqLyOk
        /// </summary>
        public bool? IsLsLqLyOk { get; set; }



        /// <summary>
        /// IsDrainValveOk
        /// </summary>
        public bool? IsDrainValveOk { get; set; }



        /// <summary>
        /// IsPowerSupplyClose
        /// </summary>
        public bool? IsPowerSupplyClose { get; set; }



        /// <summary>
        /// IsDeviceClean
        /// </summary>
        public bool? IsDeviceClean { get; set; }



        /// <summary>
        /// Desc
        /// </summary>
        public string Desc { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage = "CreationTime不能为空")]
        public DateTime CreationTime { get; set; }



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
            get {
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
            get {
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
            get;set;
        }

    }
}