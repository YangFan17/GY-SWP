
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_OutScanRecords;
using System;

namespace GYSWP.LC_OutScanRecords.Dtos
{
    public class GetLC_OutScanRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
