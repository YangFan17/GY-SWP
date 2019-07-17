using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class PositionBLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                  name: "MainPointsRecords",
                  columns: table => new
                  { Id = table.Column<Guid>(nullable: false), PositionInfoId = table.Column<Guid>(nullable: false), DocumentId = table.Column<Guid>(nullable: false), MainPoint = table.Column<string>(maxLength: 2000, nullable: true), CreationTime = table.Column<DateTime>(nullable: true) },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_MainPointsRecords", x => x.Id);
                  });

            migrationBuilder.CreateTable(
                name: "PositionInfos",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), Position = table.Column<string>(maxLength: 200, nullable: false), Duties = table.Column<string>(maxLength: 500, nullable: true), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 20, nullable: true), CreationTime = table.Column<DateTime>(nullable: true), CreatorUserId = table.Column<string>(nullable: true), LastModificationTime = table.Column<DateTime>(nullable: true), LastModifierUserId = table.Column<string>(nullable: true), DeletionTime = table.Column<DateTime>(nullable: true), DeleterUserId = table.Column<string>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionInfos", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "MainPointsRecords");
            migrationBuilder.DropTable(name: "PositionInfos");
        }
    }
}
