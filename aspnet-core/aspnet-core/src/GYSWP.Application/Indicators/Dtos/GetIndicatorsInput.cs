
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Indicators;

namespace GYSWP.Indicators.Dtos
{
    public class GetIndicatorsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
