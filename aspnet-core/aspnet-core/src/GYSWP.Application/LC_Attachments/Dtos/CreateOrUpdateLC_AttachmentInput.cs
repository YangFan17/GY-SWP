

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.DocAttachments;

namespace GYSWP.DocAttachments.Dtos
{
    public class CreateOrUpdateLC_AttachmentInput
    {
        [Required]
        public LC_AttachmentEditDto LC_Attachment { get; set; }

    }
}