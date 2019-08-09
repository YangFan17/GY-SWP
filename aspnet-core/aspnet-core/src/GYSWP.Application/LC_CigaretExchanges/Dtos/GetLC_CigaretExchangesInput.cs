
using Abp.Runtime.Validation;
using GYSWP.Dtos;
using GYSWP.LC_CigaretExchanges;
using System;

namespace GYSWP.LC_CigaretExchanges.Dtos
{
    public class GetLC_CigaretExchangesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
