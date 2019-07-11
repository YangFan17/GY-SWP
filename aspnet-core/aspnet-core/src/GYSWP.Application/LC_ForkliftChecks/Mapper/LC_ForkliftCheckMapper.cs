
using AutoMapper;
using GYSWP.LC_ForkliftChecks;
using GYSWP.LC_ForkliftChecks.Dtos;

namespace GYSWP.LC_ForkliftChecks.Mapper
{

	/// <summary>
    /// 配置LC_ForkliftCheck的AutoMapper
    /// </summary>
	internal static class LC_ForkliftCheckMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_ForkliftCheck,LC_ForkliftCheckListDto>();
            configuration.CreateMap <LC_ForkliftCheckListDto,LC_ForkliftCheck>();

            configuration.CreateMap <LC_ForkliftCheckEditDto,LC_ForkliftCheck>();
            configuration.CreateMap <LC_ForkliftCheck,LC_ForkliftCheckEditDto>();

        }
	}
}
