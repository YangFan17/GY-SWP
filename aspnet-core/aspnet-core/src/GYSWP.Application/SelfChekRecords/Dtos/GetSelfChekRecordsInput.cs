
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.SelfChekRecords;
using System;

namespace GYSWP.SelfChekRecords.Dtos
{
    public class GetSelfChekRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid DocId { get; set; }
        public Guid ClauseId { get; set; }
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
