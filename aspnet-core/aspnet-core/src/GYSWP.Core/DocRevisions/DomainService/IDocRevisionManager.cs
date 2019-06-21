

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.DocRevisions;


namespace GYSWP.DocRevisions.DomainService
{
    public interface IDocRevisionManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitDocRevision();



		 
      
         

    }
}
