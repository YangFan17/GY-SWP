

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.SystemDatas;

namespace GYSWP.SystemDatas.Dtos
{
    public class CreateOrUpdateSystemDataInput
    {
        [Required]
        public SystemDataEditDto SystemData { get; set; }

    }
}