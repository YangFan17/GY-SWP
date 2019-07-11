
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


using GYSWP.LC_CigaretExchanges;
using GYSWP.LC_CigaretExchanges.Dtos;
using GYSWP.LC_CigaretExchanges.DomainService;



namespace GYSWP.LC_CigaretExchanges
{
    /// <summary>
    /// LC_CigaretExchange应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_CigaretExchangeAppService : GYSWPAppServiceBase, ILC_CigaretExchangeAppService
    {
        private readonly IRepository<LC_CigaretExchange, Guid> _entityRepository;

        private readonly ILC_CigaretExchangeManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_CigaretExchangeAppService(
        IRepository<LC_CigaretExchange, Guid> entityRepository
        ,ILC_CigaretExchangeManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_CigaretExchange的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_CigaretExchangeListDto>> GetPaged(GetLC_CigaretExchangesInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_CigaretExchangeListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_CigaretExchangeListDto>>();

			return new PagedResultDto<LC_CigaretExchangeListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_CigaretExchangeListDto信息
		/// </summary>
		 
		public async Task<LC_CigaretExchangeListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_CigaretExchangeListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_CigaretExchange
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_CigaretExchangeForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_CigaretExchangeForEditOutput();
LC_CigaretExchangeEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_CigaretExchangeEditDto>();

				//lC_CigaretExchangeEditDto = ObjectMapper.Map<List<lC_CigaretExchangeEditDto>>(entity);
			}
			else
			{
				editDto = new LC_CigaretExchangeEditDto();
			}

			output.LC_CigaretExchange = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_CigaretExchange的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_CigaretExchangeInput input)
		{

			if (input.LC_CigaretExchange.Id.HasValue)
			{
				await Update(input.LC_CigaretExchange);
			}
			else
			{
				await Create(input.LC_CigaretExchange);
			}
		}


		/// <summary>
		/// 新增LC_CigaretExchange
		/// </summary>
		
		protected virtual async Task<LC_CigaretExchangeEditDto> Create(LC_CigaretExchangeEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_CigaretExchange>(input);
            var entity=input.MapTo<LC_CigaretExchange>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_CigaretExchangeEditDto>();
		}

		/// <summary>
		/// 编辑LC_CigaretExchange
		/// </summary>
		
		protected virtual async Task Update(LC_CigaretExchangeEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_CigaretExchange信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_CigaretExchange的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_CigaretExchange为excel表,等待开发。
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


