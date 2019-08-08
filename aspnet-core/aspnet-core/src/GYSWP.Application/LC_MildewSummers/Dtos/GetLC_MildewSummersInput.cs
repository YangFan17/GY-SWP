
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_MildewSummers;

namespace GYSWP.LC_MildewSummers.Dtos
{
    public class GetLC_MildewSummersInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
