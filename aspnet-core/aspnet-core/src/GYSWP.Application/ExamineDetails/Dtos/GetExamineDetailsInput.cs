
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.ExamineDetails;
using GYSWP.GYEnums;
using System;

namespace GYSWP.ExamineDetails.Dtos
{
    public class GetExamineDetailsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid Id { get; set; }
        public ExamineStatus Result { get; set; }
        public Guid ExamineId { get; set; }
        public string EmployeeId { get; set; }

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
