

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
using GYSWP.LC_SsjWeekWhByRecords;


namespace GYSWP.LC_SsjWeekWhByRecords.DomainService
{
    /// <summary>
    /// LC_SsjWeekWhByRecord领域层的业务管理
    ///</summary>
    public class LC_SsjWeekWhByRecordManager :GYSWPDomainServiceBase, ILC_SsjWeekWhByRecordManager
    {
		
		private readonly IRepository<LC_SsjWeekWhByRecord,Guid> _repository;

		/// <summary>
		/// LC_SsjWeekWhByRecord的构造方法
		///</summary>
		public LC_SsjWeekWhByRecordManager(
			IRepository<LC_SsjWeekWhByRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_SsjWeekWhByRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
