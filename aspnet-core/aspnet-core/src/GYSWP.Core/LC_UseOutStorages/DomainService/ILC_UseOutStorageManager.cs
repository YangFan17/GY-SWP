

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.LC_UseOutStorages;


namespace GYSWP.LC_UseOutStorages.DomainService
{
    public interface ILC_UseOutStorageManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLC_UseOutStorage();



		 
      
         

    }
}
