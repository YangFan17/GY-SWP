
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.SelfChekRecords;

namespace GYSWP.SelfChekRecords.Dtos
{
    public class GetSelfChekRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
