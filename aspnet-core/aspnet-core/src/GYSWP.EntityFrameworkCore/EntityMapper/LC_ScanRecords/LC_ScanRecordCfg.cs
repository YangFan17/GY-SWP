

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.LC_ScanRecords;

namespace GYSWP.EntityMapper.LC_ScanRecords
{
    public class LC_ScanRecordCfg : IEntityTypeConfiguration<LC_ScanRecord>
    {
        public void Configure(EntityTypeBuilder<LC_ScanRecord> builder)
        {

            builder.ToTable("LC_ScanRecords", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.TimeLogId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Type).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Status).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


