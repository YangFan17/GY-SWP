

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.DocAttachments;


namespace GYSWP.DocAttachments.DomainService
{
    public interface IDocAttachmentManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitDocAttachment();



		 
      
         

    }
}
