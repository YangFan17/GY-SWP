

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
using GYSWP.LC_QualityRecords;


namespace GYSWP.LC_QualityRecords.DomainService
{
    /// <summary>
    /// LC_QualityRecord领域层的业务管理
    ///</summary>
    public class LC_QualityRecordManager :GYSWPDomainServiceBase, ILC_QualityRecordManager
    {
		
		private readonly IRepository<LC_QualityRecord,Guid> _repository;

		/// <summary>
		/// LC_QualityRecord的构造方法
		///</summary>
		public LC_QualityRecordManager(
			IRepository<LC_QualityRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_QualityRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
