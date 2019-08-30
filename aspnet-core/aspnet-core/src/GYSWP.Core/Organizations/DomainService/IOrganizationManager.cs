

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.Organizations;


namespace GYSWP.Organizations.DomainService
{
    public interface IOrganizationManager : IDomainService
    {
        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitOrganization();

        Task<long?> GetEmpDeptRoodIdAsync(string empId);
    }
}
