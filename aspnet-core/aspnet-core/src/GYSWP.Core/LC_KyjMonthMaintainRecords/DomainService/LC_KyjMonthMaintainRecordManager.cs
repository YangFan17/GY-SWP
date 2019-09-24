

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
using GYSWP.LC_KyjMonthMaintainRecords;


namespace GYSWP.LC_KyjMonthMaintainRecords.DomainService
{
    /// <summary>
    /// LC_KyjMonthMaintainRecord领域层的业务管理
    ///</summary>
    public class LC_KyjMonthMaintainRecordManager :GYSWPDomainServiceBase, ILC_KyjMonthMaintainRecordManager
    {
		
		private readonly IRepository<LC_KyjMonthMaintainRecord,Guid> _repository;

		/// <summary>
		/// LC_KyjMonthMaintainRecord的构造方法
		///</summary>
		public LC_KyjMonthMaintainRecordManager(
			IRepository<LC_KyjMonthMaintainRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_KyjMonthMaintainRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
