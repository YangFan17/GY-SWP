using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class NewLcBLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                       name: "LC_ForkliftMonthWhRecords",
                       columns: table => new
                       { Id = table.Column<Guid>(nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SuperintendentId = table.Column<string>(maxLength: 50, nullable: false), CreationTime = table.Column<DateTime>(nullable: false), IsScrewBad = table.Column<bool>(nullable: true), IsTireSurfaceBad = table.Column<bool>(nullable: true), IsDjyDczBad = table.Column<bool>(nullable: true), IsSpareParts = table.Column<bool>(nullable: true), IsInspectQcEtc = table.Column<bool>(nullable: true), IsLimitDeviceBad = table.Column<bool>(nullable: true), IsInspectRhQcEtc = table.Column<bool>(nullable: true), IsCircuitsBad = table.Column<bool>(nullable: true), IsTerminalBad = table.Column<bool>(nullable: true), IsTubingJackBad = table.Column<bool>(nullable: true), IsPowerControlBad = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                       constraints: table =>
                       {
                           table.PrimaryKey("PK_LC_ForkliftMonthWhRecords", x => x.Id);
                       });
            migrationBuilder.CreateTable(
                name: "LC_ForkliftWeekWhRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SuperintendentId = table.Column<string>(maxLength: 50, nullable: false), CreationTime = table.Column<DateTime>(nullable: false), IsSpareParts = table.Column<bool>(nullable: true), IsInspectQcEtc = table.Column<bool>(nullable: true), IsLimitDeviceBad = table.Column<bool>(nullable: true), IsInspectRhQcEtc = table.Column<bool>(nullable: true), IsDjyDczBad = table.Column<bool>(nullable: true), IsTerminalBad = table.Column<bool>(nullable: true), IsScrewBad = table.Column<bool>(nullable: true), IsTubingJackBad = table.Column<bool>(nullable: true), IsFilterBad = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_ForkliftWeekWhRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_KyjFunctionRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), DeviceID = table.Column<string>(maxLength: 100, nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SupervisorId = table.Column<string>(maxLength: 50, nullable: false), RunningTime = table.Column<DateTime>(nullable: true), UseTime = table.Column<DateTime>(nullable: true), DownTime = table.Column<DateTime>(nullable: true), IsDeviceComplete = table.Column<bool>(nullable: true), IsPipelineOk = table.Column<bool>(nullable: true), IsLubricatingOilOk = table.Column<bool>(nullable: true), IsVentilatingFanOpen = table.Column<bool>(nullable: true), IsSafetyValveOk = table.Column<bool>(nullable: true), IsPressureNormal = table.Column<bool>(nullable: true), IsPCShowNormal = table.Column<bool>(nullable: true), IsRunningSoundBad = table.Column<bool>(nullable: true), IsLsLqLyOk = table.Column<bool>(nullable: true), IsDrainValveOk = table.Column<bool>(nullable: true), IsPowerSupplyClose = table.Column<bool>(nullable: true), IsDeviceClean = table.Column<bool>(nullable: true), Desc = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_KyjFunctionRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_KyjMonthMaintainRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), DeviceID = table.Column<string>(maxLength: 100, nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SupervisorId = table.Column<string>(maxLength: 50, nullable: false), IsDeviceClean = table.Column<bool>(nullable: true), IsSafetyMarkClear = table.Column<bool>(nullable: true), IsScrewFastening = table.Column<bool>(nullable: true), IsTheOilLevelOk = table.Column<bool>(nullable: true), IsOilAndGasBad = table.Column<bool>(nullable: true), IsAirFilterOk = table.Column<bool>(nullable: true), IsPressureGaugeOk = table.Column<bool>(nullable: true), IsPressureGaugePointerOk = table.Column<bool>(nullable: true), IsSafetyValveOk = table.Column<bool>(nullable: true), IsCoolingPlateClean = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_KyjMonthMaintainRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_KyjWeekMaintainRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), DeviceID = table.Column<string>(maxLength: 100, nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SupervisorId = table.Column<string>(maxLength: 50, nullable: false), IsScrewFastening = table.Column<bool>(nullable: true), IsOilPressureOk = table.Column<bool>(nullable: true), IsOilAndGasBad = table.Column<bool>(nullable: true), IsPressureGaugePointerOk = table.Column<bool>(nullable: true), IsTheOilLevelOk = table.Column<bool>(nullable: true), IsAirFilterOk = table.Column<bool>(nullable: true), IsDeviceClean = table.Column<bool>(nullable: true), IsPressureGaugeOk = table.Column<bool>(nullable: true), IsSafetyValveOk = table.Column<bool>(nullable: true), IsCoolingPlateClean = table.Column<bool>(nullable: true), IsDrainValveOpen = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_KyjWeekMaintainRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_LPFunctionRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), DeviceID = table.Column<string>(maxLength: 100, nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SupervisorId = table.Column<string>(maxLength: 50, nullable: false), RunningTime = table.Column<DateTime>(nullable: true), UseTime = table.Column<DateTime>(nullable: true), DownTime = table.Column<DateTime>(nullable: true), IsSwitchOk = table.Column<bool>(nullable: true), IsLiftingOk = table.Column<bool>(nullable: true), IsGuardrailOk = table.Column<bool>(nullable: true), IsOutriggerLegOk = table.Column<bool>(nullable: true), IsGroundSmooth = table.Column<bool>(nullable: true), IsOutriggerLegStable = table.Column<bool>(nullable: true), IsLevelLeveling = table.Column<bool>(nullable: true), IsLiftingMachineBad = table.Column<bool>(nullable: true), IsLoadExceeding = table.Column<bool>(nullable: true), IsGuardrailPositionOk = table.Column<bool>(nullable: true), IsOutriggerCloseUp = table.Column<bool>(nullable: true), IsDeviceClean = table.Column<bool>(nullable: true), Desc = table.Column<string>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_LPFunctionRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_LPMaintainRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), DeviceID = table.Column<string>(maxLength: 100, nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SupervisorId = table.Column<string>(maxLength: 50, nullable: false), IsLineDamaged = table.Column<bool>(nullable: true), IsControlButOk = table.Column<bool>(nullable: true), IsScramSwitchOk = table.Column<bool>(nullable: true), IsDeviceCleaning = table.Column<bool>(nullable: true), IsGuardrailOk = table.Column<bool>(nullable: true), IsOutriggerLegOk = table.Column<bool>(nullable: true), IsChainGroupTightness = table.Column<bool>(nullable: true), IsScrewFastening = table.Column<bool>(nullable: true), IsOilLevelSatisfy = table.Column<bool>(nullable: true), IsMotorRunning = table.Column<bool>(nullable: true), IsLiftingMachineBad = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_LPMaintainRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_SortingMonthRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SuperintendentId = table.Column<string>(maxLength: 50, nullable: false), CreationTime = table.Column<DateTime>(nullable: false), IsFjxButtonOk = table.Column<bool>(nullable: true), IsTensioningModerate = table.Column<bool>(nullable: true), IsFjxDeviceBad = table.Column<bool>(nullable: true), IsFjxBoltBad = table.Column<bool>(nullable: true), IsFjxProtectiveDeviceBad = table.Column<bool>(nullable: true), IsFjxNetworkDataLineBad = table.Column<bool>(nullable: true), IsFjxDlbhJdxOk = table.Column<bool>(nullable: true), IsSbTtBad = table.Column<bool>(nullable: true), IsChainLubeOk = table.Column<bool>(nullable: true), IsBzjButtonOk = table.Column<bool>(nullable: true), IsJlkModerate = table.Column<bool>(nullable: true), IsBzjDeviceBad = table.Column<bool>(nullable: true), IsBzjBoltBad = table.Column<bool>(nullable: true), IsBzjProtectiveDeviceBad = table.Column<bool>(nullable: true), IsBzjNetworkDataLineBad = table.Column<bool>(nullable: true), IsBzjDlbhJdxOk = table.Column<bool>(nullable: true), IsSslClear = table.Column<bool>(nullable: true), IsCylinderJointBad = table.Column<bool>(nullable: true), IsTbjDeviceBad = table.Column<bool>(nullable: true), IsTbjBoltBad = table.Column<bool>(nullable: true), IsOpticalSensorOk = table.Column<bool>(nullable: true), IsDmjProtectiveDeviceBad = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_SortingMonthRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_SortingWeekRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SuperintendentId = table.Column<string>(maxLength: 50, nullable: false), CreationTime = table.Column<DateTime>(nullable: false), IsInspectZjd = table.Column<bool>(nullable: true), IsBearingEtcBad = table.Column<bool>(nullable: true), IsLinePipeBad = table.Column<bool>(nullable: true), IsNetworkDataLineBad = table.Column<bool>(nullable: true), IsSbTtBad = table.Column<bool>(nullable: true), IsBeltSurfaceClean = table.Column<bool>(nullable: true), IsKzxlQJOk = table.Column<bool>(nullable: true), IsControlBtnOk = table.Column<bool>(nullable: true), IsBzjBearingEtcBad = table.Column<bool>(nullable: true), IsTracheaBad = table.Column<bool>(nullable: true), IsBzjNetworkDataLineBad = table.Column<bool>(nullable: true), IsFqdJrsGwjBad = table.Column<bool>(nullable: true), IsCylinderOk = table.Column<bool>(nullable: true), IsLiftingGuideBarBad = table.Column<bool>(nullable: true), IsPrintHeadBad = table.Column<bool>(nullable: true), IsPlacementPositionOk = table.Column<bool>(nullable: true), IsParameterSettingOk = table.Column<bool>(nullable: true), IsWipeLens = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_SortingWeekRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_SsjMonthWhByRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SuperintendentId = table.Column<string>(maxLength: 50, nullable: false), CreationTime = table.Column<DateTime>(nullable: false), IsPartLubrication = table.Column<bool>(nullable: true), IsShapeBad = table.Column<bool>(nullable: true), IsInsulationBad = table.Column<bool>(nullable: true), IsButtonBad = table.Column<bool>(nullable: true), IsBoltBad = table.Column<bool>(nullable: true), IsLineBad = table.Column<bool>(nullable: true), IsPowerCircuitBad = table.Column<bool>(nullable: true), IsChainTensionBad = table.Column<bool>(nullable: true), IsBearingRunningOk = table.Column<bool>(nullable: true), IsEviceBad = table.Column<bool>(nullable: true), IsSwitchOk = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_SsjMonthWhByRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_SsjWeekWhByRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), EmployeeId = table.Column<string>(maxLength: 50, nullable: false), SuperintendentId = table.Column<string>(maxLength: 50, nullable: false), CreationTime = table.Column<DateTime>(nullable: false), IsPartLubrication = table.Column<bool>(nullable: true), IsShapeBad = table.Column<bool>(nullable: true), IsBoltBad = table.Column<bool>(nullable: true), IsButtonBad = table.Column<bool>(nullable: true), IsPowerCircuitBad = table.Column<bool>(nullable: true), IsLineBad = table.Column<bool>(nullable: true), IsBearingRunningOk = table.Column<bool>(nullable: true), IsChainTensionBad = table.Column<bool>(nullable: true), IsBeltBad = table.Column<bool>(nullable: true), IseviceBad = table.Column<bool>(nullable: true), DiscoverProblems = table.Column<string>(maxLength: 500, nullable: true), ProcessingResult = table.Column<string>(maxLength: 500, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_SsjWeekWhByRecords", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "LC_ForkliftMonthWhRecord");
            migrationBuilder.DropTable(name: "LC_ForkliftWeekWhRecord");
            migrationBuilder.DropTable(name: "LC_KyjFunctionRecord");
            migrationBuilder.DropTable(name: "LC_KyjMonthMaintainRecord");
            migrationBuilder.DropTable(name: "LC_KyjWeekMaintainRecord");
            migrationBuilder.DropTable(name: "LC_LPFunctionRecord");
            migrationBuilder.DropTable(name: "LC_LPMaintainRecord");
            migrationBuilder.DropTable(name: "LC_SortingMonthRecord");
            migrationBuilder.DropTable(name: "LC_SortingWeekRecord");
            migrationBuilder.DropTable(name: "LC_SsjMonthWhByRecord");
            migrationBuilder.DropTable(name: "LC_SsjWeekWhByRecord");
        }
    }
}
