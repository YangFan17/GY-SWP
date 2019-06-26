

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.ExamineDetails;

namespace GYSWP.ExamineDetails.Dtos
{
    public class CreateOrUpdateExamineDetailInput
    {
        [Required]
        public ExamineDetailEditDto ExamineDetail { get; set; }

    }
}