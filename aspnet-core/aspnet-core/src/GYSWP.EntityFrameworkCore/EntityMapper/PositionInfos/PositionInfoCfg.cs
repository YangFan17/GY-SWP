

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.PositionInfos;

namespace GYSWP.EntityMapper.PositionInfos
{
    public class PositionInfoCfg : IEntityTypeConfiguration<PositionInfo>
    {
        public void Configure(EntityTypeBuilder<PositionInfo> builder)
        {

            builder.ToTable("PositionInfos", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.Position).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Duties).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


