
using AutoMapper;
using GYSWP.LC_SortingEquipChecks;
using GYSWP.LC_SortingEquipChecks.Dtos;

namespace GYSWP.LC_SortingEquipChecks.Mapper
{

	/// <summary>
    /// 配置LC_SortingEquipCheck的AutoMapper
    /// </summary>
	internal static class LC_SortingEquipCheckMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_SortingEquipCheck,LC_SortingEquipCheckListDto>();
            configuration.CreateMap <LC_SortingEquipCheckListDto,LC_SortingEquipCheck>();

            configuration.CreateMap <LC_SortingEquipCheckEditDto,LC_SortingEquipCheck>();
            configuration.CreateMap <LC_SortingEquipCheck,LC_SortingEquipCheckEditDto>();

        }
	}
}
