using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("20190626_ExamineBLL")]
    partial class ExamineBLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.CriterionExamines.CriterionExamine", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("Title").IsRequired().HasMaxLength(200); b.Property<int>("Type").IsRequired(); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("CreatorEmpeeId").IsRequired().HasMaxLength(200); b.Property<string>("CreatorEmpName").HasMaxLength(50); b.Property<long>("CreatorDeptId").IsRequired(); b.Property<long>("DeptId").IsRequired(); b.Property<string>("CreatorDeptName").HasMaxLength(100); b.Property<string>("DeptName").HasMaxLength(100); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("CriterionExamines");
            }); modelBuilder.Entity("GYSWP.ExamineDetails.ExamineDetail", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("CriterionExamineId").IsRequired(); b.Property<Guid>("ClauseId").IsRequired(); b.Property<int>("Status").IsRequired(); b.Property<int>("Result").IsRequired(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("CreatorEmpeeId").IsRequired().HasMaxLength(200); b.Property<string>("CreatorEmpName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("ExamineDetails");
            }); modelBuilder.Entity("GYSWP.ExamineFeedbacks.ExamineFeedback", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<int>("Type").IsRequired(); b.Property<Guid>("BusinessId").IsRequired(); b.Property<int>("CourseType").IsRequired(); b.Property<string>("Reason").HasMaxLength(1000); b.Property<string>("Solution").HasMaxLength(1000); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("ExamineFeedbacks");
            }); modelBuilder.Entity("GYSWP.ExamineResults.ExamineResult", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("ExamineDetailId").IsRequired(); b.Property<string>("Content").IsRequired().HasMaxLength(500); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("ExamineResults");
            });
        }
    }
}
