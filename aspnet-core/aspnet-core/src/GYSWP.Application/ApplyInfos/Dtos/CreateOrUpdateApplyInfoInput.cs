

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.ApplyInfos;

namespace GYSWP.ApplyInfos.Dtos
{
    public class CreateOrUpdateApplyInfoInput
    {
        [Required]
        public ApplyInfoEditDto ApplyInfo { get; set; }

    }
}