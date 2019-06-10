

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.Organizations;

namespace GYSWP.Organizations.Dtos
{
    public class CreateOrUpdateOrganizationInput
    {
        [Required]
        public OrganizationEditDto Organization { get; set; }

    }
}