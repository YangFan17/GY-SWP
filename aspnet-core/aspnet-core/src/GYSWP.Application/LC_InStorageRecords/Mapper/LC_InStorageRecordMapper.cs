
using AutoMapper;
using GYSWP.LC_InStorageRecords;
using GYSWP.LC_InStorageRecords.Dtos;

namespace GYSWP.LC_InStorageRecords.Mapper
{

	/// <summary>
    /// 配置LC_InStorageRecord的AutoMapper
    /// </summary>
	internal static class LC_InStorageRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_InStorageRecord,LC_InStorageRecordListDto>();
            configuration.CreateMap <LC_InStorageRecordListDto,LC_InStorageRecord>();

            configuration.CreateMap <LC_InStorageRecordEditDto,LC_InStorageRecord>();
            configuration.CreateMap <LC_InStorageRecord,LC_InStorageRecordEditDto>();

        }
	}
}
