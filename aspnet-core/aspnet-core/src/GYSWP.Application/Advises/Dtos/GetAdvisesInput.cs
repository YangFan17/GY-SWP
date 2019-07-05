
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Advises;

namespace GYSWP.Advises.Dtos
{
    public class GetAdvisesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
