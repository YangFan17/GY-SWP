
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Documents;

namespace GYSWP.Documents.Dtos
{
    public class GetDocumentsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string KeyWord { get; set; }

        public long DeptId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryCode
        {
            get
            {
                if (CategoryId.HasValue)
                {
                    return "," + CategoryId.ToString() + ",";
                }
                return null;
            }
        }
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
