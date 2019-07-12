

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
using GYSWP.LC_ScanRecords;


namespace GYSWP.LC_ScanRecords.DomainService
{
    /// <summary>
    /// LC_ScanRecord领域层的业务管理
    ///</summary>
    public class LC_ScanRecordManager :GYSWPDomainServiceBase, ILC_ScanRecordManager
    {
		
		private readonly IRepository<LC_ScanRecord,Guid> _repository;

		/// <summary>
		/// LC_ScanRecord的构造方法
		///</summary>
		public LC_ScanRecordManager(
			IRepository<LC_ScanRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_ScanRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
