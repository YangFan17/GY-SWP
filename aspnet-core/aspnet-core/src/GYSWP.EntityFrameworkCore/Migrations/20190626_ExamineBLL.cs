using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class ExamineBLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriterionExamines",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), Title = table.Column<string>(maxLength: 200, nullable: false), Type = table.Column<int>(nullable: false), CreationTime = table.Column<DateTime>(nullable: false), CreatorEmpeeId = table.Column<string>(maxLength: 200, nullable: false), CreatorEmpName = table.Column<string>(maxLength: 50, nullable: true), CreatorDeptId = table.Column<long>(nullable: false), DeptId = table.Column<long>(nullable: false), CreatorDeptName = table.Column<string>(maxLength: 100, nullable: true), DeptName = table.Column<string>(maxLength: 100, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriterionExamines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamineDetails",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), CriterionExamineId = table.Column<Guid>(nullable: false), ClauseId = table.Column<Guid>(nullable: false), Status = table.Column<int>(nullable: false), Result = table.Column<int>(nullable: false), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true), CreationTime = table.Column<DateTime>(nullable: false), CreatorEmpeeId = table.Column<string>(maxLength: 200, nullable: false), CreatorEmpName = table.Column<string>(maxLength: 50, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamineDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamineFeedbacks",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), Type = table.Column<int>(nullable: false), BusinessId = table.Column<Guid>(nullable: false), CourseType = table.Column<int>(nullable: false), Reason = table.Column<string>(maxLength: 1000, nullable: true), Solution = table.Column<string>(maxLength: 1000, nullable: true), CreationTime = table.Column<DateTime>(nullable: false), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamineFeedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamineResults",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), ExamineDetailId = table.Column<Guid>(nullable: false), Content = table.Column<string>(maxLength: 500, nullable: false), EmployeeId = table.Column<string>(maxLength: 200, nullable: false), EmployeeName = table.Column<string>(maxLength: 50, nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamineResults", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "CriterionExamines");
            migrationBuilder.DropTable(name: "ExamineDetails");
            migrationBuilder.DropTable(name: "ExamineFeedbacks");
            migrationBuilder.DropTable(name: "ExamineResults");
        }
    }
}
