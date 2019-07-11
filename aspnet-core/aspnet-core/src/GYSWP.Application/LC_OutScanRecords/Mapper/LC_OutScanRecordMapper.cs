
using AutoMapper;
using GYSWP.LC_OutScanRecords;
using GYSWP.LC_OutScanRecords.Dtos;

namespace GYSWP.LC_OutScanRecords.Mapper
{

	/// <summary>
    /// 配置LC_OutScanRecord的AutoMapper
    /// </summary>
	internal static class LC_OutScanRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_OutScanRecord,LC_OutScanRecordListDto>();
            configuration.CreateMap <LC_OutScanRecordListDto,LC_OutScanRecord>();

            configuration.CreateMap <LC_OutScanRecordEditDto,LC_OutScanRecord>();
            configuration.CreateMap <LC_OutScanRecord,LC_OutScanRecordEditDto>();

        }
	}
}
