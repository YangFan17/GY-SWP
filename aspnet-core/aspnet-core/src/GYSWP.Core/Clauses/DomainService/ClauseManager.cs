

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
using GYSWP.Clauses;


namespace GYSWP.Clauses.DomainService
{
    /// <summary>
    /// Clause领域层的业务管理
    ///</summary>
    public class ClauseManager :GYSWPDomainServiceBase, IClauseManager
    {
		
		private readonly IRepository<Clause,Guid> _repository;

		/// <summary>
		/// Clause的构造方法
		///</summary>
		public ClauseManager(
			IRepository<Clause, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitClause()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
