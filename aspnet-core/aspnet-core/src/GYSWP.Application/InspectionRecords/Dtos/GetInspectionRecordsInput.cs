
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.InspectionRecords;
using System;

namespace GYSWP.InspectionRecords.Dtos
{
    public class GetInspectionRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
