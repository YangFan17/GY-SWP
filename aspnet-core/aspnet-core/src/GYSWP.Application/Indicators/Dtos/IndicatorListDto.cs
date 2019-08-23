

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.Indicators;
using GYSWP.GYEnums;
using Abp.AutoMapper;

namespace GYSWP.Indicators.Dtos
{
    [AutoMapFrom(typeof(Indicator))]
    public class IndicatorListDto : EntityDto<Guid>, IHasCreationTime
    {


        /// <summary>
        /// Title
        /// </summary>
        [Required(ErrorMessage = "Title不能为空")]
        public string Title { get; set; }



        /// <summary>
        /// Paraphrase
        /// </summary>
        [Required(ErrorMessage = "Paraphrase不能为空")]
        public string Paraphrase { get; set; }



        /// <summary>
        /// MeasuringWay
        /// </summary>
        [Required(ErrorMessage = "MeasuringWay不能为空")]
        public string MeasuringWay { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        [Required(ErrorMessage = "CreationTime不能为空")]
        public DateTime CreationTime { get; set; }



        /// <summary>
        /// CreatorEmpeeId
        /// </summary>
        [Required(ErrorMessage = "CreatorEmpeeId不能为空")]
        public string CreatorEmpeeId { get; set; }



        /// <summary>
        /// CreatorEmpName
        /// </summary>
        public string CreatorEmpName { get; set; }



        /// <summary>
        /// CreatorDeptId
        /// </summary>
        [Required(ErrorMessage = "CreatorDeptId不能为空")]
        public long CreatorDeptId { get; set; }



        /// <summary>
        /// CreatorDeptName
        /// </summary>
        public string CreatorDeptName { get; set; }



        /// <summary>
        /// DeptId
        /// </summary>
        [Required(ErrorMessage = "DeptId不能为空")]
        public string DeptIds { get; set; }



        /// <summary>
        /// DeptName
        /// </summary>
        public string DeptNames { get; set; }

        /// <summary>
        /// 预期值
        /// </summary>
        [Required]
        public decimal ExpectedValue { get; set; }
        /// <summary>
        /// 达成条件
        /// </summary>
        [Required]
        public AchieveType AchieveType { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        [Required]
        public CycleTime CycleTime { get; set; }
        public string CycleTimeName { get { return CycleTime.ToString(); } }
        public string AchieveTypeName { get { return AchieveType.ToString(); } }

        /// <summary>
        /// 来源标准Id
        /// </summary>
        public Guid SourceDocId { get; set; }
        /// <summary>
        /// 标准名称
        /// </summary>
        public string SourceDocName { get; set; }
    }

    public class IndicatorShowDto : EntityDto<Guid>
    {
        public Guid IndicatorDetailId { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Paraphrase
        /// </summary>
        public string Paraphrase { get; set; }
        /// <summary>
        /// MeasuringWay
        /// </summary>
        public string MeasuringWay { get; set; }
        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// CreatorEmpName
        /// </summary>
        public string CreatorEmpName { get; set; }
        /// <summary>
        /// CreatorDeptName
        /// </summary>
        public string CreatorDeptName { get; set; }
        /// <summary>
        /// DeptName
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 预期值
        /// </summary>
        public decimal ExpectedValue { get; set; }
        public decimal? ActualValue { get; set; }

        public IndicatorStatus Status { get; set; }
        public string CycleTimeName { get; set; }
        public string StatusName { get { return Status.ToString(); } }
        public AchieveType AchieveType { get; set; }
        public string AchieveTypeName { get { return AchieveType.ToString(); } }
        public string SourceDocName { get; set; }
    }
    public class IndicatorReviewDto : EntityDto<Guid>
    {
        public Guid IndicatorDetailId { get; set; }

        /// <summary>
        /// DeptName
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 预期值
        /// </summary>
        public decimal ExpectedValue { get; set; }
        public decimal? ActualValue { get; set; }

        public IndicatorStatus Status { get; set; }
        public string StatusName { get { return Status.ToString(); } }
        public DateTime? CompleteTime { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDeptName { get; set; }
        public AchieveType AchieveType { get; set; }
        public string AchieveTypeName { get { return AchieveType.ToString(); } }
    }
}