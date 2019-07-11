

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.InspectionRecords;


namespace GYSWP.InspectionRecords.DomainService
{
    public interface IInspectionRecordManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitInspectionRecord();



		 
      
         

    }
}
