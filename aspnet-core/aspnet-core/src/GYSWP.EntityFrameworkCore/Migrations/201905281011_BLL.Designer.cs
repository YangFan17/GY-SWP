using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("201905281011_BLL")]
    partial class BLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.Categorys.Category", b => {
                b.Property<int>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("Name").IsRequired().HasMaxLength(50); b.Property<int?>("ParentId"); b.Property<string>("Desc").HasMaxLength(500); b.Property<bool?>("IsDeleted"); b.Property<DateTime?>("CreationTime"); b.Property<long?>("CreatorUserId"); b.Property<DateTime?>("LastModificationTime"); b.Property<long?>("LastModifierUserId"); b.Property<DateTime?>("DeletionTime"); b.Property<long?>("DeleterUserId"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("Categorys");
            });

            modelBuilder.Entity("GYSWP.Documents.Document", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("Name").IsRequired().HasMaxLength(200); b.Property<int>("CategoryId").IsRequired(); b.Property<string>("CategoryDesc").HasMaxLength(500); b.Property<string>("DocThum"); b.Property<string>("Summary").HasMaxLength(2000); b.Property<DateTime?>("ReleaseDate"); b.Property<string>("QrCodeUrl").HasMaxLength(200); b.Property<bool?>("IsDeleted"); b.Property<DateTime?>("CreationTime"); b.Property<long?>("CreatorUserId"); b.Property<DateTime?>("LastModificationTime"); b.Property<long?>("LastModifierUserId"); b.Property<DateTime?>("DeletionTime"); b.Property<long?>("DeleterUserId"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("Documents");
            });

            modelBuilder.Entity("GYSWP.DocAttachments.DocAttachment", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("Name").IsRequired().HasMaxLength(200); b.Property<int?>("FileType"); b.Property<decimal?>("FileSize"); b.Property<string>("Path").IsRequired().HasMaxLength(500); b.Property<Guid>("BLLId").IsRequired(); b.Property<Guid?>("BackUpId"); b.Property<bool?>("IsDeleted"); b.Property<DateTime?>("CreationTime"); b.Property<long?>("CreatorUserId"); b.Property<DateTime?>("LastModificationTime"); b.Property<long?>("LastModifierUserId"); b.Property<DateTime?>("DeletionTime"); b.Property<long?>("DeleterUserId"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("DocAttachments");
            }); modelBuilder.Entity("GYSWP.Documents.Document", b => {
                b.Property<long?>("DeleterUserId"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("Documents");
            });
            modelBuilder.Entity("GYSWP.Clauses.Clause", b => {
                b.Property<int>("Id")
.ValueGeneratedOnAdd(); b.Property<int?>("ParentId"); b.Property<Guid?>("DocumentId"); b.Property<DateTime?>("CreationTime"); b.Property<string>("CreatorUserId"); b.Property<DateTime?>("PublishTime"); b.Property<string>("PublishUserId"); b.Property<DateTime?>("LastModificationTime"); b.Property<string>("LastModifierUserId"); b.Property<DateTime?>("DeletionTime"); b.Property<string>("DeleterUserId"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("Clauses");
            });

            modelBuilder.Entity("GYSWP.DetpClauses.DetpClause", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("ClauseId").IsRequired(); b.Property<long>("DeptId").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("DetpClauses");
            });

            modelBuilder.Entity("GYSWP.EmployeeClauses.EmployeeClause", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("DetpClauseId").IsRequired(); b.Property<string>("EmployeeId").IsRequired(); b.Property<bool>("IsSelfCheck").IsRequired(); b.Property<DateTime?>("SelfCheckTime"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("EmployeeClauses");
            });

            modelBuilder.Entity("GYSWP.SelfChekRecords.SelfChekRecord", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("ClauseId").IsRequired(); b.Property<string>("EmployeeId").IsRequired(); b.Property<bool?>("IsTodayFirst"); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("EmployeeName"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("SelfChekRecords");
            });

            modelBuilder.Entity("GYSWP.ClauseRevisions.ClauseRevision", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("ClauseId").IsRequired(); b.Property<string>("Content"); b.Property<string>("PreContent"); b.Property<string>("EmployeeId").IsRequired(); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("ClauseRevisions");
            });

            modelBuilder.Entity("GYSWP.ExamineDetails.ExamineDetail", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<long>("DeptId").IsRequired(); b.Property<Guid>("ClauseId").IsRequired(); b.Property<int>("Score").IsRequired(); b.Property<string>("Remark"); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("CreatorUserId").IsRequired(); b.Property<string>("CreatorUserName"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("ExamineDetails");
            });

            modelBuilder.Entity("GYSWP.ExamineResults.ExamineResult", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("ExamineDetailId").IsRequired(); b.Property<string>("Remark"); b.Property<string>("CreatorUserId").IsRequired(); b.Property<string>("CreatorUserName"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("ExamineResults");
            });
            modelBuilder.Entity("GYSWP.ExamineReviews.ExamineReview", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("ExamineDetailId").IsRequired(); b.Property<int>("ScoreType").IsRequired(); b.Property<string>("ScoreName"); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("CreatorUserId").IsRequired(); b.Property<string>("CreatorUserName"); b.Property<string>("BeCheckUserId").IsRequired(); b.Property<string>("BeCheckUser"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("ExamineReviews");
            });
            modelBuilder.Entity("GYSWP.ExamineFeedbacks.ExamineFeedback", b => {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<int>("Type").IsRequired(); b.Property<Guid>("BusinessId").IsRequired(); b.Property<string>("CourseType").HasMaxLength(50); b.Property<string>("Reason").HasMaxLength(1000); b.Property<string>("Solution").HasMaxLength(1000); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("CreatorUserId").IsRequired(); b.Property<string>("CreatorUserName"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("ExamineFeedbacks");
            });
        }
    }
}
