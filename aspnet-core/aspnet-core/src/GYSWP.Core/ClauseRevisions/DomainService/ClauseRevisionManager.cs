

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
using GYSWP.ClauseRevisions;


namespace GYSWP.ClauseRevisions.DomainService
{
    /// <summary>
    /// ClauseRevision领域层的业务管理
    ///</summary>
    public class ClauseRevisionManager :GYSWPDomainServiceBase, IClauseRevisionManager
    {
		
		private readonly IRepository<ClauseRevision,Guid> _repository;

		/// <summary>
		/// ClauseRevision的构造方法
		///</summary>
		public ClauseRevisionManager(
			IRepository<ClauseRevision, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitClauseRevision()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
