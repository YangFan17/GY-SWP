using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class SummerBLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LC_MildewSummers",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), TimeLogId = table.Column<Guid>(nullable: false), AMBootTime = table.Column<DateTime>(nullable: false), AMBootBeforeTmp = table.Column<decimal>(nullable: true), AMBootBeforeHum = table.Column<decimal>(nullable: true), AMObservedTime = table.Column<DateTime>(nullable: true), AMBootingTmp = table.Column<decimal>(nullable: true), AMBootingHum = table.Column<decimal>(nullable: true), AMBootAfterTime = table.Column<DateTime>(nullable: true), AMBootAfterTmp = table.Column<decimal>(nullable: true), AMBootAfterHum = table.Column<decimal>(nullable: true), PMBootingTime = table.Column<DateTime>(nullable: true), PMBootBeforeTmp = table.Column<decimal>(nullable: true), PMBootBeforeHum = table.Column<decimal>(nullable: true), PMObservedTime = table.Column<DateTime>(nullable: true), PMBootingTmp = table.Column<decimal>(nullable: true), PMBootingHum = table.Column<decimal>(nullable: true), PMBootAfterTime = table.Column<DateTime>(nullable: true), PMBootAfterTmp = table.Column<decimal>(nullable: true), PMBootAfterHum = table.Column<decimal>(nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_MildewSummers", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "LC_MildewSummers");
        }
    }
}
