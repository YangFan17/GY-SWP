

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_CigaretExchanges;

namespace GYSWP.LC_CigaretExchanges.Dtos
{
    public class CreateOrUpdateLC_CigaretExchangeInput
    {
        [Required]
        public LC_CigaretExchangeEditDto LC_CigaretExchange { get; set; }

    }
}