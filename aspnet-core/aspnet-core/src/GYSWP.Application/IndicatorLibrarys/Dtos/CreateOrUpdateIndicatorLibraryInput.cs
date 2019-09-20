

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.IndicatorLibrarys;

namespace GYSWP.IndicatorLibrarys.Dtos
{
    public class CreateOrUpdateIndicatorLibraryInput
    {
        [Required]
        public IndicatorLibraryEditDto IndicatorLibrary { get; set; }

    }
}