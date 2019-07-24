
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using GYSWP.CriterionExamines;
using GYSWP.GYEnums;

namespace GYSWP.CriterionExamines.Dtos
{
    public class CriterionExamineEditDto : Entity<Guid?>, IHasCreationTime
    {
        /// <summary>
        /// Title
        /// </summary>
        [Required(ErrorMessage = "Title不能为空")]
        public string Title { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [Required(ErrorMessage = "Type不能为空")]
        public CriterionExamineType Type { get; set; }



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
        /// DeptId
        /// </summary>
        [Required(ErrorMessage = "DeptId不能为空")]
        public long DeptId { get; set; }



        /// <summary>
        /// CreatorDeptName
        /// </summary>
        public string CreatorDeptName { get; set; }



        /// <summary>
        /// DeptName
        /// </summary>
        public string DeptName { get; set; }
    }

    public class CriterionExamineInfoDto
    {
        /// <summary>
        /// Type
        /// </summary>
        [Required(ErrorMessage = "Type不能为空")]
        public CriterionExamineType Type { get; set; }

        /// <summary>
        /// DeptId
        /// </summary>
        [Required(ErrorMessage = "DeptId不能为空")]
        public long DeptId { get; set; }

        /// <summary>
        /// DeptName
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 被考核人列表
        /// </summary>
        public List<EmpInfo> EmpInfo { get; set; }
    }
    public class EmpInfo
    {
        public string EmpId { get; set; }
        public string EmpName { get; set; }
    }
}