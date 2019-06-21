

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.DocRevisions;

namespace GYSWP.DocRevisions.Dtos
{
    public class CreateOrUpdateDocRevisionInput
    {
        [Required]
        public DocRevisionEditDto DocRevision { get; set; }

    }
}