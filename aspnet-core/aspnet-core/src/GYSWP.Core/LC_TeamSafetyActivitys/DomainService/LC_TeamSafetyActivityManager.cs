

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
using GYSWP.LC_TeamSafetyActivitys;


namespace GYSWP.LC_TeamSafetyActivitys.DomainService
{
    /// <summary>
    /// LC_TeamSafetyActivity领域层的业务管理
    ///</summary>
    public class LC_TeamSafetyActivityManager :GYSWPDomainServiceBase, ILC_TeamSafetyActivityManager
    {
		
		private readonly IRepository<LC_TeamSafetyActivity,Guid> _repository;

		/// <summary>
		/// LC_TeamSafetyActivity的构造方法
		///</summary>
		public LC_TeamSafetyActivityManager(
			IRepository<LC_TeamSafetyActivity, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_TeamSafetyActivity()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
