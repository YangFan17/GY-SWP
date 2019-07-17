
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.PositionInfos;

namespace GYSWP.PositionInfos.Dtos
{
    public class GetMainPointsRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
