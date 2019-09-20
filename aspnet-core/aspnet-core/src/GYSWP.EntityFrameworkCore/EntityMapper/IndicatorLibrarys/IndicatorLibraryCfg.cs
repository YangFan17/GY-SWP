

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.IndicatorLibrarys;

namespace GYSWP.EntityMapper.IndicatorLibrarys
{
    public class IndicatorLibraryCfg : IEntityTypeConfiguration<IndicatorLibrary>
    {
        public void Configure(EntityTypeBuilder<IndicatorLibrary> builder)
        {

            builder.ToTable("IndicatorLibrarys", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.Title).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Paraphrase).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.MeasuringWay).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CycleTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.SourceDocId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DeptIds).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DeptNames).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


