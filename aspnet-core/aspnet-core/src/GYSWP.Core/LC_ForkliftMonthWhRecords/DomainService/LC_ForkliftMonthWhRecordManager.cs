

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
using GYSWP.LC_ForkliftMonthWhRecords;


namespace GYSWP.LC_ForkliftMonthWhRecords.DomainService
{
    /// <summary>
    /// LC_ForkliftMonthWhRecord领域层的业务管理
    ///</summary>
    public class LC_ForkliftMonthWhRecordManager :GYSWPDomainServiceBase, ILC_ForkliftMonthWhRecordManager
    {
		
		private readonly IRepository<LC_ForkliftMonthWhRecord,Guid> _repository;

		/// <summary>
		/// LC_ForkliftMonthWhRecord的构造方法
		///</summary>
		public LC_ForkliftMonthWhRecordManager(
			IRepository<LC_ForkliftMonthWhRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_ForkliftMonthWhRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
