
using AutoMapper;
using GYSWP.LC_SsjMonthWhByRecords;
using GYSWP.LC_SsjMonthWhByRecords.Dtos;

namespace GYSWP.LC_SsjMonthWhByRecords.Mapper
{

	/// <summary>
    /// 配置LC_SsjMonthWhByRecord的AutoMapper
    /// </summary>
	internal static class LC_SsjMonthWhByRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_SsjMonthWhByRecord,LC_SsjMonthWhByRecordListDto>();
            configuration.CreateMap <LC_SsjMonthWhByRecordListDto,LC_SsjMonthWhByRecord>();

            configuration.CreateMap <LC_SsjMonthWhByRecordEditDto,LC_SsjMonthWhByRecord>();
            configuration.CreateMap <LC_SsjMonthWhByRecord,LC_SsjMonthWhByRecordEditDto>();

        }
	}
}
