using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("20190924_NewLcBLL")]
    partial class NewLcBLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.LC_ForkliftMonthWhRecords.LC_ForkliftMonthWhRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SuperintendentId").IsRequired().HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<bool?>("IsScrewBad"); b.Property<bool?>("IsTireSurfaceBad"); b.Property<bool?>("IsDjyDczBad"); b.Property<bool?>("IsSpareParts"); b.Property<bool?>("IsInspectQcEtc"); b.Property<bool?>("IsLimitDeviceBad"); b.Property<bool?>("IsInspectRhQcEtc"); b.Property<bool?>("IsCircuitsBad"); b.Property<bool?>("IsTerminalBad"); b.Property<bool?>("IsTubingJackBad"); b.Property<bool?>("IsPowerControlBad"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_ForkliftMonthWhRecords");
            }); modelBuilder.Entity("GYSWP.LC_ForkliftWeekWhRecords.LC_ForkliftWeekWhRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SuperintendentId").IsRequired().HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<bool?>("IsSpareParts"); b.Property<bool?>("IsInspectQcEtc"); b.Property<bool?>("IsLimitDeviceBad"); b.Property<bool?>("IsInspectRhQcEtc"); b.Property<bool?>("IsDjyDczBad"); b.Property<bool?>("IsTerminalBad"); b.Property<bool?>("IsScrewBad"); b.Property<bool?>("IsTubingJackBad"); b.Property<bool?>("IsFilterBad"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_ForkliftWeekWhRecords");
            }); modelBuilder.Entity("GYSWP.LC_KyjFunctionRecords.LC_KyjFunctionRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("DeviceID").IsRequired().HasMaxLength(100); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SupervisorId").IsRequired().HasMaxLength(50); b.Property<DateTime?>("RunningTime"); b.Property<DateTime?>("UseTime"); b.Property<DateTime?>("DownTime"); b.Property<bool?>("IsDeviceComplete"); b.Property<bool?>("IsPipelineOk"); b.Property<bool?>("IsLubricatingOilOk"); b.Property<bool?>("IsVentilatingFanOpen"); b.Property<bool?>("IsSafetyValveOk"); b.Property<bool?>("IsPressureNormal"); b.Property<bool?>("IsPCShowNormal"); b.Property<bool?>("IsRunningSoundBad"); b.Property<bool?>("IsLsLqLyOk"); b.Property<bool?>("IsDrainValveOk"); b.Property<bool?>("IsPowerSupplyClose"); b.Property<bool?>("IsDeviceClean"); b.Property<string>("Desc").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_KyjFunctionRecords");
            }); modelBuilder.Entity("GYSWP.LC_KyjMonthMaintainRecords.LC_KyjMonthMaintainRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("DeviceID").IsRequired().HasMaxLength(100); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SupervisorId").IsRequired().HasMaxLength(50); b.Property<bool?>("IsDeviceClean"); b.Property<bool?>("IsSafetyMarkClear"); b.Property<bool?>("IsScrewFastening"); b.Property<bool?>("IsTheOilLevelOk"); b.Property<bool?>("IsOilAndGasBad"); b.Property<bool?>("IsAirFilterOk"); b.Property<bool?>("IsPressureGaugeOk"); b.Property<bool?>("IsPressureGaugePointerOk"); b.Property<bool?>("IsSafetyValveOk"); b.Property<bool?>("IsCoolingPlateClean"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_KyjMonthMaintainRecords");
            }); modelBuilder.Entity("GYSWP.LC_KyjWeekMaintainRecords.LC_KyjWeekMaintainRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("DeviceID").IsRequired().HasMaxLength(100); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SupervisorId").IsRequired().HasMaxLength(50); b.Property<bool?>("IsScrewFastening"); b.Property<bool?>("IsOilPressureOk"); b.Property<bool?>("IsOilAndGasBad"); b.Property<bool?>("IsPressureGaugePointerOk"); b.Property<bool?>("IsTheOilLevelOk"); b.Property<bool?>("IsAirFilterOk"); b.Property<bool?>("IsDeviceClean"); b.Property<bool?>("IsPressureGaugeOk"); b.Property<bool?>("IsSafetyValveOk"); b.Property<bool?>("IsCoolingPlateClean"); b.Property<bool?>("IsDrainValveOpen"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_KyjWeekMaintainRecords");
            }); modelBuilder.Entity("GYSWP.LC_LPFunctionRecords.LC_LPFunctionRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("DeviceID").IsRequired().HasMaxLength(100); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SupervisorId").IsRequired().HasMaxLength(50); b.Property<DateTime?>("RunningTime"); b.Property<DateTime?>("UseTime"); b.Property<DateTime?>("DownTime"); b.Property<bool?>("IsSwitchOk"); b.Property<bool?>("IsLiftingOk"); b.Property<bool?>("IsGuardrailOk"); b.Property<bool?>("IsOutriggerLegOk"); b.Property<bool?>("IsGroundSmooth"); b.Property<bool?>("IsOutriggerLegStable"); b.Property<bool?>("IsLevelLeveling"); b.Property<bool?>("IsLiftingMachineBad"); b.Property<bool?>("IsLoadExceeding"); b.Property<bool?>("IsGuardrailPositionOk"); b.Property<bool?>("IsOutriggerCloseUp"); b.Property<bool?>("IsDeviceClean"); b.Property<string>("Desc"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_LPFunctionRecords");
            }); modelBuilder.Entity("GYSWP.LC_LPMaintainRecords.LC_LPMaintainRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("DeviceID").IsRequired().HasMaxLength(100); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SupervisorId").IsRequired().HasMaxLength(50); b.Property<bool?>("IsLineDamaged"); b.Property<bool?>("IsControlButOk"); b.Property<bool?>("IsScramSwitchOk"); b.Property<bool?>("IsDeviceCleaning"); b.Property<bool?>("IsGuardrailOk"); b.Property<bool?>("IsOutriggerLegOk"); b.Property<bool?>("IsChainGroupTightness"); b.Property<bool?>("IsScrewFastening"); b.Property<bool?>("IsOilLevelSatisfy"); b.Property<bool?>("IsMotorRunning"); b.Property<bool?>("IsLiftingMachineBad"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_LPMaintainRecords");
            }); modelBuilder.Entity("GYSWP.LC_SortingMonthRecords.LC_SortingMonthRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SuperintendentId").IsRequired().HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<bool?>("IsFjxButtonOk"); b.Property<bool?>("IsTensioningModerate"); b.Property<bool?>("IsFjxDeviceBad"); b.Property<bool?>("IsFjxBoltBad"); b.Property<bool?>("IsFjxProtectiveDeviceBad"); b.Property<bool?>("IsFjxNetworkDataLineBad"); b.Property<bool?>("IsFjxDlbhJdxOk"); b.Property<bool?>("IsSbTtBad"); b.Property<bool?>("IsChainLubeOk"); b.Property<bool?>("IsBzjButtonOk"); b.Property<bool?>("IsJlkModerate"); b.Property<bool?>("IsBzjDeviceBad"); b.Property<bool?>("IsBzjBoltBad"); b.Property<bool?>("IsBzjProtectiveDeviceBad"); b.Property<bool?>("IsBzjNetworkDataLineBad"); b.Property<bool?>("IsBzjDlbhJdxOk"); b.Property<bool?>("IsSslClear"); b.Property<bool?>("IsCylinderJointBad"); b.Property<bool?>("IsTbjDeviceBad"); b.Property<bool?>("IsTbjBoltBad"); b.Property<bool?>("IsOpticalSensorOk"); b.Property<bool?>("IsDmjProtectiveDeviceBad"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_SortingMonthRecords");
            }); modelBuilder.Entity("GYSWP.LC_SortingWeekRecords.LC_SortingWeekRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SuperintendentId").IsRequired().HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<bool?>("IsInspectZjd"); b.Property<bool?>("IsBearingEtcBad"); b.Property<bool?>("IsLinePipeBad"); b.Property<bool?>("IsNetworkDataLineBad"); b.Property<bool?>("IsSbTtBad"); b.Property<bool?>("IsBeltSurfaceClean"); b.Property<bool?>("IsKzxlQJOk"); b.Property<bool?>("IsControlBtnOk"); b.Property<bool?>("IsBzjBearingEtcBad"); b.Property<bool?>("IsTracheaBad"); b.Property<bool?>("IsBzjNetworkDataLineBad"); b.Property<bool?>("IsFqdJrsGwjBad"); b.Property<bool?>("IsCylinderOk"); b.Property<bool?>("IsLiftingGuideBarBad"); b.Property<bool?>("IsPrintHeadBad"); b.Property<bool?>("IsPlacementPositionOk"); b.Property<bool?>("IsParameterSettingOk"); b.Property<bool?>("IsWipeLens"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_SortingWeekRecords");
            }); modelBuilder.Entity("GYSWP.LC_SsjMonthWhByRecords.LC_SsjMonthWhByRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SuperintendentId").IsRequired().HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<bool?>("IsPartLubrication"); b.Property<bool?>("IsShapeBad"); b.Property<bool?>("IsInsulationBad"); b.Property<bool?>("IsButtonBad"); b.Property<bool?>("IsBoltBad"); b.Property<bool?>("IsLineBad"); b.Property<bool?>("IsPowerCircuitBad"); b.Property<bool?>("IsChainTensionBad"); b.Property<bool?>("IsBearingRunningOk"); b.Property<bool?>("IsEviceBad"); b.Property<bool?>("IsSwitchOk"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_SsjMonthWhByRecords");
            }); modelBuilder.Entity("GYSWP.LC_SsjWeekWhByRecords.LC_SsjWeekWhByRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(50); b.Property<string>("SuperintendentId").IsRequired().HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<bool?>("IsPartLubrication"); b.Property<bool?>("IsShapeBad"); b.Property<bool?>("IsBoltBad"); b.Property<bool?>("IsButtonBad"); b.Property<bool?>("IsPowerCircuitBad"); b.Property<bool?>("IsLineBad"); b.Property<bool?>("IsBearingRunningOk"); b.Property<bool?>("IsChainTensionBad"); b.Property<bool?>("IsBeltBad"); b.Property<bool?>("IseviceBad"); b.Property<string>("DiscoverProblems").HasMaxLength(500); b.Property<string>("ProcessingResult").HasMaxLength(500); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_SsjWeekWhByRecords");
            });
        }
    }
}
