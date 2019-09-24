

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
using GYSWP.LC_LPFunctionRecords;


namespace GYSWP.LC_LPFunctionRecords.DomainService
{
    /// <summary>
    /// LC_LPFunctionRecord领域层的业务管理
    ///</summary>
    public class LC_LPFunctionRecordManager :GYSWPDomainServiceBase, ILC_LPFunctionRecordManager
    {
		
		private readonly IRepository<LC_LPFunctionRecord,Guid> _repository;

		/// <summary>
		/// LC_LPFunctionRecord的构造方法
		///</summary>
		public LC_LPFunctionRecordManager(
			IRepository<LC_LPFunctionRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_LPFunctionRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
