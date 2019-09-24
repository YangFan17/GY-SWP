
using AutoMapper;
using GYSWP.LC_SsjWeekWhByRecords;
using GYSWP.LC_SsjWeekWhByRecords.Dtos;

namespace GYSWP.LC_SsjWeekWhByRecords.Mapper
{

	/// <summary>
    /// 配置LC_SsjWeekWhByRecord的AutoMapper
    /// </summary>
	internal static class LC_SsjWeekWhByRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_SsjWeekWhByRecord,LC_SsjWeekWhByRecordListDto>();
            configuration.CreateMap <LC_SsjWeekWhByRecordListDto,LC_SsjWeekWhByRecord>();

            configuration.CreateMap <LC_SsjWeekWhByRecordEditDto,LC_SsjWeekWhByRecord>();
            configuration.CreateMap <LC_SsjWeekWhByRecord,LC_SsjWeekWhByRecordEditDto>();

        }
	}
}
