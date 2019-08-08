
using AutoMapper;
using GYSWP.LC_MildewSummers;
using GYSWP.LC_MildewSummers.Dtos;

namespace GYSWP.LC_MildewSummers.Mapper
{

	/// <summary>
    /// 配置LC_MildewSummer的AutoMapper
    /// </summary>
	internal static class LC_MildewSummerMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_MildewSummer,LC_MildewSummerListDto>();
            configuration.CreateMap <LC_MildewSummerListDto,LC_MildewSummer>();

            configuration.CreateMap <LC_MildewSummerEditDto,LC_MildewSummer>();
            configuration.CreateMap <LC_MildewSummer,LC_MildewSummerEditDto>();

        }
	}
}
