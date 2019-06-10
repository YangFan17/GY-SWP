

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.Employees;

namespace GYSWP.Employees.Dtos
{
    public class CreateOrUpdateEmployeeInput
    {
        [Required]
        public EmployeeEditDto Employee { get; set; }

    }
}