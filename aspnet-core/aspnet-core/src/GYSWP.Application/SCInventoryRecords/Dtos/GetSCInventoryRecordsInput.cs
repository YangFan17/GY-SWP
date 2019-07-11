
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.SCInventoryRecords;

namespace GYSWP.SCInventoryRecords.Dtos
{
    public class GetSCInventoryRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
