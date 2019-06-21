
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.DocRevisions;

namespace GYSWP.DocRevisions.Dtos
{
    public class GetDocRevisionsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
