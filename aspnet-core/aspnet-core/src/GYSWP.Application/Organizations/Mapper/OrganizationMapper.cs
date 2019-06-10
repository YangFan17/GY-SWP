
using AutoMapper;
using GYSWP.Organizations;
using GYSWP.Organizations.Dtos;

namespace GYSWP.Organizations.Mapper
{

	/// <summary>
    /// 配置Organization的AutoMapper
    /// </summary>
	internal static class OrganizationMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Organization,OrganizationListDto>();
            configuration.CreateMap <OrganizationListDto,Organization>();

            configuration.CreateMap <OrganizationEditDto,Organization>();
            configuration.CreateMap <Organization,OrganizationEditDto>();

        }
	}
}
