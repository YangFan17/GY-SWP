

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Abp.UI;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

using GYSWP;
using GYSWP.DocAttachments;


namespace GYSWP.DocAttachments.DomainService
{
    /// <summary>
    /// LC_Attachment领域层的业务管理
    ///</summary>
    public class LC_AttachmentManager :GYSWPDomainServiceBase, ILC_AttachmentManager
    {
		
		private readonly IRepository<LC_Attachment,Guid> _repository;

		/// <summary>
		/// LC_Attachment的构造方法
		///</summary>
		public LC_AttachmentManager(
			IRepository<LC_Attachment, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_Attachment()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
