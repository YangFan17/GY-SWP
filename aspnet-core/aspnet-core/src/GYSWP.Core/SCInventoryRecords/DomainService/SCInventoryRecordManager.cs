

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
using GYSWP.SCInventoryRecords;


namespace GYSWP.SCInventoryRecords.DomainService
{
    /// <summary>
    /// SCInventoryRecord领域层的业务管理
    ///</summary>
    public class SCInventoryRecordManager :GYSWPDomainServiceBase, ISCInventoryRecordManager
    {
		
		private readonly IRepository<SCInventoryRecord,long> _repository;

		/// <summary>
		/// SCInventoryRecord的构造方法
		///</summary>
		public SCInventoryRecordManager(
			IRepository<SCInventoryRecord, long> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitSCInventoryRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
