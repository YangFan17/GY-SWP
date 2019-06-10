
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.SystemDatas;

namespace GYSWP.SystemDatas.Dtos
{
    public class GetSystemDatasInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
