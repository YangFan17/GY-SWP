
using AutoMapper;
using GYSWP.LC_LPFunctionRecords;
using GYSWP.LC_LPFunctionRecords.Dtos;

namespace GYSWP.LC_LPFunctionRecords.Mapper
{

	/// <summary>
    /// 配置LC_LPFunctionRecord的AutoMapper
    /// </summary>
	internal static class LC_LPFunctionRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_LPFunctionRecord,LC_LPFunctionRecordListDto>();
            configuration.CreateMap <LC_LPFunctionRecordListDto,LC_LPFunctionRecord>();

            configuration.CreateMap <LC_LPFunctionRecordEditDto,LC_LPFunctionRecord>();
            configuration.CreateMap <LC_LPFunctionRecord,LC_LPFunctionRecordEditDto>();

        }
	}
}
