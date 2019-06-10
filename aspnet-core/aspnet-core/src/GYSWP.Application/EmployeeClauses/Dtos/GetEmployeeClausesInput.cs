
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.EmployeeClauses;
using System;
using System.Collections.Generic;

namespace GYSWP.EmployeeClauses.Dtos
{
    public class GetEmployeeClausesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

    }

    public class ConfirmClauseInput
    {
        public List<Guid> ClauseIds { get; set; }
        public Guid DocId { get; set; }
    }
}
