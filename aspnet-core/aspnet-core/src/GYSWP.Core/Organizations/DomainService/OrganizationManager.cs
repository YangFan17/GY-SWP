

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
using GYSWP.Organizations;
using GYSWP.Dtos;
using GYSWP.Employees;

namespace GYSWP.Organizations.DomainService
{
    /// <summary>
    /// Organization领域层的业务管理
    ///</summary>
    public class OrganizationManager :GYSWPDomainServiceBase, IOrganizationManager
    {
		
		private readonly IRepository<Organization,long> _repository;
        private readonly IRepository<Employee, string> _employeeRepository;
        /// <summary>
        /// Organization的构造方法
        ///</summary>
        public OrganizationManager(
			IRepository<Organization, long> repository
          , IRepository<Employee, string> employeeRepository
        )
        {
			_repository =  repository;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// 初始化
        ///</summary>
        public void InitOrganization()
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// 获取当前用户根部门Id
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public async Task<long?> GetEmpDeptRoodIdAsync(string empId)
        {
            string deptStr = await _employeeRepository.GetAll().Where(v => v.Id == empId).Select(v=>v.Department).FirstOrDefaultAsync();
            string deptId = deptStr.Replace('[', ' ').Replace(']', ' ').Trim();
            var curDeptId = await _repository.GetAll().Where(v => v.Id.ToString() == deptId).Select(v=> new { v.Id, v.ParentId }).FirstOrDefaultAsync();
            //long rootId =  GetRootDeptIdsync(curDeptId, ref resultId);
            long? rootId = curDeptId.Id;
            if (curDeptId.ParentId != 1)
            {
                long? resultId = curDeptId.ParentId;
                rootId = GetTopDeptId(curDeptId.ParentId, ref resultId);
            }
            return rootId;
        }

        /// <summary>
        /// 递归出根部门Id
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        private long GetRootDeptIdsync(long deptId)
        {
            var curOrg =  _repository.GetAll().Where(v => v.Id == deptId).FirstOrDefault();
            if(curOrg.ParentId == 1)
            {
                return curOrg.Id;
            }
            return GetRootDeptIdsync(curOrg.Id);
        }

        private long? GetTopDeptId(long? pId, ref long? resultId)
        {
            var result = _repository.GetAll().Where(v => v.Id == pId).Select(v => new { v.ParentId, v.Id }).FirstOrDefault();
            resultId = result.Id;
            if (result.ParentId != 1)
            {
                GetTopDeptId(result.ParentId, ref resultId);
            }
            return resultId;
        }
    }
}
