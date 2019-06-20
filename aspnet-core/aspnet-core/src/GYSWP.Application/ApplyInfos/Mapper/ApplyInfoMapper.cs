
using AutoMapper;
using GYSWP.ApplyInfos;
using GYSWP.ApplyInfos.Dtos;

namespace GYSWP.ApplyInfos.Mapper
{

	/// <summary>
    /// 配置ApplyInfo的AutoMapper
    /// </summary>
	internal static class ApplyInfoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ApplyInfo,ApplyInfoListDto>();
            configuration.CreateMap <ApplyInfoListDto,ApplyInfo>();

            configuration.CreateMap <ApplyInfoEditDto,ApplyInfo>();
            configuration.CreateMap <ApplyInfo,ApplyInfoEditDto>();

        }
	}
}
