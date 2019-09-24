

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
using GYSWP.LC_SsjMonthWhByRecords;


namespace GYSWP.LC_SsjMonthWhByRecords.DomainService
{
    /// <summary>
    /// LC_SsjMonthWhByRecord领域层的业务管理
    ///</summary>
    public class LC_SsjMonthWhByRecordManager :GYSWPDomainServiceBase, ILC_SsjMonthWhByRecordManager
    {
		
		private readonly IRepository<LC_SsjMonthWhByRecord,Guid> _repository;

		/// <summary>
		/// LC_SsjMonthWhByRecord的构造方法
		///</summary>
		public LC_SsjMonthWhByRecordManager(
			IRepository<LC_SsjMonthWhByRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_SsjMonthWhByRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
