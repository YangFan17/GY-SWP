

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.ClauseRevisions;

namespace GYSWP.ClauseRevisions.Dtos
{
    public class CreateOrUpdateClauseRevisionInput
    {
        [Required]
        public ClauseRevisionEditDto ClauseRevision { get; set; }

    }
}