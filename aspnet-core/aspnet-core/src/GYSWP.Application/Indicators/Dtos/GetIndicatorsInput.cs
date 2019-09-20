
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.GYEnums;
using GYSWP.Indicators;

namespace GYSWP.Indicators.Dtos
{
    public class GetIndicatorsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public CycleTime? CycleTime { get; set; }
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
