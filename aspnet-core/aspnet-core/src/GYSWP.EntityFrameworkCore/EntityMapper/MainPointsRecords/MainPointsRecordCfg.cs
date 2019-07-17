

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.MainPointsRecords;

namespace GYSWP.EntityMapper.MainPointsRecords
{
    public class MainPointsRecordCfg : IEntityTypeConfiguration<MainPointsRecord>
    {
        public void Configure(EntityTypeBuilder<MainPointsRecord> builder)
        {

            builder.ToTable("MainPointsRecords", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.PositionInfoId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DocumentId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.MainPoint).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


