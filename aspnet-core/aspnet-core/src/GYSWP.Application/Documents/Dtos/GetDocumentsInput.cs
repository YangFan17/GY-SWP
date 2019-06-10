
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Documents;

namespace GYSWP.Documents.Dtos
{
    public class GetDocumentsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string KeyWord { get; set; }

        public string DeptId { get; set; }
        public int? CategoryId { get; set; }

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
