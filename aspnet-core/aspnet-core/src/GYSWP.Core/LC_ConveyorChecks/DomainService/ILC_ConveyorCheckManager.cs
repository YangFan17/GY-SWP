

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.LC_ConveyorChecks;


namespace GYSWP.LC_ConveyorChecks.DomainService
{
    public interface ILC_ConveyorCheckManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLC_ConveyorCheck();



		 
      
         

    }
}
