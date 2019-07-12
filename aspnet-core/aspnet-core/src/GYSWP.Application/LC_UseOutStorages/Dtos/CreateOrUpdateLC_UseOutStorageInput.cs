

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_UseOutStorages;

namespace GYSWP.LC_UseOutStorages.Dtos
{
    public class CreateOrUpdateLC_UseOutStorageInput
    {
        [Required]
        public LC_UseOutStorageEditDto LC_UseOutStorage { get; set; }

    }
}