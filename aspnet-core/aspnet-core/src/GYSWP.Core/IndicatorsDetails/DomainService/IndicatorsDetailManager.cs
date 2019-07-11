

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
using GYSWP.IndicatorsDetails;


namespace GYSWP.IndicatorsDetails.DomainService
{
    /// <summary>
    /// IndicatorsDetail领域层的业务管理
    ///</summary>
    public class IndicatorsDetailManager :GYSWPDomainServiceBase, IIndicatorsDetailManager
    {
		
		private readonly IRepository<IndicatorsDetail,Guid> _repository;

		/// <summary>
		/// IndicatorsDetail的构造方法
		///</summary>
		public IndicatorsDetailManager(
			IRepository<IndicatorsDetail, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitIndicatorsDetail()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}