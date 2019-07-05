

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.Advises;

namespace GYSWP.Advises.Dtos
{
    public class CreateOrUpdateAdviseInput
    {
        [Required]
        public AdviseEditDto Advise { get; set; }

    }
}