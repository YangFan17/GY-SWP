

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
using GYSWP.Employees;
using GYSWP.Organizations;

namespace GYSWP.Employees.DomainService
{
    /// <summary>
    /// Employee领域层的业务管理
    ///</summary>
    public class EmployeeManager :GYSWPDomainServiceBase, IEmployeeManager
    {
		
		private readonly IRepository<Employee,string> _repository;
        private readonly IRepository<Organization, long> _organizationRepository;

        /// <summary>
        /// Employee的构造方法
        ///</summary>
        public EmployeeManager(
			IRepository<Employee, string> repository
            , IRepository<Organization, long> organizationRepository
        )
        {
			_repository =  repository;
            _organizationRepository = organizationRepository;
        }


        /// <summary>
        /// 初始化
        ///</summary>
        public void InitEmployee()
		{
			throw new NotImplementedException();
		}
        private void GetAreaDeptList(long deptId, List<long> deptIdList)
        {
            var list = _organizationRepository.GetAll().Where(o => o.ParentId == deptId).Select(o => o.Id).ToList();
            deptIdList.AddRange(list);
            foreach (var id in list)
            {
                GetAreaDeptList(id, deptIdList);
            }
        }
        public async Task<string[]> GetDeptIdArrayAsync(long deptId)
        {
            return await Task.Run(() =>
            {
                var deptList = new List<long>();
                deptList.Add(deptId);
                GetAreaDeptList(deptId, deptList);
                return deptList.Select(c => "[" + c.ToString() + "]").ToArray();
            });
        }
    }
}
