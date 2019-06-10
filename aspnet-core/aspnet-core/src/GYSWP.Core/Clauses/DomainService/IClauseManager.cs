

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.Clauses;


namespace GYSWP.Clauses.DomainService
{
    public interface IClauseManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitClause();



		 
      
         

    }
}
