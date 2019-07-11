

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.LC_OutScanRecords;


namespace GYSWP.LC_OutScanRecords.DomainService
{
    public interface ILC_OutScanRecordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLC_OutScanRecord();



		 
      
         

    }
}
