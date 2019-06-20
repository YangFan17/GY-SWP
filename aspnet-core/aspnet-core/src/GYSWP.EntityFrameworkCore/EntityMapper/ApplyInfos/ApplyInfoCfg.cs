

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.ApplyInfos;

namespace GYSWP.EntityMapper.ApplyInfos
{
    public class ApplyInfoCfg : IEntityTypeConfiguration<ApplyInfo>
    {
        public void Configure(EntityTypeBuilder<ApplyInfo> builder)
        {

            builder.ToTable("ApplyInfos", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.DocumentId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Type).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Status).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.HandleTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Reason).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Content).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ProcessInstanceId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


