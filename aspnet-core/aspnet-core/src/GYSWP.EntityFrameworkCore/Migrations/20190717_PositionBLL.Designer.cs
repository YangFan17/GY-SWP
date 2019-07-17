using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("20190717_PositionBLL")]
    partial class PositionBLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.MainPointsRecords.MainPointsRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("PositionInfoId").IsRequired(); b.Property<Guid>("DocumentId").IsRequired(); b.Property<string>("MainPoint").HasMaxLength(2000); b.Property<DateTime?>("CreationTime"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("MainPointsRecords");
            }); modelBuilder.Entity("GYSWP.PositionInfos.PositionInfo", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<string>("Position").IsRequired().HasMaxLength(200); b.Property<string>("Duties").HasMaxLength(500); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(20); b.Property<DateTime?>("CreationTime"); b.Property<string>("CreatorUserId"); b.Property<DateTime?>("LastModificationTime"); b.Property<string>("LastModifierUserId"); b.Property<DateTime?>("DeletionTime"); b.Property<string>("DeleterUserId"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("PositionInfos");
            });
        }
    }
}
