
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_SsjWeekWhByRecords;

namespace GYSWP.LC_SsjWeekWhByRecords.Dtos
{
    public class GetLC_SsjWeekWhByRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
