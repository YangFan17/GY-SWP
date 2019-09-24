
using AutoMapper;
using GYSWP.DocAttachments;
using GYSWP.DocAttachments.Dtos;

namespace GYSWP.DocAttachments.Mapper
{

	/// <summary>
    /// 配置LC_Attachment的AutoMapper
    /// </summary>
	internal static class LC_AttachmentMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <LC_Attachment,LC_AttachmentListDto>();
            configuration.CreateMap <LC_AttachmentListDto,LC_Attachment>();

            configuration.CreateMap <LC_AttachmentEditDto,LC_Attachment>();
            configuration.CreateMap <LC_Attachment,LC_AttachmentEditDto>();

        }
	}
}
