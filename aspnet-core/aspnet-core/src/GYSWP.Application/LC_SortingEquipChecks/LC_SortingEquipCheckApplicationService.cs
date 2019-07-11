
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


using GYSWP.LC_SortingEquipChecks;
using GYSWP.LC_SortingEquipChecks.Dtos;
using GYSWP.LC_SortingEquipChecks.DomainService;



namespace GYSWP.LC_SortingEquipChecks
{
    /// <summary>
    /// LC_SortingEquipCheck应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_SortingEquipCheckAppService : GYSWPAppServiceBase, ILC_SortingEquipCheckAppService
    {
        private readonly IRepository<LC_SortingEquipCheck, Guid> _entityRepository;

        private readonly ILC_SortingEquipCheckManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_SortingEquipCheckAppService(
        IRepository<LC_SortingEquipCheck, Guid> entityRepository
        ,ILC_SortingEquipCheckManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_SortingEquipCheck的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_SortingEquipCheckListDto>> GetPaged(GetLC_SortingEquipChecksInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_SortingEquipCheckListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_SortingEquipCheckListDto>>();

			return new PagedResultDto<LC_SortingEquipCheckListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_SortingEquipCheckListDto信息
		/// </summary>
		 
		public async Task<LC_SortingEquipCheckListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_SortingEquipCheckListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_SortingEquipCheck
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_SortingEquipCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_SortingEquipCheckForEditOutput();
LC_SortingEquipCheckEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_SortingEquipCheckEditDto>();

				//lC_SortingEquipCheckEditDto = ObjectMapper.Map<List<lC_SortingEquipCheckEditDto>>(entity);
			}
			else
			{
				editDto = new LC_SortingEquipCheckEditDto();
			}

			output.LC_SortingEquipCheck = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_SortingEquipCheck的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_SortingEquipCheckInput input)
		{

			if (input.LC_SortingEquipCheck.Id.HasValue)
			{
				await Update(input.LC_SortingEquipCheck);
			}
			else
			{
				await Create(input.LC_SortingEquipCheck);
			}
		}


		/// <summary>
		/// 新增LC_SortingEquipCheck
		/// </summary>
		
		protected virtual async Task<LC_SortingEquipCheckEditDto> Create(LC_SortingEquipCheckEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_SortingEquipCheck>(input);
            var entity=input.MapTo<LC_SortingEquipCheck>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_SortingEquipCheckEditDto>();
		}

		/// <summary>
		/// 编辑LC_SortingEquipCheck
		/// </summary>
		
		protected virtual async Task Update(LC_SortingEquipCheckEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_SortingEquipCheck信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_SortingEquipCheck的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_SortingEquipCheck为excel表,等待开发。
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


