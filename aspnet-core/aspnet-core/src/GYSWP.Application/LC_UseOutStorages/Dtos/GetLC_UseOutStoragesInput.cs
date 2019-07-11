
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_UseOutStorages;

namespace GYSWP.LC_UseOutStorages.Dtos
{
    public class GetLC_UseOutStoragesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
