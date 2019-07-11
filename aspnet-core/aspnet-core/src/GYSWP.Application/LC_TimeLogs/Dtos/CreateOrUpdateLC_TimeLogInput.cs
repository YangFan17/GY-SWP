

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_TimeLogs;

namespace GYSWP.LC_TimeLogs.Dtos
{
    public class CreateOrUpdateLC_TimeLogInput
    {
        [Required]
        public LC_TimeLogEditDto LC_TimeLog { get; set; }

    }
}