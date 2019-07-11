using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class EquiBLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LC_ConveyorChecks",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), EquiNo = table.Column<string>(nullable: true), ResponsibleName = table.Column<string>(maxLength: 100, nullable: true), SupervisorName = table.Column<string>(maxLength: 50, nullable: true), RunTime = table.Column<DateTime>(nullable: true), BeginTime = table.Column<DateTime>(nullable: true), EndTime = table.Column<DateTime>(nullable: true), IsEquiFaceClean = table.Column<bool>(nullable: false), IsSteadyOk = table.Column<bool>(nullable: false), IsScrewOk = table.Column<bool>(nullable: false), IsButtonOk = table.Column<bool>(nullable: false), IsElcLineBad = table.Column<bool>(nullable: false), IsBeltSlant = table.Column<bool>(nullable: false), IsBearingOk = table.Column<bool>(nullable: false), IsSoundOk = table.Column<bool>(nullable: false), IsMotor = table.Column<bool>(nullable: false), IsShutPower = table.Column<bool>(nullable: false), IsBeltBad = table.Column<bool>(nullable: false), IsClean = table.Column<bool>(nullable: false), Troubleshooting = table.Column<string>(maxLength: 2000, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_ConveyorChecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LC_ForkliftChecks",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), EquiNo = table.Column<string>(nullable: true), ResponsibleName = table.Column<string>(maxLength: 100, nullable: true), SupervisorName = table.Column<string>(maxLength: 50, nullable: true), RunTime = table.Column<DateTime>(nullable: true), BeginTime = table.Column<DateTime>(nullable: true), EndTime = table.Column<DateTime>(nullable: true), IslubricatingOk = table.Column<bool>(nullable: false), IsBatteryBad = table.Column<bool>(nullable: false), IsTurnOrBreakOk = table.Column<bool>(nullable: false), IsLightOrHornOk = table.Column<bool>(nullable: false), IsFullCharged = table.Column<bool>(nullable: false), IsForkLifhOk = table.Column<bool>(nullable: false), IsRunFullCharged = table.Column<bool>(nullable: false), IsRunTurnOrBreakOk = table.Column<bool>(nullable: false), IsRunLightOrHornOk = table.Column<bool>(nullable: false), IsRunSoundOk = table.Column<bool>(nullable: false), IsParkStandard = table.Column<bool>(nullable: false), IsShutPower = table.Column<bool>(nullable: false), IsNeedCharge = table.Column<bool>(nullable: false), IsClean = table.Column<bool>(nullable: false), Troubleshooting = table.Column<string>(maxLength: 2000, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_ForkliftChecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LC_InStorageBills",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), IsYczmBill = table.Column<bool>(nullable: false), IsJyhtBill = table.Column<bool>(nullable: false), IsZzsBill = table.Column<bool>(nullable: false), IsCarSeal = table.Column<bool>(nullable: false), IsCarElcLock = table.Column<bool>(nullable: false), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_InStorageBills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LC_InStorageRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), Name = table.Column<string>(maxLength: 100, nullable: false), CarNo = table.Column<string>(maxLength: 100, nullable: false), DeliveryUnit = table.Column<string>(maxLength: 100, nullable: false), BillNo = table.Column<string>(maxLength: 100, nullable: false), ReceivableAmount = table.Column<int>(nullable: false), ActualAmount = table.Column<int>(nullable: false), DiffContent = table.Column<string>(maxLength: 200, nullable: false), Quality = table.Column<string>(maxLength: 100, nullable: false), ReceiverName = table.Column<string>(maxLength: 50, nullable: false), Remark = table.Column<string>(maxLength: 500, nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_InStorageRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LC_QualityRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), Name = table.Column<string>(maxLength: 100, nullable: false), WholesaleAmount = table.Column<decimal>(nullable: true), SaleQuantity = table.Column<int>(nullable: true), CarNo = table.Column<string>(maxLength: 100, nullable: true), CompensationAmount = table.Column<decimal>(nullable: true), CarrierName = table.Column<string>(maxLength: 50, nullable: true), ClerkName = table.Column<string>(maxLength: 50, nullable: true), HandoverTime = table.Column<DateTime>(nullable: true), Amount = table.Column<decimal>(nullable: true), HandManName = table.Column<string>(maxLength: 50, nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_QualityRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LC_TimeLogs",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), Type = table.Column<int>(nullable: false), Status = table.Column<int>(nullable: false), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_TimeLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "LC_ConveyorChecks");
            migrationBuilder.DropTable(name: "LC_ForkliftChecks");
            migrationBuilder.DropTable(name: "LC_InStorageBills");
            migrationBuilder.DropTable(name: "LC_InStorageRecords");
            migrationBuilder.DropTable(name: "LC_QualityRecords");
            migrationBuilder.DropTable(name: "LC_TimeLogs");
        }
    }
}