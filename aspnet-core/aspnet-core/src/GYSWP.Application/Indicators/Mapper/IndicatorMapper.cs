
using AutoMapper;
using GYSWP.Indicators;
using GYSWP.Indicators.Dtos;

namespace GYSWP.Indicators.Mapper
{

	/// <summary>
    /// 配置Indicator的AutoMapper
    /// </summary>
	internal static class IndicatorMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Indicator,IndicatorListDto>();
            configuration.CreateMap <IndicatorListDto,Indicator>();

            configuration.CreateMap <IndicatorEditDto,Indicator>();
            configuration.CreateMap <Indicator,IndicatorEditDto>();

        }
	}
}
