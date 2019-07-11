

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
using GYSWP.LC_InStorageBills;


namespace GYSWP.LC_InStorageBills.DomainService
{
    /// <summary>
    /// LC_InStorageBill领域层的业务管理
    ///</summary>
    public class LC_InStorageBillManager :GYSWPDomainServiceBase, ILC_InStorageBillManager
    {
		
		private readonly IRepository<LC_InStorageBill,Guid> _repository;

		/// <summary>
		/// LC_InStorageBill的构造方法
		///</summary>
		public LC_InStorageBillManager(
			IRepository<LC_InStorageBill, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_InStorageBill()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
