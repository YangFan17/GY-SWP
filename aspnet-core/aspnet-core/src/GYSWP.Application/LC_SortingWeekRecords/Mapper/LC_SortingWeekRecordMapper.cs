
using AutoMapper;
using GYSWP.LC_SortingWeekRecords;
using GYSWP.LC_SortingWeekRecords.Dtos;

namespace GYSWP.LC_SortingWeekRecords.Mapper
{

	/// <summary>
    /// 配置LC_SortingWeekRecord的AutoMapper
    /// </summary>
	internal static class LC_SortingWeekRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_SortingWeekRecord,LC_SortingWeekRecordListDto>();
            configuration.CreateMap <LC_SortingWeekRecordListDto,LC_SortingWeekRecord>();

            configuration.CreateMap <LC_SortingWeekRecordEditDto,LC_SortingWeekRecord>();
            configuration.CreateMap <LC_SortingWeekRecord,LC_SortingWeekRecordEditDto>();

        }
	}
}
