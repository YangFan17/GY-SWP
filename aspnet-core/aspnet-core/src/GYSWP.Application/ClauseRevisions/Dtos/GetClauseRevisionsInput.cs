
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.ClauseRevisions;
using System;

namespace GYSWP.ClauseRevisions.Dtos
{
    public class GetClauseRevisionsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid ApplyInfoId { get; set; }
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
