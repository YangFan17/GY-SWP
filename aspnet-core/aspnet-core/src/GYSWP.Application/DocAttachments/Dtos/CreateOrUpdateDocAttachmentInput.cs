

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.DocAttachments;

namespace GYSWP.DocAttachments.Dtos
{
    public class CreateOrUpdateDocAttachmentInput
    {
        [Required]
        public DocAttachmentEditDto DocAttachment { get; set; }

    }
}