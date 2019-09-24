
using AutoMapper;
using GYSWP.LC_WarningReports;
using GYSWP.LC_WarningReports.Dtos;

namespace GYSWP.LC_WarningReports.Mapper
{

	/// <summary>
    /// 配置LC_WarningReport的AutoMapper
    /// </summary>
	internal static class LC_WarningReportMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_WarningReport,LC_WarningReportListDto>();
            configuration.CreateMap <LC_WarningReportListDto,LC_WarningReport>();

            configuration.CreateMap <LC_WarningReportEditDto,LC_WarningReport>();
            configuration.CreateMap <LC_WarningReport,LC_WarningReportEditDto>();

        }
	}
}
