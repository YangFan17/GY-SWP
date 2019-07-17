

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.PositionInfos;

namespace GYSWP.PositionInfos.Dtos
{
    public class CreateOrUpdatePositionInfoInput
    {
        [Required]
        public PositionInfoEditDto PositionInfo { get; set; }

    }
}