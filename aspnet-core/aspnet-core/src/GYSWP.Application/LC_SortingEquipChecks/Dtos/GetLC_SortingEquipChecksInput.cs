
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_SortingEquipChecks;

namespace GYSWP.LC_SortingEquipChecks.Dtos
{
    public class GetLC_SortingEquipChecksInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
