
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.ExamineResults;
using System;

namespace GYSWP.ExamineResults.Dtos
{
    public class GetExamineResultsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid Id { get; set; }
        public Guid ExamineDetailId { get; set; }
        public string FailReason { get; set; }

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
