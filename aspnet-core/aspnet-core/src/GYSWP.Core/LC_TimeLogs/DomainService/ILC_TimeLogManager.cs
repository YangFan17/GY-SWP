

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.LC_TimeLogs;


namespace GYSWP.LC_TimeLogs.DomainService
{
    public interface ILC_TimeLogManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitLC_TimeLog();



		 
      
         

    }
}
