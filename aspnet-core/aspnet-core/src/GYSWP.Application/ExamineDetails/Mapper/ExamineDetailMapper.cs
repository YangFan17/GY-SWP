
using AutoMapper;
using GYSWP.ExamineDetails;
using GYSWP.ExamineDetails.Dtos;

namespace GYSWP.ExamineDetails.Mapper
{

	/// <summary>
    /// 配置ExamineDetail的AutoMapper
    /// </summary>
	internal static class ExamineDetailMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ExamineDetail,ExamineDetailListDto>();
            configuration.CreateMap <ExamineDetailListDto,ExamineDetail>();

            configuration.CreateMap <ExamineDetailEditDto,ExamineDetail>();
            configuration.CreateMap <ExamineDetail,ExamineDetailEditDto>();

        }
	}
}
