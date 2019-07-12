

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.LC_ScanRecords;


namespace GYSWP.LC_ScanRecords.DomainService
{
    public interface ILC_ScanRecordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLC_ScanRecord();



		 
      
         

    }
}
