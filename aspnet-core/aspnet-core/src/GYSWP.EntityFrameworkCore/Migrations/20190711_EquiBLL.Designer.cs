using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("20190711_EquiBLL")]
    partial class EquiBLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.LC_ConveyorChecks.LC_ConveyorCheck", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<string>("EquiNo"); b.Property<string>("ResponsibleName").HasMaxLength(100); b.Property<string>("SupervisorName").HasMaxLength(50); b.Property<DateTime?>("RunTime"); b.Property<DateTime?>("BeginTime"); b.Property<DateTime?>("EndTime"); b.Property<bool>("IsEquiFaceClean").IsRequired(); b.Property<bool>("IsSteadyOk").IsRequired(); b.Property<bool>("IsScrewOk").IsRequired(); b.Property<bool>("IsButtonOk").IsRequired(); b.Property<bool>("IsElcLineBad").IsRequired(); b.Property<bool>("IsBeltSlant").IsRequired(); b.Property<bool>("IsBearingOk").IsRequired(); b.Property<bool>("IsSoundOk").IsRequired(); b.Property<bool>("IsMotor").IsRequired(); b.Property<bool>("IsShutPower").IsRequired(); b.Property<bool>("IsBeltBad").IsRequired(); b.Property<bool>("IsClean").IsRequired(); b.Property<string>("Troubleshooting").HasMaxLength(2000); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_ConveyorChecks");
            }); modelBuilder.Entity("GYSWP.LC_ForkliftChecks.LC_ForkliftCheck", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<string>("EquiNo"); b.Property<string>("ResponsibleName").HasMaxLength(100); b.Property<string>("SupervisorName").HasMaxLength(50); b.Property<DateTime?>("RunTime"); b.Property<DateTime?>("BeginTime"); b.Property<DateTime?>("EndTime"); b.Property<bool>("IslubricatingOk").IsRequired(); b.Property<bool>("IsBatteryBad").IsRequired(); b.Property<bool>("IsTurnOrBreakOk").IsRequired(); b.Property<bool>("IsLightOrHornOk").IsRequired(); b.Property<bool>("IsFullCharged").IsRequired(); b.Property<bool>("IsForkLifhOk").IsRequired(); b.Property<bool>("IsRunFullCharged").IsRequired(); b.Property<bool>("IsRunTurnOrBreakOk").IsRequired(); b.Property<bool>("IsRunLightOrHornOk").IsRequired(); b.Property<bool>("IsRunSoundOk").IsRequired(); b.Property<bool>("IsParkStandard").IsRequired(); b.Property<bool>("IsShutPower").IsRequired(); b.Property<bool>("IsNeedCharge").IsRequired(); b.Property<bool>("IsClean").IsRequired(); b.Property<string>("Troubleshooting").HasMaxLength(2000); b.Property<string>("Troubleshooting").HasMaxLength(2000); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_ForkliftChecks");
            }); modelBuilder.Entity("GYSWP.LC_InStorageBills.LC_InStorageBill", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<bool>("IsYczmBill").IsRequired(); b.Property<bool>("IsJyhtBill").IsRequired(); b.Property<bool>("IsZzsBill").IsRequired(); b.Property<bool>("IsCarSeal").IsRequired(); b.Property<bool>("IsCarElcLock").IsRequired(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_InStorageBills");
            }); modelBuilder.Entity("GYSWP.LC_InStorageRecords.LC_InStorageRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<string>("Name").IsRequired().HasMaxLength(100); b.Property<string>("CarNo").IsRequired().HasMaxLength(100); b.Property<string>("DeliveryUnit").IsRequired().HasMaxLength(100); b.Property<string>("BillNo").IsRequired().HasMaxLength(100); b.Property<int>("ReceivableAmount").IsRequired(); b.Property<int>("ActualAmount").IsRequired(); b.Property<string>("DiffContent").IsRequired().HasMaxLength(200); b.Property<string>("Quality").IsRequired().HasMaxLength(100); b.Property<string>("ReceiverName").IsRequired().HasMaxLength(50); b.Property<string>("Remark").HasMaxLength(500); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_InStorageRecords");
            }); modelBuilder.Entity("GYSWP.LC_QualityRecords.LC_QualityRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<string>("Name").IsRequired().HasMaxLength(100); b.Property<decimal?>("WholesaleAmount"); b.Property<int?>("SaleQuantity"); b.Property<string>("CarNo").HasMaxLength(100); b.Property<decimal?>("CompensationAmount"); b.Property<string>("CarrierName").HasMaxLength(50); b.Property<string>("ClerkName").HasMaxLength(50); b.Property<DateTime?>("HandoverTime"); b.Property<decimal?>("Amount"); b.Property<string>("HandManName").HasMaxLength(50); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_QualityRecords");
            }); modelBuilder.Entity("GYSWP.LC_TimeLogs.LC_TimeLog", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<int>("Type").IsRequired(); b.Property<int>("Status").IsRequired(); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_TimeLogs");
            });
        }
    }
}
