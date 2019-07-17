

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.PositionInfos;


namespace GYSWP.PositionInfos.DomainService
{
    public interface IPositionInfoManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitPositionInfo();



		 
      
         

    }
}
