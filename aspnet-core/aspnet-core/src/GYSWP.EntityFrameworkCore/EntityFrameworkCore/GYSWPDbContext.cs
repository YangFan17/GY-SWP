using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using GYSWP.Authorization.Roles;
using GYSWP.Authorization.Users;
using GYSWP.MultiTenancy;
using GYSWP.Employees;
using GYSWP.Organizations;
using GYSWP.SystemDatas;
using GYSWP.Categorys;
using GYSWP.Documents;
using GYSWP.Clauses;
using GYSWP.DocAttachments;
using GYSWP.SelfChekRecords;
using GYSWP.EmployeeClauses;
using GYSWP.ApplyInfos;
using GYSWP.ClauseRevisions;
using GYSWP.DocRevisions;
using GYSWP.CriterionExamines;
using GYSWP.ExamineDetails;
using GYSWP.ExamineResults;
using GYSWP.ExamineFeedbacks;
using GYSWP.Advises;
using GYSWP.Indicators;
using GYSWP.IndicatorsDetails;
using GYSWP.LC_ConveyorChecks;
using GYSWP.LC_ForkliftChecks;
using GYSWP.LC_InStorageBills;
using GYSWP.LC_InStorageRecords;
using GYSWP.LC_QualityRecords;
using GYSWP.LC_TimeLogs;
using GYSWP.EntryExitRegistrations;
using GYSWP.InspectionRecords;
using GYSWP.SCInventoryRecords;
using GYSWP.LC_CigaretExchanges;
using GYSWP.LC_OutScanRecords;
using GYSWP.LC_SortingEquipChecks;
using GYSWP.LC_TeamSafetyActivitys;
using GYSWP.LC_UseOutStorages;
using GYSWP.LC_ScanRecords;
using GYSWP.PositionInfos;
using GYSWP.MainPointsRecords;
using GYSWP.LC_MildewSummers;
using GYSWP.LC_WarningReports;
using GYSWP.IndicatorLibrarys;
using GYSWP.LC_ForkliftMonthWhRecords;
using GYSWP.LC_ForkliftWeekWhRecords;
using GYSWP.LC_KyjFunctionRecords;
using GYSWP.LC_KyjMonthMaintainRecords;
using GYSWP.LC_KyjWeekMaintainRecords;
using GYSWP.LC_LPFunctionRecords;
using GYSWP.LC_LPMaintainRecords;
using GYSWP.LC_SortingMonthRecords;
using GYSWP.LC_SortingWeekRecords;
using GYSWP.LC_SsjMonthWhByRecords;
using GYSWP.LC_SsjWeekWhByRecords;

namespace GYSWP.EntityFrameworkCore
{
    public class GYSWPDbContext : AbpZeroDbContext<Tenant, Role, User, GYSWPDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public GYSWPDbContext(DbContextOptions<GYSWPDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<SystemData> SystemDatas { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Clause> Clauses { get; set; }
        public virtual DbSet<DocAttachment> DocAttachments { get; set; }
        public virtual DbSet<SelfChekRecord> SelfChekRecords { get; set; }
        public virtual DbSet<EmployeeClause> EmployeeClauses { get; set; }
        public virtual DbSet<ApplyInfo> ApplyInfos { get; set; }
        public virtual DbSet<ClauseRevision> ClauseRevisions { get; set; }
        public virtual DbSet<DocRevision> DocRevisions { get; set; }
        public virtual DbSet<CriterionExamine> CriterionExamines { get; set; }
        public virtual DbSet<ExamineDetail> ExamineDetails { get; set; }
        public virtual DbSet<ExamineResult> ExamineResults { get; set; }
        public virtual DbSet<ExamineFeedback> ExamineFeedbacks { get; set; }
        public virtual DbSet<Advise> Advises { get; set; }
        public virtual DbSet<Indicator> Indicators { get; set; }
        public virtual DbSet<IndicatorsDetail> IndicatorsDetails { get; set; }
        public virtual DbSet<PositionInfo> PositionInfos { get; set; }
        public virtual DbSet<MainPointsRecord> MainPointsRecords { get; set; }
        public virtual DbSet<IndicatorLibrary> IndicatorLibrarys { get; set; }
        public virtual DbSet<EntryExitRegistration> LC_EntryExitRegistrations { get; set; }
        public virtual DbSet<InspectionRecord> LC_InspectionRecords { get; set; }
        public virtual DbSet<SCInventoryRecord> LC_SCInventoryRecords { get; set; }
        public virtual DbSet<LC_ConveyorCheck> LC_ConveyorChecks { get; set; }
        public virtual DbSet<LC_ForkliftCheck> LC_ForkliftChecks { get; set; }
        public virtual DbSet<LC_InStorageBill> LC_InStorageBills { get; set; }
        public virtual DbSet<LC_InStorageRecord> LC_InStorageRecords { get; set; }
        public virtual DbSet<LC_QualityRecord> LC_QualityRecords { get; set; }
        public virtual DbSet<LC_TimeLog> LC_TimeLogs { get; set; }
        public virtual DbSet<EntryExitRegistration> EntryExitRegistrations { get; set; }
        public virtual DbSet<InspectionRecord> InspectionRecords { get; set; }
        public virtual DbSet<SCInventoryRecord> SCInventoryRecords { get; set; }
        public virtual DbSet<LC_CigaretExchange> LC_CigaretExchanges { get; set; }
        public virtual DbSet<LC_OutScanRecord> LC_OutScanRecords { get; set; }
        public virtual DbSet<LC_SortingEquipCheck> LC_SortingEquipChecks { get; set; }
        public virtual DbSet<LC_TeamSafetyActivity> LC_TeamSafetyActivitys { get; set; }
        public virtual DbSet<LC_UseOutStorage> LC_UseOutStorages { get; set; }
        public virtual DbSet<LC_ScanRecord> LC_ScanRecords { get; set; }
        public virtual DbSet<LC_MildewSummer> LC_MildewSummers { get; set; }
        public virtual DbSet<LC_Attachment> LC_Attachments { get; set; }
        public virtual DbSet<LC_WarningReport> LC_WarningReports { get; set; }

        public virtual DbSet<LC_ForkliftMonthWhRecord> LC_ForkliftMonthWhRecords { get; set; }
        public virtual DbSet<LC_ForkliftWeekWhRecord> LC_ForkliftWeekWhRecords { get; set; }
        public virtual DbSet<LC_KyjFunctionRecord> LC_KyjFunctionRecords { get; set; }
        public virtual DbSet<LC_KyjMonthMaintainRecord> LC_KyjMonthMaintainRecords { get; set; }
        public virtual DbSet<LC_KyjWeekMaintainRecord> LC_KyjWeekMaintainRecords { get; set; }
        public virtual DbSet<LC_LPFunctionRecord> LC_LPFunctionRecords { get; set; }
        public virtual DbSet<LC_LPMaintainRecord> LC_LPMaintainRecords { get; set; }
        public virtual DbSet<LC_SortingMonthRecord> LC_SortingMonthRecords { get; set; }
        public virtual DbSet<LC_SortingWeekRecord> LC_SortingWeekRecords { get; set; }
        public virtual DbSet<LC_SsjMonthWhByRecord> LC_SsjMonthWhByRecords { get; set; }
        public virtual DbSet<LC_SsjWeekWhByRecord> LC_SsjWeekWhByRecords { get; set; }

    }
}
