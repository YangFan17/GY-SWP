

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
using GYSWP.Indicators;


namespace GYSWP.Indicators.DomainService
{
    /// <summary>
    /// Indicator领域层的业务管理
    ///</summary>
    public class IndicatorManager :GYSWPDomainServiceBase, IIndicatorManager
    {
		
		private readonly IRepository<Indicator,Guid> _repository;

		/// <summary>
		/// Indicator的构造方法
		///</summary>
		public IndicatorManager(
			IRepository<Indicator, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitIndicator()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
