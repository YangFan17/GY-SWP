
using AutoMapper;
using GYSWP.LC_SortingMonthRecords;
using GYSWP.LC_SortingMonthRecords.Dtos;

namespace GYSWP.LC_SortingMonthRecords.Mapper
{

	/// <summary>
    /// 配置LC_SortingMonthRecord的AutoMapper
    /// </summary>
	internal static class LC_SortingMonthRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_SortingMonthRecord,LC_SortingMonthRecordListDto>();
            configuration.CreateMap <LC_SortingMonthRecordListDto,LC_SortingMonthRecord>();

            configuration.CreateMap <LC_SortingMonthRecordEditDto,LC_SortingMonthRecord>();
            configuration.CreateMap <LC_SortingMonthRecord,LC_SortingMonthRecordEditDto>();

        }
	}
}
