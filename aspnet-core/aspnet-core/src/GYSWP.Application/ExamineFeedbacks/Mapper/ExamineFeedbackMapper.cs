
using AutoMapper;
using GYSWP.ExamineFeedbacks;
using GYSWP.ExamineFeedbacks.Dtos;

namespace GYSWP.ExamineFeedbacks.Mapper
{

	/// <summary>
    /// 配置ExamineFeedback的AutoMapper
    /// </summary>
	internal static class ExamineFeedbackMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ExamineFeedback,ExamineFeedbackListDto>();
            configuration.CreateMap <ExamineFeedbackListDto,ExamineFeedback>();

            configuration.CreateMap <ExamineFeedbackEditDto,ExamineFeedback>();
            configuration.CreateMap <ExamineFeedback,ExamineFeedbackEditDto>();

        }
	}
}
