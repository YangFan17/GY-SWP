
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

    /// <summary>
    /// 标准统计报表Dto
    /// </summary>
    public class GetReportDocInput : PagedSortedAndFilteredInputDto
    {
        public long DeptId { get; set; }
        public DateTime? StartTime
        {
            get
            {
               var date = Convert.ToDateTime(Month);
                return new DateTime(date.Year, date.Month, 1);
            }
        }
        public string Month { get; set; }

        public DateTime? EndTime
        {
            get
            {
                return StartTime.Value.AddMonths(1);
            }
        }
        public ReportDocEnum Type { get; set; }
    }

    public class GetActionDocumentsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public ActionCategoryEnum? CategoryId { get; set; }
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

    public enum ReportDocEnum
    {
        现行标准总数 = 1,
        标准制定个数 = 2,
        标准定制条数 = 3,
        标准修订个数 = 4,
        标准修订条数 = 5,
        标准废止个数 = 6
    }

    public enum ActionCategoryEnum
    {
        技术标准 = 0,
        管理标准 = 1,
        工作标准 = 2,
        法律法规 = 3,
        上级文件 = 4,
        外来标准 = 5,
        风险库 = 6,
        现行标准库 = 999
    }
}
