

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_SortingEquipChecks;

namespace GYSWP.LC_SortingEquipChecks.Dtos
{
    public class CreateOrUpdateLC_SortingEquipCheckInput
    {
        [Required]
        public LC_SortingEquipCheckEditDto LC_SortingEquipCheck { get; set; }

    }
}