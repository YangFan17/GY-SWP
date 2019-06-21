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
    }
}
