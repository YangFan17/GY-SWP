
using AutoMapper;
using GYSWP.EmployeeClauses;
using GYSWP.EmployeeClauses.Dtos;

namespace GYSWP.EmployeeClauses.Mapper
{

	/// <summary>
    /// 配置EmployeeClause的AutoMapper
    /// </summary>
	internal static class EmployeeClauseMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <EmployeeClause,EmployeeClauseListDto>();
            configuration.CreateMap <EmployeeClauseListDto,EmployeeClause>();

            configuration.CreateMap <EmployeeClauseEditDto,EmployeeClause>();
            configuration.CreateMap <EmployeeClause,EmployeeClauseEditDto>();

        }
	}
}
