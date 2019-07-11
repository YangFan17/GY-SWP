

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
using GYSWP.LC_ConveyorChecks;


namespace GYSWP.LC_ConveyorChecks.DomainService
{
    /// <summary>
    /// LC_ConveyorCheck领域层的业务管理
    ///</summary>
    public class LC_ConveyorCheckManager :GYSWPDomainServiceBase, ILC_ConveyorCheckManager
    {
		
		private readonly IRepository<LC_ConveyorCheck,Guid> _repository;

		/// <summary>
		/// LC_ConveyorCheck的构造方法
		///</summary>
		public LC_ConveyorCheckManager(
			IRepository<LC_ConveyorCheck, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_ConveyorCheck()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
