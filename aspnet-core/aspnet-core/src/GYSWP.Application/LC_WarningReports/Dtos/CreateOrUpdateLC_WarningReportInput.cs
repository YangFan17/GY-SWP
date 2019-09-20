

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.LC_WarningReports;

namespace GYSWP.LC_WarningReports.Dtos
{
    public class CreateOrUpdateLC_WarningReportInput
    {
        [Required]
        public LC_WarningReportEditDto LC_WarningReport { get; set; }

    }
}