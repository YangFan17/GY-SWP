using System;
using GYSWP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYSWP.Migrations
{
    [DbContext(typeof(GYSWPDbContext))]
    [Migration("20190712_SordAndOutBLL")]
    partial class SordAndOutBLL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("GYSWP.LC_CigaretExchanges.LC_CigaretExchange", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<string>("OriginPlace").HasMaxLength(100); b.Property<string>("Name").HasMaxLength(100); b.Property<string>("Unit").HasMaxLength(100); b.Property<int?>("Num"); b.Property<string>("Reason").HasMaxLength(2000); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_CigaretExchanges");
            }); modelBuilder.Entity("GYSWP.LC_OutScanRecords.LC_OutScanRecord", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<int?>("OrderNum"); b.Property<int?>("ExpectedScanNum"); b.Property<int?>("AcutalScanNum"); b.Property<int?>("AloneNotScanNum"); b.Property<string>("Remark").HasMaxLength(2000); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_OutScanRecords");
            }); modelBuilder.Entity("GYSWP.LC_SortingEquipChecks.LC_SortingEquipCheck", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<string>("ResponsibleName").HasMaxLength(100); b.Property<string>("SupervisorName").HasMaxLength(50); b.Property<bool>("IsChainPlateOk").IsRequired(); b.Property<bool>("IsControlSwitchOk").IsRequired(); b.Property<bool>("IsElcOrGasBad").IsRequired(); b.Property<bool>("IsLiftUp").IsRequired(); b.Property<bool>("IsSortSysOk").IsRequired(); b.Property<bool>("IsChanDirty").IsRequired(); b.Property<bool>("IsCutSealDirty").IsRequired(); b.Property<bool>("IsBZJControlSwitchOk").IsRequired(); b.Property<bool>("IsBZJElcOrGasBad").IsRequired(); b.Property<bool>("IsTempOk").IsRequired(); b.Property<bool>("IsBZJSysOk").IsRequired(); b.Property<bool>("IsStoveOk").IsRequired(); b.Property<bool>("IsLabelingOk").IsRequired(); b.Property<bool>("IsTBJElcOrGasBad").IsRequired(); b.Property<bool>("IsLaserShieldOk").IsRequired(); b.Property<bool>("IsLineOrMachineOk").IsRequired(); b.Property<bool>("IsCigaretteHouseOk").IsRequired(); b.Property<bool>("IsSingleOk").IsRequired(); b.Property<bool>("IsMainLineOk").IsRequired(); b.Property<bool>("IsCoderOk").IsRequired(); b.Property<bool?>("IsBZJWorkOk"); b.Property<bool>("IsBeltDeviation").IsRequired(); b.Property<bool>("IsFBJOk").IsRequired(); b.Property<bool>("IsTBJOk").IsRequired(); b.Property<bool>("IsSysOutOk").IsRequired(); b.Property<bool>("IsShutElcOrGas").IsRequired(); b.Property<bool>("IsDataCallback").IsRequired(); b.Property<bool>("IsMachineClean").IsRequired(); b.Property<string>("Troubleshooting").HasMaxLength(2000); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_SortingEquipChecks");
            }); modelBuilder.Entity("GYSWP.LC_TeamSafetyActivitys.LC_TeamSafetyActivity", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<string>("SafetyMeeting").HasMaxLength(500); b.Property<bool>("IsSafeEquipOk").IsRequired(); b.Property<bool>("IsEmpHealth").IsRequired(); b.Property<bool>("IsTdjOrLsjOk").IsRequired(); b.Property<bool>("IsAisleOk").IsRequired(); b.Property<bool>("IsExitBad").IsRequired(); b.Property<bool>("IsFireEquipBad").IsRequired(); b.Property<bool>("IsSafeMarkClean").IsRequired(); b.Property<bool>("IsSafeMarkFall").IsRequired(); b.Property<string>("EmpSafeAdvice"); b.Property<int?>("CommonCigaretNum"); b.Property<int?>("ShapedCigaretNum"); b.Property<DateTime?>("BeginSortTime"); b.Property<DateTime?>("EndSortTime"); b.Property<DateTime?>("NormalStopTime"); b.Property<DateTime?>("AbnormalStopTime"); b.Property<bool>("IsNotDanger").IsRequired(); b.Property<bool>("IsOtherAdmittance").IsRequired(); b.Property<bool>("IsViolation").IsRequired(); b.Property<bool>("IsElcOrGasShut").IsRequired(); b.Property<bool>("IsCloseWindow").IsRequired(); b.Property<string>("SafeSupervision").HasMaxLength(500); b.Property<string>("ResponsibleName").HasMaxLength(500); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_TeamSafetyActivitys");
            }); modelBuilder.Entity("GYSWP.LC_UseOutStorages.LC_UseOutStorage", b =>
            {
                b.Property<Guid>("Id")
.ValueGeneratedOnAdd(); b.Property<Guid>("TimeLogId").IsRequired(); b.Property<string>("SortLineName").HasMaxLength(200); b.Property<string>("ProductName").HasMaxLength(100); b.Property<int?>("PreDiffNum"); b.Property<int?>("PreAloneNum"); b.Property<int?>("SupWholeNum"); b.Property<int?>("SupAllPieceNum"); b.Property<int?>("SupAllItemNum"); b.Property<int?>("AcutalOrderNum"); b.Property<int?>("CheckNum"); b.Property<int?>("CheckAloneNum"); b.Property<string>("ClerkName").HasMaxLength(50); b.Property<string>("SortorName").HasMaxLength(50); b.Property<string>("EmployeeId").IsRequired().HasMaxLength(200); b.Property<string>("EmployeeName").HasMaxLength(50); b.Property<DateTime>("CreationTime").IsRequired(); b.HasKey("Id");

                //b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                b.ToTable("LC_UseOutStorages");
            });
        }
    }
}