using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class IndicatorsBLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Indicators",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), Title = table.Column<string>(maxLength: 200, nullable: false), Paraphrase = table.Column<string>(maxLength: 2000, nullable: false), MeasuringWay = table.Column<string>(maxLength: 2000, nullable: false), CreationTime = table.Column<DateTime>(nullable: false), CreatorEmpeeId = table.Column<string>(maxLength: 200, nullable: false), CreatorEmpName = table.Column<string>(maxLength: 50, nullable: true), CreatorDeptId = table.Column<long>(nullable: false), CreatorDeptName = table.Column<string>(maxLength: 100, nullable: true), DeptId = table.Column<long>(nullable: false), DeptName = table.Column<string>(maxLength: 100, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorsDetails",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), IndicatorsId = table.Column<Guid>(nullable: false), ClauseId = table.Column<decimal>(nullable: false), Status = table.Column<int>(nullable: false), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorsDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Indicatorss");
            migrationBuilder.DropTable(name: "IndicatorsDetails");
        }
    }
}
