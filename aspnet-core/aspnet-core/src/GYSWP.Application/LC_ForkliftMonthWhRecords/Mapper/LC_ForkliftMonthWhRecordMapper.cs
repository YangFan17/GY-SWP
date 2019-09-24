
using AutoMapper;
using GYSWP.LC_ForkliftMonthWhRecords;
using GYSWP.LC_ForkliftMonthWhRecords.Dtos;

namespace GYSWP.LC_ForkliftMonthWhRecords.Mapper
{

	/// <summary>
    /// 配置LC_ForkliftMonthWhRecord的AutoMapper
    /// </summary>
	internal static class LC_ForkliftMonthWhRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_ForkliftMonthWhRecord,LC_ForkliftMonthWhRecordListDto>();
            configuration.CreateMap <LC_ForkliftMonthWhRecordListDto,LC_ForkliftMonthWhRecord>();

            configuration.CreateMap <LC_ForkliftMonthWhRecordEditDto,LC_ForkliftMonthWhRecord>();
            configuration.CreateMap <LC_ForkliftMonthWhRecord,LC_ForkliftMonthWhRecordEditDto>();

        }
	}
}
