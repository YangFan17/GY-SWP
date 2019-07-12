

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
using GYSWP.LC_UseOutStorages;


namespace GYSWP.LC_UseOutStorages.DomainService
{
    /// <summary>
    /// LC_UseOutStorage领域层的业务管理
    ///</summary>
    public class LC_UseOutStorageManager :GYSWPDomainServiceBase, ILC_UseOutStorageManager
    {
		
		private readonly IRepository<LC_UseOutStorage,Guid> _repository;

		/// <summary>
		/// LC_UseOutStorage的构造方法
		///</summary>
		public LC_UseOutStorageManager(
			IRepository<LC_UseOutStorage, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_UseOutStorage()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
