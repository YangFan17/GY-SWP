
using AutoMapper;
using GYSWP.MainPointsRecords;
using GYSWP.PositionInfos.Dtos;

namespace GYSWP.PositionInfos.Mapper
{

	/// <summary>
    /// 配置MainPointsRecord的AutoMapper
    /// </summary>
	internal static class MainPointsRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <MainPointsRecord,MainPointsRecordListDto>();
            configuration.CreateMap <MainPointsRecordListDto,MainPointsRecord>();

            configuration.CreateMap <MainPointsRecordEditDto,MainPointsRecord>();
            configuration.CreateMap <MainPointsRecord,MainPointsRecordEditDto>();

        }
	}
}
