
using AutoMapper;
using GYSWP.SCInventoryRecords;
using GYSWP.SCInventoryRecords.Dtos;

namespace GYSWP.SCInventoryRecords.Mapper
{

	/// <summary>
    /// 配置SCInventoryRecord的AutoMapper
    /// </summary>
	internal static class SCInventoryRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <SCInventoryRecord,SCInventoryRecordListDto>();
            configuration.CreateMap <SCInventoryRecordListDto,SCInventoryRecord>();

            configuration.CreateMap <SCInventoryRecordEditDto,SCInventoryRecord>();
            configuration.CreateMap <SCInventoryRecord,SCInventoryRecordEditDto>();

        }
	}
}
