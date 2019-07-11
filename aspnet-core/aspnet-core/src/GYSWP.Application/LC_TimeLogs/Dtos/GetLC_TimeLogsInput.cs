
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_TimeLogs;

namespace GYSWP.LC_TimeLogs.Dtos
{
    public class GetLC_TimeLogsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
