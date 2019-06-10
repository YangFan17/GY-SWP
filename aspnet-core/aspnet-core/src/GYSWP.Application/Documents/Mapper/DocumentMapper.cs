
using AutoMapper;
using GYSWP.Documents;
using GYSWP.Documents.Dtos;

namespace GYSWP.Documents.Mapper
{

	/// <summary>
    /// 配置Document的AutoMapper
    /// </summary>
	internal static class DocumentMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Document,DocumentListDto>();
            configuration.CreateMap <DocumentListDto,Document>();

            configuration.CreateMap <DocumentEditDto,Document>();
            configuration.CreateMap <Document,DocumentEditDto>();

        }
	}
}
