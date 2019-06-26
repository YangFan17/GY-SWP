

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
using GYSWP.CriterionExamines;


namespace GYSWP.CriterionExamines.DomainService
{
    /// <summary>
    /// CriterionExamine领域层的业务管理
    ///</summary>
    public class CriterionExamineManager :GYSWPDomainServiceBase, ICriterionExamineManager
    {
		
		private readonly IRepository<CriterionExamine,Guid> _repository;

		/// <summary>
		/// CriterionExamine的构造方法
		///</summary>
		public CriterionExamineManager(
			IRepository<CriterionExamine, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitCriterionExamine()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
