
using AutoMapper;
using GYSWP.Clauses;
using GYSWP.Clauses.Dtos;

namespace GYSWP.Clauses.Mapper
{

	/// <summary>
    /// 配置Clause的AutoMapper
    /// </summary>
	internal static class ClauseMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Clause,ClauseListDto>();
            configuration.CreateMap <ClauseListDto,Clause>();

            configuration.CreateMap <ClauseEditDto,Clause>();
            configuration.CreateMap <Clause,ClauseEditDto>();

        }
	}
}
