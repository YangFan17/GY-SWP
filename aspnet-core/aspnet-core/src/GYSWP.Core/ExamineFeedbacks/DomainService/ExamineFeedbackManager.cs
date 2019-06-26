

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
using GYSWP.ExamineFeedbacks;


namespace GYSWP.ExamineFeedbacks.DomainService
{
    /// <summary>
    /// ExamineFeedback领域层的业务管理
    ///</summary>
    public class ExamineFeedbackManager :GYSWPDomainServiceBase, IExamineFeedbackManager
    {
		
		private readonly IRepository<ExamineFeedback,Guid> _repository;

		/// <summary>
		/// ExamineFeedback的构造方法
		///</summary>
		public ExamineFeedbackManager(
			IRepository<ExamineFeedback, Guid> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitExamineFeedback()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
