

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.Documents;


namespace GYSWP.Documents.DomainService
{
    public interface IDocumentManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitDocument();



		 
      
         

    }
}
