
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_SortingMonthRecords;

namespace GYSWP.LC_SortingMonthRecords.Dtos
{
    public class GetLC_SortingMonthRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
