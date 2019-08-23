

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.Documents;
using Abp.AutoMapper;
using GYSWP.Clauses.Dtos;
using System.Collections.Generic;
using GYSWP.GYEnums;

namespace GYSWP.Documents.Dtos
{
    [AutoMapFrom(typeof(Document))]
    public class DocumentListDto : FullAuditedEntityDto<Guid>
    {

        public string Name { get; set; }


        /// <summary>
        /// DocNo
        /// </summary>
        //[Required(ErrorMessage = "DocNo不能为空")]
        public string DocNo { get; set; }


        /// <summary>
        /// DeptIds
        /// </summary>
        public string DeptIds { get; set; }
        /// <summary>
        /// DeptDesc
        /// </summary>
        public string DeptDesc { get; set; }
        public int CategoryId { get; set; }

        /// <summary>
        /// 分类Id层级 用逗号分隔
        /// </summary>
        public string CategoryCode { get; set; }
        /// <summary>
        /// CategoryDesc
        /// </summary>
        public string CategoryDesc { get; set; }


        /// <summary>
        /// Summary
        /// </summary>
        public string Summary { get; set; }


        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 授权员工名称（以逗号分隔）
        /// </summary>
        public string EmployeeDes { get; set; }

        /// <summary>
        /// 是否是全部用户
        /// </summary>
        public bool IsAllUser { get; set; }
        /// <summary>
        /// 员工授权Ids（以逗号分隔）
        /// </summary>
        public string EmployeeIds { get; set; }

        /// <summary>
        /// 是否启用（作废）
        /// </summary>
        public bool IsAction { get; set; }
        /// <summary>
        /// 作废时间
        /// </summary>
        public DateTime? InvalidTime { get; set; }
        /// <summary>
        /// 电子章数组 （以逗号分隔）
        /// </summary>
        [StringLength(50)]
        public string Stamps { get; set; }

        public string SubTitle
        {
            get
            {
                return string.Format("{0} 发布于：{1}", DocNo, TimeFormat);
            }
        }

        public string TimeFormat
        {
            get
            {
                return PublishTime.HasValue ? PublishTime.Value.ToString("yyyy.MM.dd") : "";
            }
        }

        public string DeptName { get; set; }

        public List<ClauseListDto> ClauseList { get; set; }
        /// <summary>
        /// 适用于（QMS,EMS,OHS）
        /// </summary>
        public string SuitableCode { get; set; }
    }

    [AutoMapFrom(typeof(Document))]
    public class DocumentTitleDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string DocNo { get; set; }
        public string CategoryDesc { get; set; }
        public DateTime? PublishTime { get; set; }
        public string DeptName { get; set; }
        /// <summary>
        /// 电子章数组 （以逗号分隔）
        /// </summary>
        [StringLength(50)]
        public string Stamps { get; set; }
    }

    [AutoMapFrom(typeof(Document))]
    public class DocumentConfirmDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string DocNo { get; set; }
        public string CategoryDesc { get; set; }
        public DateTime? PublishTime { get; set; }
        public string DeptName { get; set; }
        public int ShouldNum { get; set; }
        public int ActualNum { get; set; }
        public string DeptIds { get; set; }
        public bool IsAllUser { get; set; }
        public string EmployeeIds { get; set; }
        public int DifferNum
        {
            get
            {
               return ShouldNum - ActualNum;
            }
        }
    }
}