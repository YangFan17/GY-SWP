
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Clauses;
using System;
using GYSWP.GYEnums;

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

    public class ApplyInput {
        public ClauseEditDto Entity { get; set; }
        public RevisionType RevisionType { get; set; }
        public Guid ApplyId { get; set; }
    }

    public class ApplyDeleteInput
    {
        public Guid DocumentId { get; set; }
        public Guid ApplyInfoId { get; set; }
        public Guid Id { get; set; }
    }
}
