

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.Categorys;

namespace GYSWP.Categorys.Dtos
{
    public class CreateOrUpdateCategoryInput
    {
        [Required]
        public CategoryEditDto Category { get; set; }

    }
}