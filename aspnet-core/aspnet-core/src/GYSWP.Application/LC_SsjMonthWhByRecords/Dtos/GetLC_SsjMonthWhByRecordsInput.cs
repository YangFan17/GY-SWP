
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_SsjMonthWhByRecords;

namespace GYSWP.LC_SsjMonthWhByRecords.Dtos
{
    public class GetLC_SsjMonthWhByRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
