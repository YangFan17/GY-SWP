

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GYSWP.EmployeeClauses;

namespace GYSWP.EmployeeClauses.Dtos
{
    public class CreateOrUpdateEmployeeClauseInput
    {
        [Required]
        public EmployeeClauseEditDto EmployeeClause { get; set; }

    }
}