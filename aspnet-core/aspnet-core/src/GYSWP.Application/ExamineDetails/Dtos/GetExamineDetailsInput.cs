
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.ExamineDetails;

namespace GYSWP.ExamineDetails.Dtos
{
    public class GetExamineDetailsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
