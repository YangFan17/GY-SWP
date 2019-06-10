

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.SystemDatas;


namespace GYSWP.SystemDatas.DomainService
{
    public interface ISystemDataManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitSystemData();



		 
      
         

    }
}
