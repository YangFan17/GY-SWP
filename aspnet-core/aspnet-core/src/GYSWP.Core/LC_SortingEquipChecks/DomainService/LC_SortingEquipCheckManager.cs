

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
using GYSWP.LC_SortingEquipChecks;


namespace GYSWP.LC_SortingEquipChecks.DomainService
{
    /// <summary>
    /// LC_SortingEquipCheck领域层的业务管理
    ///</summary>
    public class LC_SortingEquipCheckManager :GYSWPDomainServiceBase, ILC_SortingEquipCheckManager
    {
		
		private readonly IRepository<LC_SortingEquipCheck,Guid> _repository;

		/// <summary>
		/// LC_SortingEquipCheck的构造方法
		///</summary>
		public LC_SortingEquipCheckManager(
			IRepository<LC_SortingEquipCheck, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitLC_SortingEquipCheck()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
