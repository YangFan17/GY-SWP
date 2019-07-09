
using AutoMapper;
using GYSWP.IndicatorsDetails;
using GYSWP.IndicatorsDetails.Dtos;

namespace GYSWP.IndicatorsDetails.Mapper
{

	/// <summary>
    /// 配置IndicatorsDetail的AutoMapper
    /// </summary>
	internal static class IndicatorsDetailMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <IndicatorsDetail,IndicatorsDetailListDto>();
            configuration.CreateMap <IndicatorsDetailListDto,IndicatorsDetail>();

            configuration.CreateMap <IndicatorsDetailEditDto,IndicatorsDetail>();
            configuration.CreateMap <IndicatorsDetail,IndicatorsDetailEditDto>();

        }
	}
}
