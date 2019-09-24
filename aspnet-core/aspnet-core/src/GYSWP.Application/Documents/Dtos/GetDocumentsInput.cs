
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Documents;
using System;
using GYSWP.GYEnums;

namespace GYSWP.Documents.Dtos
{
    public class GetDocumentsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string KeyWord { get; set; }

        public long DeptId { get; set; }
        public int? CategoryId { get; set; }

        public CategoryType? CategoryTypeId { get; set; } 
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

    public class DocumentReadInput 
    {
        public string Path { get; set; }

        public int CategoryId { get; set; }
    }

    public class GetConfirmTypeInput : PagedSortedAndFilteredInputDto
    {
        /// <summary>
        /// 1 已认领 2 未认领
        /// </summary>
        public int Type { get; set; }
        public Guid DocId { get; set; }
    }
}
