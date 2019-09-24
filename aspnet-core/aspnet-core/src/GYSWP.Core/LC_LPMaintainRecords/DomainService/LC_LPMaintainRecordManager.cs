

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
using GYSWP.LC_LPMaintainRecords;


namespace GYSWP.LC_LPMaintainRecords.DomainService
{
    /// <summary>
    /// LC_LPMaintainRecord领域层的业务管理
    ///</summary>
    public class LC_LPMaintainRecordManager :GYSWPDomainServiceBase, ILC_LPMaintainRecordManager
    {
		
		private readonly IRepository<LC_LPMaintainRecord,Guid> _repository;

		/// <summary>
		/// LC_LPMaintainRecord的构造方法
		///</summary>
		public LC_LPMaintainRecordManager(
			IRepository<LC_LPMaintainRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_LPMaintainRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
