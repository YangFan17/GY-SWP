

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.IndicatorLibrarys;


namespace GYSWP.IndicatorLibrarys.DomainService
{
    public interface IIndicatorLibraryManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitIndicatorLibrary();



		 
      
         

    }
}
