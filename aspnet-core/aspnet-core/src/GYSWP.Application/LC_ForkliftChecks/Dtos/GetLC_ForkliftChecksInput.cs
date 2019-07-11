
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_ForkliftChecks;

namespace GYSWP.LC_ForkliftChecks.Dtos
{
    public class GetLC_ForkliftChecksInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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