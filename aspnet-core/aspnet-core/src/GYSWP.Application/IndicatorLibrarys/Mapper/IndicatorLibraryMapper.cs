
using AutoMapper;
using GYSWP.IndicatorLibrarys;
using GYSWP.IndicatorLibrarys.Dtos;

namespace GYSWP.IndicatorLibrarys.Mapper
{

	/// <summary>
    /// 配置IndicatorLibrary的AutoMapper
    /// </summary>
	internal static class IndicatorLibraryMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <IndicatorLibrary,IndicatorLibraryListDto>();
            configuration.CreateMap <IndicatorLibraryListDto,IndicatorLibrary>();

            configuration.CreateMap <IndicatorLibraryEditDto,IndicatorLibrary>();
            configuration.CreateMap <IndicatorLibrary,IndicatorLibraryEditDto>();

        }
	}
}
