

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_InStorageBills;

namespace GYSWP.LC_InStorageBills.Dtos
{
    public class CreateOrUpdateLC_InStorageBillInput
    {
        [Required]
        public LC_InStorageBillEditDto LC_InStorageBill { get; set; }

    }
}