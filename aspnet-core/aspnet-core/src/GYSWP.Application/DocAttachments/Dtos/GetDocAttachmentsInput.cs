
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.DocAttachments;
using System;

namespace GYSWP.DocAttachments.Dtos
{
    public class GetDocAttachmentsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid Id { get; set; }
        public Guid BllId { get; set; }
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
