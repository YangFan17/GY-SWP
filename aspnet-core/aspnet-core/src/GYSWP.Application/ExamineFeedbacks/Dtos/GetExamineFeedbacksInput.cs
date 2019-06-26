
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.ExamineFeedbacks;

namespace GYSWP.ExamineFeedbacks.Dtos
{
    public class GetExamineFeedbacksInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
