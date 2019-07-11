
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


using GYSWP.LC_InStorageBills;
using GYSWP.LC_InStorageBills.Dtos;
using GYSWP.LC_InStorageBills.DomainService;



namespace GYSWP.LC_InStorageBills
{
    /// <summary>
    /// LC_InStorageBill应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_InStorageBillAppService : GYSWPAppServiceBase, ILC_InStorageBillAppService
    {
        private readonly IRepository<LC_InStorageBill, Guid> _entityRepository;

        private readonly ILC_InStorageBillManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_InStorageBillAppService(
        IRepository<LC_InStorageBill, Guid> entityRepository
        ,ILC_InStorageBillManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_InStorageBill的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_InStorageBillListDto>> GetPaged(GetLC_InStorageBillsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_InStorageBillListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_InStorageBillListDto>>();

			return new PagedResultDto<LC_InStorageBillListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_InStorageBillListDto信息
		/// </summary>
		 
		public async Task<LC_InStorageBillListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_InStorageBillListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_InStorageBill
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_InStorageBillForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_InStorageBillForEditOutput();
LC_InStorageBillEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_InStorageBillEditDto>();

				//lC_InStorageBillEditDto = ObjectMapper.Map<List<lC_InStorageBillEditDto>>(entity);
			}
			else
			{
				editDto = new LC_InStorageBillEditDto();
			}

			output.LC_InStorageBill = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_InStorageBill的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_InStorageBillInput input)
		{

			if (input.LC_InStorageBill.Id.HasValue)
			{
				await Update(input.LC_InStorageBill);
			}
			else
			{
				await Create(input.LC_InStorageBill);
			}
		}


		/// <summary>
		/// 新增LC_InStorageBill
		/// </summary>
		
		protected virtual async Task<LC_InStorageBillEditDto> Create(LC_InStorageBillEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_InStorageBill>(input);
            var entity=input.MapTo<LC_InStorageBill>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_InStorageBillEditDto>();
		}

		/// <summary>
		/// 编辑LC_InStorageBill
		/// </summary>
		
		protected virtual async Task Update(LC_InStorageBillEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_InStorageBill信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_InStorageBill的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_InStorageBill为excel表,等待开发。
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

