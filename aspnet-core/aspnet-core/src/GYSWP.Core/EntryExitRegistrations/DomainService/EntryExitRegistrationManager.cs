

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
using GYSWP.EntryExitRegistrations;


namespace GYSWP.EntryExitRegistrations.DomainService
{
    /// <summary>
    /// EntryExitRegistration领域层的业务管理
    ///</summary>
    public class EntryExitRegistrationManager :GYSWPDomainServiceBase, IEntryExitRegistrationManager
    {
		
		private readonly IRepository<EntryExitRegistration,long> _repository;

		/// <summary>
		/// EntryExitRegistration的构造方法
		///</summary>
		public EntryExitRegistrationManager(
			IRepository<EntryExitRegistration, long> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitEntryExitRegistration()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
