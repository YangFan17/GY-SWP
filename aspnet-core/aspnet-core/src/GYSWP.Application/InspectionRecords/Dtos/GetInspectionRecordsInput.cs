
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.InspectionRecords;

namespace GYSWP.InspectionRecords.Dtos
{
    public class GetInspectionRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
