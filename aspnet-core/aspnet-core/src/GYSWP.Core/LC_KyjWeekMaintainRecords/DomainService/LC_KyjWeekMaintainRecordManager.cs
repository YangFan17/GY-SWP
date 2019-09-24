

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
using GYSWP.LC_KyjWeekMaintainRecords;


namespace GYSWP.LC_KyjWeekMaintainRecords.DomainService
{
    /// <summary>
    /// LC_KyjWeekMaintainRecord领域层的业务管理
    ///</summary>
    public class LC_KyjWeekMaintainRecordManager :GYSWPDomainServiceBase, ILC_KyjWeekMaintainRecordManager
    {
		
		private readonly IRepository<LC_KyjWeekMaintainRecord,Guid> _repository;

		/// <summary>
		/// LC_KyjWeekMaintainRecord的构造方法
		///</summary>
		public LC_KyjWeekMaintainRecordManager(
			IRepository<LC_KyjWeekMaintainRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_KyjWeekMaintainRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
