

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.EmployeeClauses;

namespace GYSWP.EntityMapper.EmployeeClauses
{
    public class EmployeeClauseCfg : IEntityTypeConfiguration<EmployeeClause>
    {
        public void Configure(EntityTypeBuilder<EmployeeClause> builder)
        {

            builder.ToTable("EmployeeClauses", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.ClauseId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
        }
    }
}


