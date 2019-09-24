
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.DocAttachments;

namespace GYSWP.DocAttachments.Dtos
{
    public class GetLC_AttachmentsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
