

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_MildewSummers;

namespace GYSWP.LC_MildewSummers.Dtos
{
    public class CreateOrUpdateLC_MildewSummerInput
    {
        [Required]
        public LC_MildewSummerEditDto LC_MildewSummer { get; set; }

    }
}