
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


using GYSWP.EntryExitRegistrations;
using GYSWP.EntryExitRegistrations.Dtos;
using GYSWP.EntryExitRegistrations.DomainService;
using Abp.Auditing;

namespace GYSWP.EntryExitRegistrations
{
    /// <summary>
    /// EntryExitRegistration应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class EntryExitRegistrationAppService : GYSWPAppServiceBase, IEntryExitRegistrationAppService
    {
        private readonly IRepository<EntryExitRegistration, long> _entityRepository;

        private readonly IEntryExitRegistrationManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public EntryExitRegistrationAppService(
        IRepository<EntryExitRegistration, long> entityRepository
        ,IEntryExitRegistrationManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取EntryExitRegistration的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<EntryExitRegistrationListDto>> GetPagedAsync(GetEntryExitRegistrationsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<EntryExitRegistrationListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<EntryExitRegistrationListDto>>();

			return new PagedResultDto<EntryExitRegistrationListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取EntryExitRegistrationListDto信息
		/// </summary>
		 
		public async Task<EntryExitRegistrationListDto> GetByIdAsync(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<EntryExitRegistrationListDto>();
		}

		/// <summary>
		/// 获取编辑 EntryExitRegistration
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetEntryExitRegistrationForEditOutput> GetForEditAsync(NullableIdDto<long> input)
		{
			var output = new GetEntryExitRegistrationForEditOutput();
EntryExitRegistrationEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<EntryExitRegistrationEditDto>();

				//entryExitRegistrationEditDto = ObjectMapper.Map<List<entryExitRegistrationEditDto>>(entity);
			}
			else
			{
				editDto = new EntryExitRegistrationEditDto();
			}

			output.EntryExitRegistration = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改EntryExitRegistration的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdateAsync(CreateOrUpdateEntryExitRegistrationInput input)
		{

			if (input.EntryExitRegistration.Id.HasValue)
			{
				await Update(input.EntryExitRegistration);
			}
			else
			{
				await Create(input.EntryExitRegistration);
			}
		}


		/// <summary>
		/// 新增EntryExitRegistration
		/// </summary>
		
		protected virtual async Task<EntryExitRegistrationEditDto> Create(EntryExitRegistrationEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <EntryExitRegistration>(input);
            var entity=input.MapTo<EntryExitRegistration>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<EntryExitRegistrationEditDto>();
		}

		/// <summary>
		/// 编辑EntryExitRegistration
		/// </summary>
		
		protected virtual async Task Update(EntryExitRegistrationEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除EntryExitRegistration信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteAsync(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除EntryExitRegistration的方法
		/// </summary>
		
		public async Task BatchDeleteAsync(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出EntryExitRegistration为excel表,等待开发。
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


