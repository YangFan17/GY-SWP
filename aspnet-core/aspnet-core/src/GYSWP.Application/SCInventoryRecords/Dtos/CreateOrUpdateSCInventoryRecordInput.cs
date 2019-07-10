

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.SCInventoryRecords;

namespace GYSWP.SCInventoryRecords.Dtos
{
    public class CreateOrUpdateSCInventoryRecordInput
    {
        [Required]
        public SCInventoryRecordEditDto SCInventoryRecord { get; set; }

    }
}