

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
using GYSWP.LC_ForkliftChecks;


namespace GYSWP.LC_ForkliftChecks.DomainService
{
    /// <summary>
    /// LC_ForkliftCheck领域层的业务管理
    ///</summary>
    public class LC_ForkliftCheckManager :GYSWPDomainServiceBase, ILC_ForkliftCheckManager
    {
		
		private readonly IRepository<LC_ForkliftCheck,Guid> _repository;

		/// <summary>
		/// LC_ForkliftCheck的构造方法
		///</summary>
		public LC_ForkliftCheckManager(
			IRepository<LC_ForkliftCheck, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_ForkliftCheck()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
