
using AutoMapper;
using GYSWP.LC_KyjFunctionRecords;
using GYSWP.LC_KyjFunctionRecords.Dtos;

namespace GYSWP.LC_KyjFunctionRecords.Mapper
{

	/// <summary>
    /// 配置LC_KyjFunctionRecord的AutoMapper
    /// </summary>
	internal static class LC_KyjFunctionRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_KyjFunctionRecord,LC_KyjFunctionRecordListDto>();
            configuration.CreateMap <LC_KyjFunctionRecordListDto,LC_KyjFunctionRecord>();

            configuration.CreateMap <LC_KyjFunctionRecordEditDto,LC_KyjFunctionRecord>();
            configuration.CreateMap <LC_KyjFunctionRecord,LC_KyjFunctionRecordEditDto>();

        }
	}
}
