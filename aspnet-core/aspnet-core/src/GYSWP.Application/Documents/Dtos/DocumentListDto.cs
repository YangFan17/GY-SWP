

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.Documents;
using Abp.AutoMapper;

namespace GYSWP.Documents.Dtos
{
    [AutoMapFrom(typeof(Document))]
    public class DocumentListDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Name不能为空")]
        public string Name { get; set; }


        /// <summary>
        /// DocNo
        /// </summary>
        [Required(ErrorMessage = "DocNo不能为空")]
        public string DocNo { get; set; }


        /// <summary>
        /// DeptIds
        /// </summary>
        public string DeptIds { get; set; }
        /// <summary>
        /// CategoryId
        /// </summary>
        [Required(ErrorMessage = "CategoryId不能为空")]
        public int CategoryId { get; set; }


        /// <summary>
        /// CategoryDesc
        /// </summary>
        public string CategoryDesc { get; set; }


        /// <summary>
        /// DocThum
        /// </summary>
        public string DocThum { get; set; }


        /// <summary>
        /// Summary
        /// </summary>
        public string Summary { get; set; }


        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? PublishTime { get; set; }


        /// <summary>
        /// QrCodeUrl
        /// </summary>
        public string QrCodeUrl { get; set; }
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
    }

    [AutoMapFrom(typeof(Document))]
    public class DocumentTitleDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string DocNo { get; set; }
        public string CategoryDesc { get; set; }
        public DateTime? PublishTime { get; set; }
        public string DeptName { get; set; }
    }
}