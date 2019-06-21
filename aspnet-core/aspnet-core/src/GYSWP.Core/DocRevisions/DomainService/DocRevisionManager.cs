

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
using GYSWP.DocRevisions;


namespace GYSWP.DocRevisions.DomainService
{
    /// <summary>
    /// DocRevision领域层的业务管理
    ///</summary>
    public class DocRevisionManager :GYSWPDomainServiceBase, IDocRevisionManager
    {
		
		private readonly IRepository<DocRevision,Guid> _repository;

		/// <summary>
		/// DocRevision的构造方法
		///</summary>
		public DocRevisionManager(
			IRepository<DocRevision, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitDocRevision()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
