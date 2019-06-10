
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Organizations;

namespace GYSWP.Organizations.Dtos
{
    public class GetOrganizationsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
