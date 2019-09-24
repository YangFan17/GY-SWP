
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using GYSWP.LC_SortingEquipChecks;

namespace  GYSWP.LC_SortingEquipChecks.Dtos
{
    public class LC_SortingEquipCheckEditDto : EntityDto<Guid?>, IHasCreationTime
    {

		/// <summary>
		/// TimeLogId
		/// </summary>
		public Guid? TimeLogId { get; set; }



		/// <summary>
		/// ResponsibleName
		/// </summary>
		public string ResponsibleName { get; set; }



		/// <summary>
		/// SupervisorName
		/// </summary>
		public string SupervisorName { get; set; }



		/// <summary>
		/// IsChainPlateOk
		/// </summary>
		[Required(ErrorMessage="IsChainPlateOk不能为空")]
		public bool IsChainPlateOk { get; set; }



		/// <summary>
		/// IsControlSwitchOk
		/// </summary>
		[Required(ErrorMessage="IsControlSwitchOk不能为空")]
		public bool IsControlSwitchOk { get; set; }



		/// <summary>
		/// IsElcOrGasBad
		/// </summary>
		[Required(ErrorMessage="IsElcOrGasBad不能为空")]
		public bool IsElcOrGasBad { get; set; }



		/// <summary>
		/// IsLiftUp
		/// </summary>
		[Required(ErrorMessage="IsLiftUp不能为空")]
		public bool IsLiftUp { get; set; }



		/// <summary>
		/// IsSortSysOk
		/// </summary>
		[Required(ErrorMessage="IsSortSysOk不能为空")]
		public bool IsSortSysOk { get; set; }



		/// <summary>
		/// IsChanDirty
		/// </summary>
		[Required(ErrorMessage="IsChanDirty不能为空")]
		public bool IsChanDirty { get; set; }



		/// <summary>
		/// IsCutSealDirty
		/// </summary>
		[Required(ErrorMessage="IsCutSealDirty不能为空")]
		public bool IsCutSealDirty { get; set; }



		/// <summary>
		/// IsBZJControlSwitchOk
		/// </summary>
		[Required(ErrorMessage="IsBZJControlSwitchOk不能为空")]
		public bool IsBZJControlSwitchOk { get; set; }



		/// <summary>
		/// IsBZJElcOrGasBad
		/// </summary>
		[Required(ErrorMessage="IsBZJElcOrGasBad不能为空")]
		public bool IsBZJElcOrGasBad { get; set; }



		/// <summary>
		/// IsTempOk
		/// </summary>
		[Required(ErrorMessage="IsTempOk不能为空")]
		public bool IsTempOk { get; set; }



		/// <summary>
		/// IsBZJSysOk
		/// </summary>
		[Required(ErrorMessage="IsBZJSysOk不能为空")]
		public bool IsBZJSysOk { get; set; }



		/// <summary>
		/// IsStoveOk
		/// </summary>
		[Required(ErrorMessage="IsStoveOk不能为空")]
		public bool IsStoveOk { get; set; }



		/// <summary>
		/// IsLabelingOk
		/// </summary>
		[Required(ErrorMessage="IsLabelingOk不能为空")]
		public bool IsLabelingOk { get; set; }



		/// <summary>
		/// IsTBJElcOrGasBad
		/// </summary>
		[Required(ErrorMessage="IsTBJElcOrGasBad不能为空")]
		public bool IsTBJElcOrGasBad { get; set; }



		/// <summary>
		/// IsLaserShieldOk
		/// </summary>
		[Required(ErrorMessage="IsLaserShieldOk不能为空")]
		public bool IsLaserShieldOk { get; set; }



		/// <summary>
		/// IsLineOrMachineOk
		/// </summary>
		[Required(ErrorMessage="IsLineOrMachineOk不能为空")]
		public bool IsLineOrMachineOk { get; set; }



		/// <summary>
		/// IsCigaretteHouseOk
		/// </summary>
		[Required(ErrorMessage="IsCigaretteHouseOk不能为空")]
		public bool IsCigaretteHouseOk { get; set; }



		/// <summary>
		/// IsSingleOk
		/// </summary>
		[Required(ErrorMessage="IsSingleOk不能为空")]
		public bool IsSingleOk { get; set; }



		/// <summary>
		/// IsMainLineOk
		/// </summary>
		[Required(ErrorMessage="IsMainLineOk不能为空")]
		public bool IsMainLineOk { get; set; }



		/// <summary>
		/// IsCoderOk
		/// </summary>
		[Required(ErrorMessage="IsCoderOk不能为空")]
		public bool IsCoderOk { get; set; }



		/// <summary>
		/// IsBZJWorkOk
		/// </summary>
		public bool? IsBZJWorkOk { get; set; }



		/// <summary>
		/// IsBeltDeviation
		/// </summary>
		[Required(ErrorMessage="IsBeltDeviation不能为空")]
		public bool IsBeltDeviation { get; set; }



		/// <summary>
		/// IsFBJOk
		/// </summary>
		[Required(ErrorMessage="IsFBJOk不能为空")]
		public bool IsFBJOk { get; set; }



		/// <summary>
		/// IsTBJOk
		/// </summary>
		[Required(ErrorMessage="IsTBJOk不能为空")]
		public bool IsTBJOk { get; set; }



		/// <summary>
		/// IsSysOutOk
		/// </summary>
		[Required(ErrorMessage="IsSysOutOk不能为空")]
		public bool IsSysOutOk { get; set; }



		/// <summary>
		/// IsShutElcOrGas
		/// </summary>
		[Required(ErrorMessage="IsShutElcOrGas不能为空")]
		public bool IsShutElcOrGas { get; set; }



		/// <summary>
		/// IsDataCallback
		/// </summary>
		[Required(ErrorMessage="IsDataCallback不能为空")]
		public bool IsDataCallback { get; set; }



		/// <summary>
		/// IsMachineClean
		/// </summary>
		[Required(ErrorMessage="IsMachineClean不能为空")]
		public bool IsMachineClean { get; set; }



		/// <summary>
		/// Troubleshooting
		/// </summary>
		public string Troubleshooting { get; set; }



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