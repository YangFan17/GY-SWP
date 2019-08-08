

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
using GYSWP.LC_MildewSummers;


namespace GYSWP.LC_MildewSummers.DomainService
{
    /// <summary>
    /// LC_MildewSummer领域层的业务管理
    ///</summary>
    public class LC_MildewSummerManager :GYSWPDomainServiceBase, ILC_MildewSummerManager
    {
		
		private readonly IRepository<LC_MildewSummer,Guid> _repository;

		/// <summary>
		/// LC_MildewSummer的构造方法
		///</summary>
		public LC_MildewSummerManager(
			IRepository<LC_MildewSummer, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_MildewSummer()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
