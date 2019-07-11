

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_TeamSafetyActivitys;

namespace GYSWP.LC_TeamSafetyActivitys.Dtos
{
    public class CreateOrUpdateLC_TeamSafetyActivityInput
    {
        [Required]
        public LC_TeamSafetyActivityEditDto LC_TeamSafetyActivity { get; set; }

    }
}