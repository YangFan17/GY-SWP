
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_LPFunctionRecords;

namespace GYSWP.LC_LPFunctionRecords.Dtos
{
    public class GetLC_LPFunctionRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
