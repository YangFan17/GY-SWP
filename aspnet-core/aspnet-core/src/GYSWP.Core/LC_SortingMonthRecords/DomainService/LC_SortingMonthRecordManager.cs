

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
using GYSWP.LC_SortingMonthRecords;


namespace GYSWP.LC_SortingMonthRecords.DomainService
{
    /// <summary>
    /// LC_SortingMonthRecord领域层的业务管理
    ///</summary>
    public class LC_SortingMonthRecordManager :GYSWPDomainServiceBase, ILC_SortingMonthRecordManager
    {
		
		private readonly IRepository<LC_SortingMonthRecord,Guid> _repository;

		/// <summary>
		/// LC_SortingMonthRecord的构造方法
		///</summary>
		public LC_SortingMonthRecordManager(
			IRepository<LC_SortingMonthRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_SortingMonthRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
