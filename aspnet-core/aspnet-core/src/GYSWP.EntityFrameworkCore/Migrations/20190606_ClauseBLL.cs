using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class ClauseBLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "SelfChekRecords",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false)
                , ClauseId = table.Column<Guid>(nullable: false)
                , EmployeeId = table.Column<string>(nullable: false)
                , IsTodayFirst = table.Column<bool>(nullable: true)
                , CreationTime = table.Column<DateTime>(nullable: false)
                , EmployeeName = table.Column<string>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfChekRecords", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SelfChekRecords");
        }
    }
}
