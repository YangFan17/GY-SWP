
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


using GYSWP.LC_TeamSafetyActivitys;
using GYSWP.LC_TeamSafetyActivitys.Dtos;
using GYSWP.LC_TeamSafetyActivitys.DomainService;
using Abp.Auditing;

namespace GYSWP.LC_TeamSafetyActivitys
{
    /// <summary>
    /// LC_TeamSafetyActivity应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_TeamSafetyActivityAppService : GYSWPAppServiceBase, ILC_TeamSafetyActivityAppService
    {
        private readonly IRepository<LC_TeamSafetyActivity, Guid> _entityRepository;

        private readonly ILC_TeamSafetyActivityManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_TeamSafetyActivityAppService(
        IRepository<LC_TeamSafetyActivity, Guid> entityRepository
        ,ILC_TeamSafetyActivityManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_TeamSafetyActivity的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_TeamSafetyActivityListDto>> GetPaged(GetLC_TeamSafetyActivitysInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_TeamSafetyActivityListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_TeamSafetyActivityListDto>>();

			return new PagedResultDto<LC_TeamSafetyActivityListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_TeamSafetyActivityListDto信息
		/// </summary>
		 
		public async Task<LC_TeamSafetyActivityListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_TeamSafetyActivityListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_TeamSafetyActivity
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_TeamSafetyActivityForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_TeamSafetyActivityForEditOutput();
LC_TeamSafetyActivityEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_TeamSafetyActivityEditDto>();

				//lC_TeamSafetyActivityEditDto = ObjectMapper.Map<List<lC_TeamSafetyActivityEditDto>>(entity);
			}
			else
			{
				editDto = new LC_TeamSafetyActivityEditDto();
			}

			output.LC_TeamSafetyActivity = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改LC_TeamSafetyActivity的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdate(CreateOrUpdateLC_TeamSafetyActivityInput input)
		{

			if (input.LC_TeamSafetyActivity.Id.HasValue)
			{
				await Update(input.LC_TeamSafetyActivity);
			}
			else
			{
				await Create(input.LC_TeamSafetyActivity);
			}
		}


		/// <summary>
		/// 新增LC_TeamSafetyActivity
		/// </summary>
		
		protected virtual async Task<LC_TeamSafetyActivityEditDto> Create(LC_TeamSafetyActivityEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_TeamSafetyActivity>(input);
            var entity=input.MapTo<LC_TeamSafetyActivity>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_TeamSafetyActivityEditDto>();
		}

		/// <summary>
		/// 编辑LC_TeamSafetyActivity
		/// </summary>
		
		protected virtual async Task Update(LC_TeamSafetyActivityEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_TeamSafetyActivity信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_TeamSafetyActivity的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_TeamSafetyActivity为excel表,等待开发。
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


