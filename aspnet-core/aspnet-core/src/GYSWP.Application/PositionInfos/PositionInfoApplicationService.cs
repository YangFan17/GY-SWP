
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


using GYSWP.PositionInfos;
using GYSWP.PositionInfos.Dtos;
using GYSWP.PositionInfos.DomainService;



namespace GYSWP.PositionInfos
{
    /// <summary>
    /// PositionInfo应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class PositionInfoAppService : GYSWPAppServiceBase, IPositionInfoAppService
    {
        private readonly IRepository<PositionInfo, Guid> _entityRepository;

        private readonly IPositionInfoManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public PositionInfoAppService(
        IRepository<PositionInfo, Guid> entityRepository
        ,IPositionInfoManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取PositionInfo的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<PositionInfoListDto>> GetPaged(GetPositionInfosInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<PositionInfoListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<PositionInfoListDto>>();

			return new PagedResultDto<PositionInfoListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取PositionInfoListDto信息
		/// </summary>
		 
		public async Task<PositionInfoListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<PositionInfoListDto>();
		}

		/// <summary>
		/// 获取编辑 PositionInfo
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetPositionInfoForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetPositionInfoForEditOutput();
PositionInfoEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<PositionInfoEditDto>();

				//positionInfoEditDto = ObjectMapper.Map<List<positionInfoEditDto>>(entity);
			}
			else
			{
				editDto = new PositionInfoEditDto();
			}

			output.PositionInfo = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改PositionInfo的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdatePositionInfoInput input)
		{

			if (input.PositionInfo.Id.HasValue)
			{
				await Update(input.PositionInfo);
			}
			else
			{
				await Create(input.PositionInfo);
			}
		}


		/// <summary>
		/// 新增PositionInfo
		/// </summary>
		
		protected virtual async Task<PositionInfoEditDto> Create(PositionInfoEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <PositionInfo>(input);
            var entity=input.MapTo<PositionInfo>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<PositionInfoEditDto>();
		}

		/// <summary>
		/// 编辑PositionInfo
		/// </summary>
		
		protected virtual async Task Update(PositionInfoEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除PositionInfo信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除PositionInfo的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出PositionInfo为excel表,等待开发。
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


