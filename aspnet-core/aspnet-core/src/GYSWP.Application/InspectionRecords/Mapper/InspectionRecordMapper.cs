
using AutoMapper;
using GYSWP.InspectionRecords;
using GYSWP.InspectionRecords.Dtos;

namespace GYSWP.InspectionRecords.Mapper
{

	/// <summary>
    /// 配置InspectionRecord的AutoMapper
    /// </summary>
	internal static class InspectionRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <InspectionRecord,InspectionRecordListDto>();
            configuration.CreateMap <InspectionRecordListDto,InspectionRecord>();

            configuration.CreateMap <InspectionRecordEditDto,InspectionRecord>();
            configuration.CreateMap <InspectionRecord,InspectionRecordEditDto>();

        }
	}
}
