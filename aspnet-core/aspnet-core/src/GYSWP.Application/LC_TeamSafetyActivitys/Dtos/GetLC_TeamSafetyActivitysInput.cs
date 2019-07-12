
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_TeamSafetyActivitys;

namespace GYSWP.LC_TeamSafetyActivitys.Dtos
{
    public class GetLC_TeamSafetyActivitysInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
