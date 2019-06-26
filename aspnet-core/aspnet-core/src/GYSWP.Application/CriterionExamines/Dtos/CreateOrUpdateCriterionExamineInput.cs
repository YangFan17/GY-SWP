

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.CriterionExamines;

namespace GYSWP.CriterionExamines.Dtos
{
    public class CreateOrUpdateCriterionExamineInput
    {
        [Required]
        public CriterionExamineEditDto CriterionExamine { get; set; }

    }
}