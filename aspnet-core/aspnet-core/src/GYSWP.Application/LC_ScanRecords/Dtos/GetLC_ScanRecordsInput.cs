
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.GYEnums;
using GYSWP.LC_ScanRecords;
using System;

namespace GYSWP.LC_ScanRecords.Dtos
{
    public class GetLC_ScanRecordsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
    public class CreateLC_ScanRecordInput
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Guid TimeLogId { get; set; }
        public LC_TimeStatus Status { get; set; }
    }
}
