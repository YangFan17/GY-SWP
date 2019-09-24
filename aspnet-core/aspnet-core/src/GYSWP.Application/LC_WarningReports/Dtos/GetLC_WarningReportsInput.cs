
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_WarningReports;

namespace GYSWP.LC_WarningReports.Dtos
{
    public class GetLC_WarningReportsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
