
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.GYEnums;
using GYSWP.IndicatorLibrarys;

namespace GYSWP.IndicatorLibrarys.Dtos
{
    public class GetIndicatorLibrarysInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
