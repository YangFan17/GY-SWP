
using AutoMapper;
using GYSWP.ClauseRevisions;
using GYSWP.ClauseRevisions.Dtos;

namespace GYSWP.ClauseRevisions.Mapper
{

	/// <summary>
    /// 配置ClauseRevision的AutoMapper
    /// </summary>
	internal static class ClauseRevisionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ClauseRevision,ClauseRevisionListDto>();
            configuration.CreateMap <ClauseRevisionListDto,ClauseRevision>();

            configuration.CreateMap <ClauseRevisionEditDto,ClauseRevision>();
            configuration.CreateMap <ClauseRevision,ClauseRevisionEditDto>();

        }
	}
}
