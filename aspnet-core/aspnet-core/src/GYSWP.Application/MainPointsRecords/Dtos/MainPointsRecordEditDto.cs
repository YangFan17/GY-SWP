
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.PositionInfos;

namespace  GYSWP.PositionInfos.Dtos
{
    public class MainPointsRecordEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
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