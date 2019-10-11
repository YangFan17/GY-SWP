
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_InStorageBills;
using System;

namespace GYSWP.LC_InStorageBills.Dtos
{
    public class GetLC_InStorageBillsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
