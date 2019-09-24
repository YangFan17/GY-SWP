
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Documents;
using System;

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

    /// <summary>
    /// 标准统计报表Dto
    /// </summary>
    public class GetReportDocInput : PagedSortedAndFilteredInputDto
    {
        public long DeptId { get; set; }
        public string EndTime { get; set; }
        public DateTime EndTimeFormart {
            get
            {
               return Convert.ToDateTime(EndTime);
            }
        }

        public DateTime StartTime { get; set; }
        public ReportDocEnum Type { get; set; }
    }

    public enum ReportDocEnum
    {
        现行标准总数 = 1,
        标准制定个数 = 2,
        标准定制条数 = 3,
        标准修订个数 = 4,
        标准修订条数 = 5,
        标准废止个数 = 6
    }
}
