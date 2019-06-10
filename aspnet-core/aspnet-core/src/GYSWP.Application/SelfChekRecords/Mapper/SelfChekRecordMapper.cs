
using AutoMapper;
using GYSWP.SelfChekRecords;
using GYSWP.SelfChekRecords.Dtos;

namespace GYSWP.SelfChekRecords.Mapper
{

	/// <summary>
    /// 配置SelfChekRecord的AutoMapper
    /// </summary>
	internal static class SelfChekRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <SelfChekRecord,SelfChekRecordListDto>();
            configuration.CreateMap <SelfChekRecordListDto,SelfChekRecord>();

            configuration.CreateMap <SelfChekRecordEditDto,SelfChekRecord>();
            configuration.CreateMap <SelfChekRecord,SelfChekRecordEditDto>();

        }
	}
}
