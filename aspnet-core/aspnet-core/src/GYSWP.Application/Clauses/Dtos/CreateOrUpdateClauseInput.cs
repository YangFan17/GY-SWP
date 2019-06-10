

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.Clauses;

namespace GYSWP.Clauses.Dtos
{
    public class CreateOrUpdateClauseInput
    {
        [Required]
        public ClauseEditDto Clause { get; set; }

    }
}