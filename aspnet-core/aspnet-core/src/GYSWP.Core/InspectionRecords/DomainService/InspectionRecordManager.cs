

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
using GYSWP.InspectionRecords;


namespace GYSWP.InspectionRecords.DomainService
{
    /// <summary>
    /// InspectionRecord领域层的业务管理
    ///</summary>
    public class InspectionRecordManager :GYSWPDomainServiceBase, IInspectionRecordManager
    {
		
		private readonly IRepository<InspectionRecord,long> _repository;

		/// <summary>
		/// InspectionRecord的构造方法
		///</summary>
		public InspectionRecordManager(
			IRepository<InspectionRecord, long> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitInspectionRecord()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
