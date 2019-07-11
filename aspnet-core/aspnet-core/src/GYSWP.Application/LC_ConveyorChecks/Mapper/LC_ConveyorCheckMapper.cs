
using AutoMapper;
using GYSWP.LC_ConveyorChecks;
using GYSWP.LC_ConveyorChecks.Dtos;

namespace GYSWP.LC_ConveyorChecks.Mapper
{

	/// <summary>
    /// 配置LC_ConveyorCheck的AutoMapper
    /// </summary>
	internal static class LC_ConveyorCheckMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_ConveyorCheck,LC_ConveyorCheckListDto>();
            configuration.CreateMap <LC_ConveyorCheckListDto,LC_ConveyorCheck>();

            configuration.CreateMap <LC_ConveyorCheckEditDto,LC_ConveyorCheck>();
            configuration.CreateMap <LC_ConveyorCheck,LC_ConveyorCheckEditDto>();

        }
	}
}
