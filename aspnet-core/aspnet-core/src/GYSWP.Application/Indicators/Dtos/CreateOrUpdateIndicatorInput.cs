

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.Indicators;

namespace GYSWP.Indicators.Dtos
{
    public class CreateOrUpdateIndicatorInput
    {
        [Required]
        public IndicatorEditDto Indicator { get; set; }
        public List<DeptInfo> DeptInfo { get; set; }
    }
    public class DeptInfo
    {
        public long DeptId { get; set; }
        public string DeptName { get; set; }
    }
}