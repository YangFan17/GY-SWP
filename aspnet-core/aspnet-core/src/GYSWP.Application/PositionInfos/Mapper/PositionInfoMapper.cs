
using AutoMapper;
using GYSWP.PositionInfos;
using GYSWP.PositionInfos.Dtos;

namespace GYSWP.PositionInfos.Mapper
{

	/// <summary>
    /// 配置PositionInfo的AutoMapper
    /// </summary>
	internal static class PositionInfoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <PositionInfo,PositionInfoListDto>();
            configuration.CreateMap <PositionInfoListDto,PositionInfo>();

            configuration.CreateMap <PositionInfoEditDto,PositionInfo>();
            configuration.CreateMap <PositionInfo,PositionInfoEditDto>();

        }
	}
}
