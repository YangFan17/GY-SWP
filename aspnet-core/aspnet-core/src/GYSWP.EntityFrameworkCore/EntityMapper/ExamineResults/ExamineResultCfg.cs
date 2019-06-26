

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GYSWP.ExamineResults;

namespace GYSWP.EntityMapper.ExamineResults
{
    public class ExamineResultCfg : IEntityTypeConfiguration<ExamineResult>
    {
        public void Configure(EntityTypeBuilder<ExamineResult> builder)
        {

            builder.ToTable("ExamineResults", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.ExamineDetailId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Content).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EmployeeName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


