

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_SortingEquipChecks;

namespace GYSWP.LC_SortingEquipChecks.Dtos
{
    public class LC_SortingEquipCheckListDto : EntityDto<Guid>,IHasCreationTime 
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
		public bool? IsChainPlateOk { get; set; }



		/// <summary>
		/// IsControlSwitchOk
		/// </summary>
		public bool? IsControlSwitchOk { get; set; }



		/// <summary>
		/// IsElcOrGasBad
		/// </summary>
		public bool? IsElcOrGasBad { get; set; }



		/// <summary>
		/// IsLiftUp
		/// </summary>
		public bool? IsLiftUp { get; set; }



		/// <summary>
		/// IsSortSysOk
		/// </summary>
		public bool? IsSortSysOk { get; set; }



		/// <summary>
		/// IsChanDirty
		/// </summary>
		public bool? IsChanDirty { get; set; }



		/// <summary>
		/// IsCutSealDirty
		/// </summary>
		public bool? IsCutSealDirty { get; set; }



		/// <summary>
		/// IsBZJControlSwitchOk
		/// </summary>
		public bool? IsBZJControlSwitchOk { get; set; }



		/// <summary>
		/// IsBZJElcOrGasBad
		/// </summary>
		public bool? IsBZJElcOrGasBad { get; set; }



		/// <summary>
		/// IsTempOk
		/// </summary>
		public bool? IsTempOk { get; set; }



		/// <summary>
		/// IsBZJSysOk
		/// </summary>
		public bool? IsBZJSysOk { get; set; }



		/// <summary>
		/// IsStoveOk
		/// </summary>
		public bool? IsStoveOk { get; set; }



		/// <summary>
		/// IsLabelingOk
		/// </summary>
		public bool? IsLabelingOk { get; set; }



		/// <summary>
		/// IsTBJElcOrGasBad
		/// </summary>
		public bool? IsTBJElcOrGasBad { get; set; }



		/// <summary>
		/// IsLaserShieldOk
		/// </summary>
		public bool? IsLaserShieldOk { get; set; }



		/// <summary>
		/// IsLineOrMachineOk
		/// </summary>
		public bool? IsLineOrMachineOk { get; set; }



		/// <summary>
		/// IsCigaretteHouseOk
		/// </summary>
		public bool? IsCigaretteHouseOk { get; set; }



		/// <summary>
		/// IsSingleOk
		/// </summary>
		public bool? IsSingleOk { get; set; }



		/// <summary>
		/// IsMainLineOk
		/// </summary>
		public bool? IsMainLineOk { get; set; }



		/// <summary>
		/// IsCoderOk
		/// </summary>
		public bool? IsCoderOk { get; set; }



		/// <summary>
		/// IsBZJWorkOk
		/// </summary>
		public bool? IsBZJWorkOk { get; set; }



		/// <summary>
		/// IsBeltDeviation
		/// </summary>
		public bool? IsBeltDeviation { get; set; }



		/// <summary>
		/// IsFBJOk
		/// </summary>
		public bool? IsFBJOk { get; set; }



		/// <summary>
		/// IsTBJOk
		/// </summary>
		public bool? IsTBJOk { get; set; }



		/// <summary>
		/// IsSysOutOk
		/// </summary>
		public bool? IsSysOutOk { get; set; }



		/// <summary>
		/// IsShutElcOrGas
		/// </summary>
		public bool? IsShutElcOrGas { get; set; }



		/// <summary>
		/// IsDataCallback
		/// </summary>
		public bool? IsDataCallback { get; set; }



		/// <summary>
		/// IsMachineClean
		/// </summary>
		public bool? IsMachineClean { get; set; }



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