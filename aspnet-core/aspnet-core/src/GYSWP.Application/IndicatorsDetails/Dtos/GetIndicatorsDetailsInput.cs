
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.IndicatorsDetails;

namespace GYSWP.IndicatorsDetails.Dtos
{
    public class GetIndicatorsDetailsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
