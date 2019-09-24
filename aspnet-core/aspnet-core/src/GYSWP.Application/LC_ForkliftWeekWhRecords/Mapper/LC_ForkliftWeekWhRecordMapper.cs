
using AutoMapper;
using GYSWP.LC_ForkliftWeekWhRecords;
using GYSWP.LC_ForkliftWeekWhRecords.Dtos;

namespace GYSWP.LC_ForkliftWeekWhRecords.Mapper
{

	/// <summary>
    /// 配置LC_ForkliftWeekWhRecord的AutoMapper
    /// </summary>
	internal static class LC_ForkliftWeekWhRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_ForkliftWeekWhRecord,LC_ForkliftWeekWhRecordListDto>();
            configuration.CreateMap <LC_ForkliftWeekWhRecordListDto,LC_ForkliftWeekWhRecord>();

            configuration.CreateMap <LC_ForkliftWeekWhRecordEditDto,LC_ForkliftWeekWhRecord>();
            configuration.CreateMap <LC_ForkliftWeekWhRecord,LC_ForkliftWeekWhRecordEditDto>();

        }
	}
}
