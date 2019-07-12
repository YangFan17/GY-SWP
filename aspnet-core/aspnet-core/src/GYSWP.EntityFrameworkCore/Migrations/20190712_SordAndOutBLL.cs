using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class SordAndOutBLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LC_CigaretExchanges",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), OriginPlace = table.Column<string>(maxLength: 100, nullable: true), Name = table.Column<string>(maxLength: 100, nullable: true), Unit = table.Column<string>(maxLength: 100, nullable: true), Num = table.Column<int>(nullable: true), Reason = table.Column<string>(maxLength: 2000, nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_CigaretExchanges", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_OutScanRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), OrderNum = table.Column<int>(nullable: true), ExpectedScanNum = table.Column<int>(nullable: true), AcutalScanNum = table.Column<int>(nullable: true), AloneNotScanNum = table.Column<int>(nullable: true), Remark = table.Column<string>(maxLength: 2000, nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_OutScanRecords", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_SortingEquipChecks",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), ResponsibleName = table.Column<string>(maxLength: 100, nullable: true), SupervisorName = table.Column<string>(maxLength: 50, nullable: true), IsChainPlateOk = table.Column<bool>(nullable: false), IsControlSwitchOk = table.Column<bool>(nullable: false), IsElcOrGasBad = table.Column<bool>(nullable: false), IsLiftUp = table.Column<bool>(nullable: false), IsSortSysOk = table.Column<bool>(nullable: false), IsChanDirty = table.Column<bool>(nullable: false), IsCutSealDirty = table.Column<bool>(nullable: false), IsBZJControlSwitchOk = table.Column<bool>(nullable: false), IsBZJElcOrGasBad = table.Column<bool>(nullable: false), IsTempOk = table.Column<bool>(nullable: false), IsBZJSysOk = table.Column<bool>(nullable: false), IsStoveOk = table.Column<bool>(nullable: false), IsLabelingOk = table.Column<bool>(nullable: false), IsTBJElcOrGasBad = table.Column<bool>(nullable: false), IsLaserShieldOk = table.Column<bool>(nullable: false), IsLineOrMachineOk = table.Column<bool>(nullable: false), IsCigaretteHouseOk = table.Column<bool>(nullable: false), IsSingleOk = table.Column<bool>(nullable: false), IsMainLineOk = table.Column<bool>(nullable: false), IsCoderOk = table.Column<bool>(nullable: false), IsBZJWorkOk = table.Column<bool>(nullable: true), IsBeltDeviation = table.Column<bool>(nullable: false), IsFBJOk = table.Column<bool>(nullable: false), IsTBJOk = table.Column<bool>(nullable: false), IsSysOutOk = table.Column<bool>(nullable: false), IsShutElcOrGas = table.Column<bool>(nullable: false), IsDataCallback = table.Column<bool>(nullable: false), IsMachineClean = table.Column<bool>(nullable: false), Troubleshooting = table.Column<string>(maxLength: 2000, nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_SortingEquipChecks", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_TeamSafetyActivitys",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), SafetyMeeting = table.Column<string>(maxLength: 500, nullable: true), IsSafeEquipOk = table.Column<bool>(nullable: false), IsEmpHealth = table.Column<bool>(nullable: false), IsTdjOrLsjOk = table.Column<bool>(nullable: false), IsAisleOk = table.Column<bool>(nullable: false), IsExitBad = table.Column<bool>(nullable: false), IsFireEquipBad = table.Column<bool>(nullable: false), IsSafeMarkClean = table.Column<bool>(nullable: false), IsSafeMarkFall = table.Column<bool>(nullable: false), EmpSafeAdvice = table.Column<string>(nullable: true), CommonCigaretNum = table.Column<int>(nullable: true), ShapedCigaretNum = table.Column<int>(nullable: true), BeginSortTime = table.Column<DateTime>(nullable: true), EndSortTime = table.Column<DateTime>(nullable: true), NormalStopTime = table.Column<DateTime>(nullable: true), AbnormalStopTime = table.Column<DateTime>(nullable: true), IsNotDanger = table.Column<bool>(nullable: false), IsOtherAdmittance = table.Column<bool>(nullable: false), IsViolation = table.Column<bool>(nullable: false), IsElcOrGasShut = table.Column<bool>(nullable: false), IsCloseWindow = table.Column<bool>(nullable: false), SafeSupervision = table.Column<string>(maxLength: 500, nullable: true), ResponsibleName = table.Column<string>(maxLength: 500, nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_TeamSafetyActivitys", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "LC_UseOutStorages",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), SortLineName = table.Column<string>(maxLength: 200, nullable: true), ProductName = table.Column<string>(maxLength: 100, nullable: true), PreDiffNum = table.Column<int>(nullable: true), PreAloneNum = table.Column<int>(nullable: true), SupWholeNum = table.Column<int>(nullable: true), SupAllPieceNum = table.Column<int>(nullable: true), SupAllItemNum = table.Column<int>(nullable: true), AcutalOrderNum = table.Column<int>(nullable: true), CheckNum = table.Column<int>(nullable: true), CheckAloneNum = table.Column<int>(nullable: true), ClerkName = table.Column<string>(maxLength: 50, nullable: true), SortorName = table.Column<string>(maxLength: 50, nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_UseOutStorages", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "LC_CigaretExchanges");
            migrationBuilder.DropTable(name: "LC_OutScanRecords");
            migrationBuilder.DropTable(name: "LC_SortingEquipChecks");
            migrationBuilder.DropTable(name: "LC_TeamSafetyActivitys");
            migrationBuilder.DropTable(name: "LC_UseOutStorages");
        }
    }
}
