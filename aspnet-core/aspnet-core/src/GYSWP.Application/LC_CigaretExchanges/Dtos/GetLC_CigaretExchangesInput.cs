
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_CigaretExchanges;

namespace GYSWP.LC_CigaretExchanges.Dtos
{
    public class GetLC_CigaretExchangesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
