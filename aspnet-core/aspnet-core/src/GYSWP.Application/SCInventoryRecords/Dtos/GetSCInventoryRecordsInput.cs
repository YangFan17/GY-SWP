
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.SCInventoryRecords;
using System;

namespace GYSWP.SCInventoryRecords.Dtos
{
    public class GetSCInventoryRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
