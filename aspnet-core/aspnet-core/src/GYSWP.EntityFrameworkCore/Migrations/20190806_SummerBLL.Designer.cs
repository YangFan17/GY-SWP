using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("20190806_SummerBLL")]
    partial class SummerBLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.LC_MildewSummers.LC_MildewSummer", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<DateTime>("AMBootTime").IsRequired(); b.Property<decimal?>("AMBootBeforeTmp"); b.Property<decimal?>("AMBootBeforeHum"); b.Property<DateTime?>("AMObservedTime"); b.Property<decimal?>("AMBootingTmp"); b.Property<decimal?>("AMBootingHum"); b.Property<DateTime?>("AMBootAfterTime"); b.Property<decimal?>("AMBootAfterTmp"); b.Property<decimal?>("AMBootAfterHum"); b.Property<DateTime?>("PMBootingTime"); b.Property<decimal?>("PMBootBeforeTmp"); b.Property<decimal?>("PMBootBeforeHum"); b.Property<DateTime?>("PMObservedTime"); b.Property<decimal?>("PMBootingTmp"); b.Property<decimal?>("PMBootingHum"); b.Property<DateTime?>("PMBootAfterTime"); b.Property<decimal?>("PMBootAfterTmp"); b.Property<decimal?>("PMBootAfterHum"); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");
                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");
                b.ToTable("LC_MildewSummers");
            });
        }
    }
}