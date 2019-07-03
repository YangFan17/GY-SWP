
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.GYEnums;
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

        /// <summary>
        /// 查询条件-所属模块（标准化工作平台）
        /// </summary>
        public ConfigModel? ModelId { get; set; }

        /// <summary>
        /// 查询条件-配置类型（等）
        /// </summary>
        public ConfigType? Type { get; set; }

    }
}
