

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_ForkliftChecks;

namespace GYSWP.LC_ForkliftChecks.Dtos
{
    public class CreateOrUpdateLC_ForkliftCheckInput
    {
        [Required]
        public LC_ForkliftCheckEditDto LC_ForkliftCheck { get; set; }

    }
}