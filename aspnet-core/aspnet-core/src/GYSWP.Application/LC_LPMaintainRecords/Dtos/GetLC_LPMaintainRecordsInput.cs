
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_LPMaintainRecords;

namespace GYSWP.LC_LPMaintainRecords.Dtos
{
    public class GetLC_LPMaintainRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
