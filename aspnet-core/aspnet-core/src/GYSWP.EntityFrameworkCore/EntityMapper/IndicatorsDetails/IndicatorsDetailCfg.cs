

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.IndicatorsDetails;

namespace GYSWP.EntityMapper.IndicatorsDetails
{
    public class IndicatorsDetailCfg : IEntityTypeConfiguration<IndicatorsDetail>
    {
        public void Configure(EntityTypeBuilder<IndicatorsDetail> builder)
        {

            builder.ToTable("IndicatorsDetails", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.IndicatorsId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ActualValue).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Status).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


