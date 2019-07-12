
using AutoMapper;
using GYSWP.LC_TeamSafetyActivitys;
using GYSWP.LC_TeamSafetyActivitys.Dtos;

namespace GYSWP.LC_TeamSafetyActivitys.Mapper
{

	/// <summary>
    /// 配置LC_TeamSafetyActivity的AutoMapper
    /// </summary>
	internal static class LC_TeamSafetyActivityMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_TeamSafetyActivity,LC_TeamSafetyActivityListDto>();
            configuration.CreateMap <LC_TeamSafetyActivityListDto,LC_TeamSafetyActivity>();

            configuration.CreateMap <LC_TeamSafetyActivityEditDto,LC_TeamSafetyActivity>();
            configuration.CreateMap <LC_TeamSafetyActivity,LC_TeamSafetyActivityEditDto>();

        }
	}
}
