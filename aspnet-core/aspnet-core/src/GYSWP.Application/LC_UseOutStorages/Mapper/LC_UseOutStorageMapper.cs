
using AutoMapper;
using GYSWP.LC_UseOutStorages;
using GYSWP.LC_UseOutStorages.Dtos;

namespace GYSWP.LC_UseOutStorages.Mapper
{

	/// <summary>
    /// 配置LC_UseOutStorage的AutoMapper
    /// </summary>
	internal static class LC_UseOutStorageMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_UseOutStorage,LC_UseOutStorageListDto>();
            configuration.CreateMap <LC_UseOutStorageListDto,LC_UseOutStorage>();

            configuration.CreateMap <LC_UseOutStorageEditDto,LC_UseOutStorage>();
            configuration.CreateMap <LC_UseOutStorage,LC_UseOutStorageEditDto>();

        }
	}
}
