
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.EntryExitRegistrations;

namespace GYSWP.EntryExitRegistrations.Dtos
{
    public class GetEntryExitRegistrationsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
