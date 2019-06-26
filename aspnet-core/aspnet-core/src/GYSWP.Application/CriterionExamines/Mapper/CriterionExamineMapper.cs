
using AutoMapper;
using GYSWP.CriterionExamines;
using GYSWP.CriterionExamines.Dtos;

namespace GYSWP.CriterionExamines.Mapper
{

	/// <summary>
    /// 配置CriterionExamine的AutoMapper
    /// </summary>
	internal static class CriterionExamineMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <CriterionExamine,CriterionExamineListDto>();
            configuration.CreateMap <CriterionExamineListDto,CriterionExamine>();

            configuration.CreateMap <CriterionExamineEditDto,CriterionExamine>();
            configuration.CreateMap <CriterionExamine,CriterionExamineEditDto>();

        }
	}
}
