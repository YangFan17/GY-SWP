

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.EmployeeClauses;


namespace GYSWP.EmployeeClauses.DomainService
{
    public interface IEmployeeClauseManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitEmployeeClause();



		 
      
         

    }
}
