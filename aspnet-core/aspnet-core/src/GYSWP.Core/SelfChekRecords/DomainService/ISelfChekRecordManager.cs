

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.SelfChekRecords;


namespace GYSWP.SelfChekRecords.DomainService
{
    public interface ISelfChekRecordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitSelfChekRecord();



		 
      
         

    }
}
