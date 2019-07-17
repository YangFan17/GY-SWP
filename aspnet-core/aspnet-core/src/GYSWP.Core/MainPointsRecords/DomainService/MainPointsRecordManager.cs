

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
using GYSWP.PositionInfos;
using GYSWP.MainPointsRecords;

namespace GYSWP.PositionInfos.DomainService
{
    /// <summary>
    /// MainPointsRecord领域层的业务管理
    ///</summary>
    public class MainPointsRecordManager :GYSWPDomainServiceBase, IMainPointsRecordManager
    {
		
		private readonly IRepository<MainPointsRecord,Guid> _repository;

		/// <summary>
		/// MainPointsRecord的构造方法
		///</summary>
		public MainPointsRecordManager(
			IRepository<MainPointsRecord, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitMainPointsRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
