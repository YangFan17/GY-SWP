

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.PositionInfos;

namespace GYSWP.PositionInfos.Dtos
{
    public class CreateOrUpdateMainPointsRecordInput
    {
        [Required]
        public MainPointsRecordEditDto MainPointsRecord { get; set; }

    }
}