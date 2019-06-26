

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.ExamineResults;

namespace GYSWP.ExamineResults.Dtos
{
    public class CreateOrUpdateExamineResultInput
    {
        [Required]
        public ExamineResultEditDto ExamineResult { get; set; }

    }
}