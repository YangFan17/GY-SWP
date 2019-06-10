
using AutoMapper;
using GYSWP.SystemDatas;
using GYSWP.SystemDatas.Dtos;

namespace GYSWP.SystemDatas.Mapper
{

	/// <summary>
    /// 配置SystemData的AutoMapper
    /// </summary>
	internal static class SystemDataMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <SystemData,SystemDataListDto>();
            configuration.CreateMap <SystemDataListDto,SystemData>();

            configuration.CreateMap <SystemDataEditDto,SystemData>();
            configuration.CreateMap <SystemData,SystemDataEditDto>();

        }
	}
}
