
using AutoMapper;
using GYSWP.DocAttachments;
using GYSWP.DocAttachments.Dtos;

namespace GYSWP.DocAttachments.Mapper
{

	/// <summary>
    /// 配置DocAttachment的AutoMapper
    /// </summary>
	internal static class DocAttachmentMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <DocAttachment,DocAttachmentListDto>();
            configuration.CreateMap <DocAttachmentListDto,DocAttachment>();

            configuration.CreateMap <DocAttachmentEditDto,DocAttachment>();
            configuration.CreateMap <DocAttachment,DocAttachmentEditDto>();

        }
	}
}
