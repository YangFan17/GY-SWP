

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
using GYSWP.LC_KyjFunctionRecords;


namespace GYSWP.LC_KyjFunctionRecords.DomainService
{
    /// <summary>
    /// LC_KyjFunctionRecord领域层的业务管理
    ///</summary>
    public class LC_KyjFunctionRecordManager :GYSWPDomainServiceBase, ILC_KyjFunctionRecordManager
    {
		
		private readonly IRepository<LC_KyjFunctionRecord,Guid> _repository;

		/// <summary>
		/// LC_KyjFunctionRecord的构造方法
		///</summary>
		public LC_KyjFunctionRecordManager(
			IRepository<LC_KyjFunctionRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_KyjFunctionRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
