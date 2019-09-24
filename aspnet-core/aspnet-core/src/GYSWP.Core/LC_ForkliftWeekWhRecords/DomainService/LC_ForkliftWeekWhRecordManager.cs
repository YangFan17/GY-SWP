

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
using GYSWP.LC_ForkliftWeekWhRecords;


namespace GYSWP.LC_ForkliftWeekWhRecords.DomainService
{
    /// <summary>
    /// LC_ForkliftWeekWhRecord领域层的业务管理
    ///</summary>
    public class LC_ForkliftWeekWhRecordManager :GYSWPDomainServiceBase, ILC_ForkliftWeekWhRecordManager
    {
		
		private readonly IRepository<LC_ForkliftWeekWhRecord,Guid> _repository;

		/// <summary>
		/// LC_ForkliftWeekWhRecord的构造方法
		///</summary>
		public LC_ForkliftWeekWhRecordManager(
			IRepository<LC_ForkliftWeekWhRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_ForkliftWeekWhRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
