

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.EntryExitRegistrations;


namespace GYSWP.EntryExitRegistrations.DomainService
{
    public interface IEntryExitRegistrationManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitEntryExitRegistration();



		 
      
         

    }
}
