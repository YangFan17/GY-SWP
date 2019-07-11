

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
using GYSWP.LC_TimeLogs;


namespace GYSWP.LC_TimeLogs.DomainService
{
    /// <summary>
    /// LC_TimeLog领域层的业务管理
    ///</summary>
    public class LC_TimeLogManager :GYSWPDomainServiceBase, ILC_TimeLogManager
    {
		
		private readonly IRepository<LC_TimeLog,Guid> _repository;

		/// <summary>
		/// LC_TimeLog的构造方法
		///</summary>
		public LC_TimeLogManager(
			IRepository<LC_TimeLog, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_TimeLog()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
