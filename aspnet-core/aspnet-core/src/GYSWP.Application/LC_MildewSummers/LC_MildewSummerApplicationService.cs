
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


using GYSWP.LC_MildewSummers;
using GYSWP.LC_MildewSummers.Dtos;
using GYSWP.LC_MildewSummers.DomainService;
using Abp.Auditing;

namespace GYSWP.LC_MildewSummers
{
    /// <summary>
    /// LC_MildewSummer应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_MildewSummerAppService : GYSWPAppServiceBase, ILC_MildewSummerAppService
    {
        private readonly IRepository<LC_MildewSummer, Guid> _entityRepository;

        private readonly ILC_MildewSummerManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_MildewSummerAppService(
        IRepository<LC_MildewSummer, Guid> entityRepository
        ,ILC_MildewSummerManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_MildewSummer的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_MildewSummerListDto>> GetPaged(GetLC_MildewSummersInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_MildewSummerListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_MildewSummerListDto>>();

			return new PagedResultDto<LC_MildewSummerListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_MildewSummerListDto信息
		/// </summary>
		 
		public async Task<LC_MildewSummerListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_MildewSummerListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_MildewSummer
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_MildewSummerForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_MildewSummerForEditOutput();
LC_MildewSummerEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_MildewSummerEditDto>();

				//lC_MildewSummerEditDto = ObjectMapper.Map<List<lC_MildewSummerEditDto>>(entity);
			}
			else
			{
				editDto = new LC_MildewSummerEditDto();
			}

			output.LC_MildewSummer = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_MildewSummer的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdate(CreateOrUpdateLC_MildewSummerInput input)
		{

			if (input.LC_MildewSummer.Id.HasValue)
			{
				await Update(input.LC_MildewSummer);
			}
			else
			{
				await Create(input.LC_MildewSummer);
			}
		}


		/// <summary>
		/// 新增LC_MildewSummer
		/// </summary>
		
		protected virtual async Task<LC_MildewSummerEditDto> Create(LC_MildewSummerEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_MildewSummer>(input);
            var entity=input.MapTo<LC_MildewSummer>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_MildewSummerEditDto>();
		}

		/// <summary>
		/// 编辑LC_MildewSummer
		/// </summary>
		
		protected virtual async Task Update(LC_MildewSummerEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_MildewSummer信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_MildewSummer的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_MildewSummer为excel表,等待开发。
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


