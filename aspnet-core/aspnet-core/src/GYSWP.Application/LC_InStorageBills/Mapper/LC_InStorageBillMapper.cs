
using AutoMapper;
using GYSWP.LC_InStorageBills;
using GYSWP.LC_InStorageBills.Dtos;

namespace GYSWP.LC_InStorageBills.Mapper
{

	/// <summary>
    /// 配置LC_InStorageBill的AutoMapper
    /// </summary>
	internal static class LC_InStorageBillMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_InStorageBill,LC_InStorageBillListDto>();
            configuration.CreateMap <LC_InStorageBillListDto,LC_InStorageBill>();

            configuration.CreateMap <LC_InStorageBillEditDto,LC_InStorageBill>();
            configuration.CreateMap <LC_InStorageBill,LC_InStorageBillEditDto>();

        }
	}
}
