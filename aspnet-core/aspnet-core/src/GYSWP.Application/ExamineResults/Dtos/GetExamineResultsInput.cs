
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.ExamineResults;

namespace GYSWP.ExamineResults.Dtos
{
    public class GetExamineResultsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
}
