
using AutoMapper;
using GYSWP.EntryExitRegistrations;
using GYSWP.EntryExitRegistrations.Dtos;

namespace GYSWP.EntryExitRegistrations.Mapper
{

	/// <summary>
    /// 配置EntryExitRegistration的AutoMapper
    /// </summary>
	internal static class EntryExitRegistrationMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <EntryExitRegistration,EntryExitRegistrationListDto>();
            configuration.CreateMap <EntryExitRegistrationListDto,EntryExitRegistration>();

            configuration.CreateMap <EntryExitRegistrationEditDto,EntryExitRegistration>();
            configuration.CreateMap <EntryExitRegistration,EntryExitRegistrationEditDto>();

        }
	}
}
