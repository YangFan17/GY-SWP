
using AutoMapper;
using GYSWP.LC_QualityRecords;
using GYSWP.LC_QualityRecords.Dtos;

namespace GYSWP.LC_QualityRecords.Mapper
{

	/// <summary>
    /// 配置LC_QualityRecord的AutoMapper
    /// </summary>
	internal static class LC_QualityRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_QualityRecord,LC_QualityRecordListDto>();
            configuration.CreateMap <LC_QualityRecordListDto,LC_QualityRecord>();

            configuration.CreateMap <LC_QualityRecordEditDto,LC_QualityRecord>();
            configuration.CreateMap <LC_QualityRecord,LC_QualityRecordEditDto>();

        }
	}
}
