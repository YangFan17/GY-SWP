

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.LC_ForkliftChecks;


namespace GYSWP.LC_ForkliftChecks.DomainService
{
    public interface ILC_ForkliftCheckManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLC_ForkliftCheck();



		 
      
         

    }
}
