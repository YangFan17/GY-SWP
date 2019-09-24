
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using GYSWP.Documents;
using GYSWP.GYEnums;

namespace  GYSWP.Documents.Dtos
{
    [AutoMapTo(typeof(Document))]

    public class DocumentEditDto : FullAuditedEntityDto<Guid?>
    {

        /// <summary>
        /// Id
        /// </summary>
        //public Guid? Id { get; set; }         


        
		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }

        public string DeptDesc { get; set; }
        /// <summary>
        /// DeptIds
        /// </summary>
        public string DeptIds { get; set; }
        /// <summary>
        /// DocNo
        /// </summary>
        [Required(ErrorMessage="DocNo不能为空")]
		public string DocNo { get; set; }



		/// <summary>
		/// CategoryId
		/// </summary>
		[Required(ErrorMessage="CategoryId不能为空")]
		public int CategoryId { get; set; }



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
        [Required]
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

        /// <summary>
        /// 适用于（QMS,EMS,OHS）
        /// </summary>
        public string SuitableCode { get; set; }

        /// <summary>
        /// 业务操作Id(制订记录Id)
        /// </summary>
        public Guid? BLLId { get; set; }
    }
}