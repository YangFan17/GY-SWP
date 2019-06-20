

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
using GYSWP.ApplyInfos;


namespace GYSWP.ApplyInfos.DomainService
{
    /// <summary>
    /// ApplyInfo领域层的业务管理
    ///</summary>
    public class ApplyInfoManager :GYSWPDomainServiceBase, IApplyInfoManager
    {
		
		private readonly IRepository<ApplyInfo,Guid> _repository;

		/// <summary>
		/// ApplyInfo的构造方法
		///</summary>
		public ApplyInfoManager(
			IRepository<ApplyInfo, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitApplyInfo()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
