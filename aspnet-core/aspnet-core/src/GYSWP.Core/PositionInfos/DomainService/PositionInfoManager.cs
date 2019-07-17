

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


namespace GYSWP.PositionInfos.DomainService
{
    /// <summary>
    /// PositionInfo领域层的业务管理
    ///</summary>
    public class PositionInfoManager :GYSWPDomainServiceBase, IPositionInfoManager
    {
		
		private readonly IRepository<PositionInfo,Guid> _repository;

		/// <summary>
		/// PositionInfo的构造方法
		///</summary>
		public PositionInfoManager(
			IRepository<PositionInfo, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitPositionInfo()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
