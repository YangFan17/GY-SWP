
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_ForkliftWeekWhRecords;

namespace GYSWP.LC_ForkliftWeekWhRecords.Dtos
{
    public class GetLC_ForkliftWeekWhRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
