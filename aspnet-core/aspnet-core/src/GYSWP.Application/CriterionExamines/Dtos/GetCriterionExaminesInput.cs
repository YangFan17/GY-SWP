
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.CriterionExamines;

namespace GYSWP.CriterionExamines.Dtos
{
    public class GetCriterionExaminesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
