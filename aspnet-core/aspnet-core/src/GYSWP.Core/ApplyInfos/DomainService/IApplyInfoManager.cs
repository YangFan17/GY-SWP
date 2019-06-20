

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.ApplyInfos;


namespace GYSWP.ApplyInfos.DomainService
{
    public interface IApplyInfoManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitApplyInfo();



		 
      
         

    }
}
