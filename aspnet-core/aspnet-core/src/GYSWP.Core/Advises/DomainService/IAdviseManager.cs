

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.Advises;


namespace GYSWP.Advises.DomainService
{
    public interface IAdviseManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitAdvise();



		 
      
         

    }
}
