
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_ScanRecords;

namespace GYSWP.LC_ScanRecords.Dtos
{
    public class GetLC_ScanRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
