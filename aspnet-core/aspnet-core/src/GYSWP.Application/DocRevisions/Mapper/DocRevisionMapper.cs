
using AutoMapper;
using GYSWP.DocRevisions;
using GYSWP.DocRevisions.Dtos;

namespace GYSWP.DocRevisions.Mapper
{

	/// <summary>
    /// 配置DocRevision的AutoMapper
    /// </summary>
	internal static class DocRevisionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <DocRevision,DocRevisionListDto>();
            configuration.CreateMap <DocRevisionListDto,DocRevision>();

            configuration.CreateMap <DocRevisionEditDto,DocRevision>();
            configuration.CreateMap <DocRevision,DocRevisionEditDto>();

        }
	}
}
