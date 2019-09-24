
using AutoMapper;
using GYSWP.LC_LPMaintainRecords;
using GYSWP.LC_LPMaintainRecords.Dtos;

namespace GYSWP.LC_LPMaintainRecords.Mapper
{

	/// <summary>
    /// 配置LC_LPMaintainRecord的AutoMapper
    /// </summary>
	internal static class LC_LPMaintainRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_LPMaintainRecord,LC_LPMaintainRecordListDto>();
            configuration.CreateMap <LC_LPMaintainRecordListDto,LC_LPMaintainRecord>();

            configuration.CreateMap <LC_LPMaintainRecordEditDto,LC_LPMaintainRecord>();
            configuration.CreateMap <LC_LPMaintainRecord,LC_LPMaintainRecordEditDto>();

        }
	}
}
