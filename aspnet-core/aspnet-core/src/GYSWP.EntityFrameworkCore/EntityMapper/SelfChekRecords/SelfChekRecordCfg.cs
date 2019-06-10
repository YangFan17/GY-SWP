

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.SelfChekRecords;

namespace GYSWP.EntityMapper.SelfChekRecords
{
    public class SelfChekRecordCfg : IEntityTypeConfiguration<SelfChekRecord>
    {
        public void Configure(EntityTypeBuilder<SelfChekRecord> builder)
        {

            builder.ToTable("SelfChekRecords", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.ClauseId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.IsTodayFirst).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


