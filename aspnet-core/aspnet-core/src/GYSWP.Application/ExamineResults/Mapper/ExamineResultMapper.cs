
using AutoMapper;
using GYSWP.ExamineResults;
using GYSWP.ExamineResults.Dtos;

namespace GYSWP.ExamineResults.Mapper
{

	/// <summary>
    /// 配置ExamineResult的AutoMapper
    /// </summary>
	internal static class ExamineResultMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ExamineResult,ExamineResultListDto>();
            configuration.CreateMap <ExamineResultListDto,ExamineResult>();

            configuration.CreateMap <ExamineResultEditDto,ExamineResult>();
            configuration.CreateMap <ExamineResult,ExamineResultEditDto>();

        }
	}
}
