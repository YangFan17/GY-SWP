

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
using GYSWP.Categorys;


namespace GYSWP.Categorys.DomainService
{
    /// <summary>
    /// Category领域层的业务管理
    ///</summary>
    public class CategoryManager :GYSWPDomainServiceBase, ICategoryManager
    {
		
		private readonly IRepository<Category,int> _repository;

		/// <summary>
		/// Category的构造方法
		///</summary>
		public CategoryManager(
			IRepository<Category, int> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitCategory()
		{
			throw new NotImplementedException();
		}

        private async Task GetParentCategoryAsync(int id, List<Category> plist)
        {
            var entity = await _repository.GetAsync(id);
            if (entity != null)
            {
                plist.Insert(0, entity);
                if (entity.ParentId.HasValue && entity.ParentId != 0)
                {
                    await GetParentCategoryAsync(entity.ParentId.Value, plist);
                }
            }
        }

        public async Task<List<Category>> GetHierarchyCategories(int id)
        {
            var plist = new List<Category>();
            await GetParentCategoryAsync(id, plist);
            return plist;
        }
    }
}
