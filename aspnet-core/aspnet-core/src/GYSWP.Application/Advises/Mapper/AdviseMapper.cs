
using AutoMapper;
using GYSWP.Advises;
using GYSWP.Advises.Dtos;

namespace GYSWP.Advises.Mapper
{

	/// <summary>
    /// 配置Advise的AutoMapper
    /// </summary>
	internal static class AdviseMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Advise,AdviseListDto>();
            configuration.CreateMap <AdviseListDto,Advise>();

            configuration.CreateMap <AdviseEditDto,Advise>();
            configuration.CreateMap <Advise,AdviseEditDto>();

        }
	}
}
