
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.EntryExitRegistrations;
using System;

namespace GYSWP.EntryExitRegistrations.Dtos
{
    public class GetEntryExitRegistrationsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
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
