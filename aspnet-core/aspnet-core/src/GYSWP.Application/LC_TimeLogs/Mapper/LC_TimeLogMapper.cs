
using AutoMapper;
using GYSWP.LC_TimeLogs;
using GYSWP.LC_TimeLogs.Dtos;

namespace GYSWP.LC_TimeLogs.Mapper
{

	/// <summary>
    /// 配置LC_TimeLog的AutoMapper
    /// </summary>
	internal static class LC_TimeLogMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_TimeLog,LC_TimeLogListDto>();
            configuration.CreateMap <LC_TimeLogListDto,LC_TimeLog>();

            configuration.CreateMap <LC_TimeLogEditDto,LC_TimeLog>();
            configuration.CreateMap <LC_TimeLog,LC_TimeLogEditDto>();

        }
	}
}
