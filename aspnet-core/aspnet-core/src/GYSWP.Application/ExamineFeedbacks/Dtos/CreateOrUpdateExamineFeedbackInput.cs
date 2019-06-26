

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.ExamineFeedbacks;

namespace GYSWP.ExamineFeedbacks.Dtos
{
    public class CreateOrUpdateExamineFeedbackInput
    {
        [Required]
        public ExamineFeedbackEditDto ExamineFeedback { get; set; }

    }
}