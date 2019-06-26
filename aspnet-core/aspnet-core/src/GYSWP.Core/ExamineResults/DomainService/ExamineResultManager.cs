

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
using GYSWP.ExamineResults;


namespace GYSWP.ExamineResults.DomainService
{
    /// <summary>
    /// ExamineResult领域层的业务管理
    ///</summary>
    public class ExamineResultManager :GYSWPDomainServiceBase, IExamineResultManager
    {
		
		private readonly IRepository<ExamineResult,Guid> _repository;

		/// <summary>
		/// ExamineResult的构造方法
		///</summary>
		public ExamineResultManager(
			IRepository<ExamineResult, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitExamineResult()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
