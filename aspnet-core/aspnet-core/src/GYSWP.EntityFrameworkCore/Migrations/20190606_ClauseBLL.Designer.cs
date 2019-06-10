using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("20190606_ClauseBLL")]
    partial class ClauseBLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.SelfChekRecords.SelfChekRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("ClauseId").IsRequired(); b.Property<string>("EmployeeId").IsRequired(); b.Property<bool?>("IsTodayFirst"); b.Property<DateTime>("CreationTime").IsRequired(); b.Property<string>("EmployeeName"); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("SelfChekRecords");
            });
        }
    }
}
