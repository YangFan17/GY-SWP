

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.Documents;

namespace GYSWP.Documents.Dtos
{
    public class CreateOrUpdateDocumentInput
    {
        [Required]
        public DocumentEditDto Document { get; set; }

    }
}