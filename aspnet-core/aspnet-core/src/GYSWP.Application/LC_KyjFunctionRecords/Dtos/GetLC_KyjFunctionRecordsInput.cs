
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_KyjFunctionRecords;

namespace GYSWP.LC_KyjFunctionRecords.Dtos
{
    public class GetLC_KyjFunctionRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
