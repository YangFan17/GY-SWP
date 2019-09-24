
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_ForkliftMonthWhRecords;

namespace GYSWP.LC_ForkliftMonthWhRecords.Dtos
{
    public class GetLC_ForkliftMonthWhRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
