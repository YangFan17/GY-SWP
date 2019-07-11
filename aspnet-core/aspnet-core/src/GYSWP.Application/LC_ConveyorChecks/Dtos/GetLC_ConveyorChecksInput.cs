
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_ConveyorChecks;

namespace GYSWP.LC_ConveyorChecks.Dtos
{
    public class GetLC_ConveyorChecksInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
