using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("20190708_IndicatorsBLL")]
    partial class IndicatorsBLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.Indicatorss.Indicator", b =>
           {
               b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("Title").IsRequired().HasMaxLength(200); b.Property<string>("Paraphrase").IsRequired().HasMaxLength(2000); b.Property<string>("MeasuringWay").IsRequired().HasMaxLength(2000); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("CreatorEmpeeId").IsRequired().HasMaxLength(200); b.Property<string>("CreatorEmpName").HasMaxLength(50); b.Property<long>("CreatorDeptId").IsRequired(); b.Property<string>("CreatorDeptName").HasMaxLength(100); b.Property<long>("DeptId").IsRequired(); b.Property<string>("DeptName").HasMaxLength(100); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("Indicators");
           }); modelBuilder.Entity("GYSWP.IndicatorsDetails.IndicatorsDetail", b =>
           {
               b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("IndicatorsId").IsRequired(); b.Property<decimal>("ClauseId").IsRequired(); b.Property<int>("Status").IsRequired(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("IndicatorsDetails");
           });
        }
    }
}
