

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
using GYSWP.LC_OutScanRecords;


namespace GYSWP.LC_OutScanRecords.DomainService
{
    /// <summary>
    /// LC_OutScanRecord领域层的业务管理
    ///</summary>
    public class LC_OutScanRecordManager :GYSWPDomainServiceBase, ILC_OutScanRecordManager
    {
		
		private readonly IRepository<LC_OutScanRecord,Guid> _repository;

		/// <summary>
		/// LC_OutScanRecord的构造方法
		///</summary>
		public LC_OutScanRecordManager(
			IRepository<LC_OutScanRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_OutScanRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
