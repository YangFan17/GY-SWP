

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
using GYSWP.IndicatorLibrarys;


namespace GYSWP.IndicatorLibrarys.DomainService
{
    /// <summary>
    /// IndicatorLibrary领域层的业务管理
    ///</summary>
    public class IndicatorLibraryManager :GYSWPDomainServiceBase, IIndicatorLibraryManager
    {
		
		private readonly IRepository<IndicatorLibrary,Guid> _repository;

		/// <summary>
		/// IndicatorLibrary的构造方法
		///</summary>
		public IndicatorLibraryManager(
			IRepository<IndicatorLibrary, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitIndicatorLibrary()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
