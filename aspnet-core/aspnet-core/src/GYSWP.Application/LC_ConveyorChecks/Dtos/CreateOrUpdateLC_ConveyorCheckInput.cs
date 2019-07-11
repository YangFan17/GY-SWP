

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_ConveyorChecks;

namespace GYSWP.LC_ConveyorChecks.Dtos
{
    public class CreateOrUpdateLC_ConveyorCheckInput
    {
        [Required]
        public LC_ConveyorCheckEditDto LC_ConveyorCheck { get; set; }

    }
}