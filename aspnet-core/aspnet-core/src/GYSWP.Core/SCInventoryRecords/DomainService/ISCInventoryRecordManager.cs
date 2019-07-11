

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.SCInventoryRecords;


namespace GYSWP.SCInventoryRecords.DomainService
{
    public interface ISCInventoryRecordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitSCInventoryRecord();



		 
      
         

    }
}
