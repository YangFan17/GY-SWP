
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


using GYSWP.SystemDatas;
using GYSWP.SystemDatas.Dtos;
using GYSWP.SystemDatas.DomainService;



namespace GYSWP.SystemDatas
{
    /// <summary>
    /// SystemData应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class SystemDataAppService : GYSWPAppServiceBase, ISystemDataAppService
    {
        private readonly IRepository<SystemData, int> _entityRepository;

        private readonly ISystemDataManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public SystemDataAppService(
        IRepository<SystemData, int> entityRepository
        ,ISystemDataManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取SystemData的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<SystemDataListDto>> GetPagedAsync(GetSystemDatasInput input)
		{

		    var query = _entityRepository.GetAll()
                .WhereIf(input.ModelId.HasValue,aa=>aa.ModelId==input.ModelId.Value)
                .WhereIf(input.Type.HasValue, aa => aa.Type == input.Type.Value);
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<SystemDataListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<SystemDataListDto>>();

			return new PagedResultDto<SystemDataListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取SystemDataListDto信息
		/// </summary>
		 
		public async Task<SystemDataListDto> GetByIdAsync(EntityDto<int> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<SystemDataListDto>();
		}

		/// <summary>
		/// 获取编辑 SystemData
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetSystemDataForEditOutput> GetForEditAsync(NullableIdDto<int> input)
		{
			var output = new GetSystemDataForEditOutput();
SystemDataEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<SystemDataEditDto>();

				//systemDataEditDto = ObjectMapper.Map<List<systemDataEditDto>>(entity);
			}
			else
			{
				editDto = new SystemDataEditDto();
			}

			output.SystemData = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改SystemData的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdateAsync(CreateOrUpdateSystemDataInput input)
		{

			if (input.SystemData.Id.HasValue)
			{
				await Update(input.SystemData);
			}
			else
			{
				await Create(input.SystemData);
			}
		}


		/// <summary>
		/// 新增SystemData
		/// </summary>
		
		protected virtual async Task<SystemDataEditDto> Create(SystemDataEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <SystemData>(input);
            var entity=input.MapTo<SystemData>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<SystemDataEditDto>();
		}

		/// <summary>
		/// 编辑SystemData
		/// </summary>
		
		protected virtual async Task Update(SystemDataEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除SystemData信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteAsync(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除SystemData的方法
		/// </summary>
		
		public async Task BatchDeleteAsync(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出SystemData为excel表,等待开发。
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


