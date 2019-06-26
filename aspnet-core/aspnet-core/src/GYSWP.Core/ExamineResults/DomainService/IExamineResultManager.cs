

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.ExamineResults;


namespace GYSWP.ExamineResults.DomainService
{
    public interface IExamineResultManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitExamineResult();



		 
      
         

    }
}
