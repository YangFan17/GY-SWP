
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.Employees;

namespace GYSWP.Employees.Dtos
{
    public class GetEmployeesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string KeyWord { get; set; }
        public string Name { get; set; }
        public string DepartId { get; set; }
        public string Mobile { get; set; }

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
