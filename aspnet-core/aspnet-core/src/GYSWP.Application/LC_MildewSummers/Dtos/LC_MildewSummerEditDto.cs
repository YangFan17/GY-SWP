
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_MildewSummers;

namespace  GYSWP.LC_MildewSummers.Dtos
{
    public class LC_MildewSummerEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// TimeLogId
		/// </summary>
		[Required(ErrorMessage="TimeLogId不能为空")]
		public Guid TimeLogId { get; set; }



		/// <summary>
		/// AMBootTime
		/// </summary>
		[Required(ErrorMessage="AMBootTime不能为空")]
		public DateTime AMBootTime { get; set; }



		/// <summary>
		/// AMBootBeforeTmp
		/// </summary>
		public decimal? AMBootBeforeTmp { get; set; }



		/// <summary>
		/// AMBootBeforeHum
		/// </summary>
		public decimal? AMBootBeforeHum { get; set; }



		/// <summary>
		/// AMObservedTime
		/// </summary>
		public DateTime? AMObservedTime { get; set; }



		/// <summary>
		/// AMBootingTmp
		/// </summary>
		public decimal? AMBootingTmp { get; set; }



		/// <summary>
		/// AMBootingHum
		/// </summary>
		public decimal? AMBootingHum { get; set; }



		/// <summary>
		/// AMBootAfterTime
		/// </summary>
		public DateTime? AMBootAfterTime { get; set; }



		/// <summary>
		/// AMBootAfterTmp
		/// </summary>
		public decimal? AMBootAfterTmp { get; set; }



		/// <summary>
		/// AMBootAfterHum
		/// </summary>
		public decimal? AMBootAfterHum { get; set; }



		/// <summary>
		/// PMBootingTime
		/// </summary>
		public DateTime? PMBootingTime { get; set; }



		/// <summary>
		/// PMBootBeforeTmp
		/// </summary>
		public decimal? PMBootBeforeTmp { get; set; }



		/// <summary>
		/// PMBootBeforeHum
		/// </summary>
		public decimal? PMBootBeforeHum { get; set; }



		/// <summary>
		/// PMObservedTime
		/// </summary>
		public DateTime? PMObservedTime { get; set; }



		/// <summary>
		/// PMBootingTmp
		/// </summary>
		public decimal? PMBootingTmp { get; set; }



		/// <summary>
		/// PMBootingHum
		/// </summary>
		public decimal? PMBootingHum { get; set; }



		/// <summary>
		/// PMBootAfterTime
		/// </summary>
		public DateTime? PMBootAfterTime { get; set; }



		/// <summary>
		/// PMBootAfterTmp
		/// </summary>
		public decimal? PMBootAfterTmp { get; set; }



		/// <summary>
		/// PMBootAfterHum
		/// </summary>
		public decimal? PMBootAfterHum { get; set; }



		/// <summary>
		/// EmployeeId
		/// </summary>
		[Required(ErrorMessage="EmployeeId不能为空")]
		public string EmployeeId { get; set; }



		/// <summary>
		/// EmployeeName
		/// </summary>
		public string EmployeeName { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		[Required(ErrorMessage="CreationTime不能为空")]
		public DateTime CreationTime { get; set; }




    }
}