

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.ClauseRevisions;


namespace GYSWP.ClauseRevisions.DomainService
{
    public interface IClauseRevisionManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitClauseRevision();



		 
      
         

    }
}
