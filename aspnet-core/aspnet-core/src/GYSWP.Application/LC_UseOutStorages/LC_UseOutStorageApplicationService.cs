
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using GYSWP.LC_UseOutStorages;
using GYSWP.LC_UseOutStorages.Dtos;
using GYSWP.LC_UseOutStorages.DomainService;



namespace GYSWP.LC_UseOutStorages
{
    /// <summary>
    /// LC_UseOutStorage应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_UseOutStorageAppService : GYSWPAppServiceBase, ILC_UseOutStorageAppService
    {
        private readonly IRepository<LC_UseOutStorage, Guid> _entityRepository;

        private readonly ILC_UseOutStorageManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_UseOutStorageAppService(
        IRepository<LC_UseOutStorage, Guid> entityRepository
        ,ILC_UseOutStorageManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_UseOutStorage的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_UseOutStorageListDto>> GetPaged(GetLC_UseOutStoragesInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_UseOutStorageListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_UseOutStorageListDto>>();

			return new PagedResultDto<LC_UseOutStorageListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_UseOutStorageListDto信息
		/// </summary>
		 
		public async Task<LC_UseOutStorageListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_UseOutStorageListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_UseOutStorage
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_UseOutStorageForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_UseOutStorageForEditOutput();
LC_UseOutStorageEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_UseOutStorageEditDto>();

				//lC_UseOutStorageEditDto = ObjectMapper.Map<List<lC_UseOutStorageEditDto>>(entity);
			}
			else
			{
				editDto = new LC_UseOutStorageEditDto();
			}

			output.LC_UseOutStorage = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_UseOutStorage的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_UseOutStorageInput input)
		{

			if (input.LC_UseOutStorage.Id.HasValue)
			{
				await Update(input.LC_UseOutStorage);
			}
			else
			{
				await Create(input.LC_UseOutStorage);
			}
		}


		/// <summary>
		/// 新增LC_UseOutStorage
		/// </summary>
		
		protected virtual async Task<LC_UseOutStorageEditDto> Create(LC_UseOutStorageEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_UseOutStorage>(input);
            var entity=input.MapTo<LC_UseOutStorage>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_UseOutStorageEditDto>();
		}

		/// <summary>
		/// 编辑LC_UseOutStorage
		/// </summary>
		
		protected virtual async Task Update(LC_UseOutStorageEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_UseOutStorage信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_UseOutStorage的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_UseOutStorage为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetToExcel()
		//{
		//	var users = await UserManager.Users.ToListAsync();
		//	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
		//	await FillRoleNames(userListDtos);
		//	return _userListExcelExporter.ExportToFile(userListDtos);
		//}

    }
}


