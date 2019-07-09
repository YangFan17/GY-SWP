

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.Indicators;


namespace GYSWP.Indicators.DomainService
{
    public interface IIndicatorManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitIndicator();



		 
      
         

    }
}
