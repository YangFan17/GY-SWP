

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.PositionInfos;

namespace GYSWP.PositionInfos.Dtos
{
    public class MainPointsRecordListDto : EntityDto<Guid>,IHasCreationTime 
    {

        
		/// <summary>
		/// PositionInfoId
		/// </summary>
		[Required(ErrorMessage="PositionInfoId不能为空")]
		public Guid PositionInfoId { get; set; }



		/// <summary>
		/// DocumentId
		/// </summary>
		[Required(ErrorMessage="DocumentId不能为空")]
		public Guid DocumentId { get; set; }



		/// <summary>
		/// MainPoint
		/// </summary>
		public string MainPoint { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }




    }
}