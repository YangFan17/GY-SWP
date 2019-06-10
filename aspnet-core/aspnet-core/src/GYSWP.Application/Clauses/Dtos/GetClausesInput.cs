
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Clauses;
using System;

namespace GYSWP.Clauses.Dtos
{
    public class GetClausesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid DocumentId { get; set; }
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
}
