
using AutoMapper;
using GYSWP.LC_KyjWeekMaintainRecords;
using GYSWP.LC_KyjWeekMaintainRecords.Dtos;

namespace GYSWP.LC_KyjWeekMaintainRecords.Mapper
{

	/// <summary>
    /// 配置LC_KyjWeekMaintainRecord的AutoMapper
    /// </summary>
	internal static class LC_KyjWeekMaintainRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_KyjWeekMaintainRecord,LC_KyjWeekMaintainRecordListDto>();
            configuration.CreateMap <LC_KyjWeekMaintainRecordListDto,LC_KyjWeekMaintainRecord>();

            configuration.CreateMap <LC_KyjWeekMaintainRecordEditDto,LC_KyjWeekMaintainRecord>();
            configuration.CreateMap <LC_KyjWeekMaintainRecord,LC_KyjWeekMaintainRecordEditDto>();

        }
	}
}
