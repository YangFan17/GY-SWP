

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.SelfChekRecords;

namespace GYSWP.SelfChekRecords.Dtos
{
    public class CreateOrUpdateSelfChekRecordInput
    {
        [Required]
        public SelfChekRecordEditDto SelfChekRecord { get; set; }

    }
}