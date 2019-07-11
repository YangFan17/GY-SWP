

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.LC_TimeLogs;

namespace GYSWP.EntityMapper.LC_TimeLogs
{
    public class LC_TimeLogCfg : IEntityTypeConfiguration<LC_TimeLog>
    {
        public void Configure(EntityTypeBuilder<LC_TimeLog> builder)
        {

            builder.ToTable("LC_TimeLogs", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.Type).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Status).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


