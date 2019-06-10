using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GYSWP.Migrations
{
    public partial class BLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                { Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), Name = table.Column<string>(maxLength: 50, nullable: false), ParentId = table.Column<int>(nullable: true), Desc = table.Column<string>(maxLength: 500, nullable: true), IsDeleted = table.Column<bool>(nullable: true), CreationTime = table.Column<DateTime>(nullable: true), CreatorUserId = table.Column<string>(nullable: true), LastModificationTime = table.Column<DateTime>(nullable: true), LastModifierUserId = table.Column<string>(nullable: true), DeletionTime = table.Column<DateTime>(nullable: true), DeleterUserId = table.Column<string>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clauses",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), ParentId = table.Column<Guid>(nullable: true), Type = table.Column<int>(nullable: false), DocumentId = table.Column<Guid>(nullable: true), CreationTime = table.Column<DateTime>(nullable: true), CreatorUserId = table.Column<string>(nullable: true), PublishTime = table.Column<DateTime>(nullable: true), PublishUserId = table.Column<string>(nullable: true), LastModificationTime = table.Column<DateTime>(nullable: true), LastModifierUserId = table.Column<string>(nullable: true), DeletionTime = table.Column<DateTime>(nullable: true), DeleterUserId = table.Column<string>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clauses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClauseRevisions",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), ClauseId = table.Column<Guid>(nullable: false), Content = table.Column<string>(nullable: true), PreContent = table.Column<string>(nullable: true), EmployeeId = table.Column<string>(nullable: false), CreationTime = table.Column<DateTime>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClauseRevisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetpClauses",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), ClauseId = table.Column<Guid>(nullable: false), DeptId = table.Column<long>(nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetpClauses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), Name = table.Column<string>(maxLength: 200, nullable: false), CategoryId = table.Column<int>(nullable: false), CategoryDesc = table.Column<string>(maxLength: 500, nullable: true), DocThum = table.Column<string>(nullable: true), Summary = table.Column<string>(maxLength: 2000, nullable: true), ReleaseDate = table.Column<DateTime>(nullable: true), QrCodeUrl = table.Column<string>(maxLength: 200, nullable: true), IsDeleted = table.Column<bool>(nullable: true), CreationTime = table.Column<DateTime>(nullable: true), CreatorUserId = table.Column<string>(nullable: true), LastModificationTime = table.Column<DateTime>(nullable: true), LastModifierUserId = table.Column<string>(nullable: true), DeletionTime = table.Column<DateTime>(nullable: true), DeleterUserId = table.Column<string>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeClauses",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), DetpClauseId = table.Column<Guid>(nullable: false), EmployeeId = table.Column<string>(nullable: false), IsSelfCheck = table.Column<bool>(nullable: false), SelfCheckTime = table.Column<DateTime>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeClauses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamineDetails",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), DeptId = table.Column<long>(nullable: false), ClauseId = table.Column<Guid>(nullable: false), Score = table.Column<int>(nullable: false), Remark = table.Column<string>(nullable: true), CreationTime = table.Column<DateTime>(nullable: false), CreatorUserId = table.Column<string>(nullable: false), CreatorUserName = table.Column<string>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamineDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocAttachments",
                columns: table => new
                { Id = table.Column<Guid>(nullable: false), Type = table.Column<int>(nullable: false), Name = table.Column<string>(maxLength: 200, nullable: false), FileType = table.Column<int>(nullable: true), FileSize = table.Column<decimal>(nullable: true), Path = table.Column<string>(maxLength: 500, nullable: false), DocId = table.Column<Guid>(nullable: false), BackUpId = table.Column<Guid>(nullable: true), IsDeleted = table.Column<bool>(nullable: true), CreationTime = table.Column<DateTime>(nullable: true), CreatorUserId = table.Column<string>(nullable: true), LastModificationTime = table.Column<DateTime>(nullable: true), LastModifierUserId = table.Column<string>(nullable: true), DeletionTime = table.Column<DateTime>(nullable: true), DeleterUserId = table.Column<string>(nullable: true) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocAttachments", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Categorys");
            migrationBuilder.DropTable(name: "Clauses");
            migrationBuilder.DropTable(name: "ClauseRevisions");
            migrationBuilder.DropTable(name: "DetpClauses");
            migrationBuilder.DropTable(name: "DocAttachments");
            migrationBuilder.DropTable(name: "Documents");
            migrationBuilder.DropTable(name: "EmployeeClauses");
            migrationBuilder.DropTable(name: "ExamineDetails");
            migrationBuilder.DropTable(name: "DocAttachments");
        }
    }
}
