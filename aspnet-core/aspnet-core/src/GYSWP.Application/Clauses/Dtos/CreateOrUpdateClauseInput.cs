

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.Clauses;
using GYSWP.DocAttachments.Dtos;

namespace GYSWP.Clauses.Dtos
{
    public class CreateOrUpdateClauseInput
    {
        [Required]
        public ClauseEditDto Clause { get; set; }
        public List<DocAttachmentEditDto> DocAttachment { get; set; }
    }
}