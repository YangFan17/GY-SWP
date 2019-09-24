
using AutoMapper;
using GYSWP.LC_KyjMonthMaintainRecords;
using GYSWP.LC_KyjMonthMaintainRecords.Dtos;

namespace GYSWP.LC_KyjMonthMaintainRecords.Mapper
{

	/// <summary>
    /// 配置LC_KyjMonthMaintainRecord的AutoMapper
    /// </summary>
	internal static class LC_KyjMonthMaintainRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_KyjMonthMaintainRecord,LC_KyjMonthMaintainRecordListDto>();
            configuration.CreateMap <LC_KyjMonthMaintainRecordListDto,LC_KyjMonthMaintainRecord>();

            configuration.CreateMap <LC_KyjMonthMaintainRecordEditDto,LC_KyjMonthMaintainRecord>();
            configuration.CreateMap <LC_KyjMonthMaintainRecord,LC_KyjMonthMaintainRecordEditDto>();

        }
	}
}
