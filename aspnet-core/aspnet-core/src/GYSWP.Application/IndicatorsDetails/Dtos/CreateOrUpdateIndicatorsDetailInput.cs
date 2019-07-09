

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.IndicatorsDetails;

namespace GYSWP.IndicatorsDetails.Dtos
{
    public class CreateOrUpdateIndicatorsDetailInput
    {
        [Required]
        public IndicatorsDetailEditDto IndicatorsDetail { get; set; }

    }
}