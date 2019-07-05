

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using GYSWP.Categorys;


namespace GYSWP.Categorys.DomainService
{
    public interface ICategoryManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitCategory();
        Task<List<Category>> GetHierarchyCategories(int id);
    }
}
