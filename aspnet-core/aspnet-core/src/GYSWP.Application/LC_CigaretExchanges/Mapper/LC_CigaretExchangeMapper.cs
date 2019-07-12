
using AutoMapper;
using GYSWP.LC_CigaretExchanges;
using GYSWP.LC_CigaretExchanges.Dtos;

namespace GYSWP.LC_CigaretExchanges.Mapper
{

	/// <summary>
    /// 配置LC_CigaretExchange的AutoMapper
    /// </summary>
	internal static class LC_CigaretExchangeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_CigaretExchange,LC_CigaretExchangeListDto>();
            configuration.CreateMap <LC_CigaretExchangeListDto,LC_CigaretExchange>();

            configuration.CreateMap <LC_CigaretExchangeEditDto,LC_CigaretExchange>();
            configuration.CreateMap <LC_CigaretExchange,LC_CigaretExchangeEditDto>();

        }
	}
}
