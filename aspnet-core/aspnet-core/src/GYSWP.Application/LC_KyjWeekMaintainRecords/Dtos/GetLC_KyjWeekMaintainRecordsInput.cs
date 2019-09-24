
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_KyjWeekMaintainRecords;

namespace GYSWP.LC_KyjWeekMaintainRecords.Dtos
{
    public class GetLC_KyjWeekMaintainRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
