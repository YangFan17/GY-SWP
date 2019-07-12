
using AutoMapper;
using GYSWP.LC_ScanRecords;
using GYSWP.LC_ScanRecords.Dtos;

namespace GYSWP.LC_ScanRecords.Mapper
{

	/// <summary>
    /// 配置LC_ScanRecord的AutoMapper
    /// </summary>
	internal static class LC_ScanRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_ScanRecord,LC_ScanRecordListDto>();
            configuration.CreateMap <LC_ScanRecordListDto,LC_ScanRecord>();

            configuration.CreateMap <LC_ScanRecordEditDto,LC_ScanRecord>();
            configuration.CreateMap <LC_ScanRecord,LC_ScanRecordEditDto>();

        }
	}
}
