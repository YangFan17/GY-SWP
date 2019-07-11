

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
using GYSWP.LC_CigaretExchanges;


namespace GYSWP.LC_CigaretExchanges.DomainService
{
    /// <summary>
    /// LC_CigaretExchange领域层的业务管理
    ///</summary>
    public class LC_CigaretExchangeManager :GYSWPDomainServiceBase, ILC_CigaretExchangeManager
    {
		
		private readonly IRepository<LC_CigaretExchange,Guid> _repository;

		/// <summary>
		/// LC_CigaretExchange的构造方法
		///</summary>
		public LC_CigaretExchangeManager(
			IRepository<LC_CigaretExchange, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_CigaretExchange()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
