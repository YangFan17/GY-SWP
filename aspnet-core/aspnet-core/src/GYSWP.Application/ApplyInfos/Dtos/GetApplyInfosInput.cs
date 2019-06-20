
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.ApplyInfos;

namespace GYSWP.ApplyInfos.Dtos
{
    public class GetApplyInfosInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
