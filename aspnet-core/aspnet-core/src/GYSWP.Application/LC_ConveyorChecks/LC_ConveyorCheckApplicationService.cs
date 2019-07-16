
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


using GYSWP.LC_ConveyorChecks;
using GYSWP.LC_ConveyorChecks.Dtos;
using GYSWP.LC_ConveyorChecks.DomainService;
using GYSWP.Dtos;

namespace GYSWP.LC_ConveyorChecks
{
    /// <summary>
    /// LC_ConveyorCheck应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_ConveyorCheckAppService : GYSWPAppServiceBase, ILC_ConveyorCheckAppService
    {
        private readonly IRepository<LC_ConveyorCheck, Guid> _entityRepository;

        private readonly ILC_ConveyorCheckManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_ConveyorCheckAppService(
        IRepository<LC_ConveyorCheck, Guid> entityRepository
        ,ILC_ConveyorCheckManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_ConveyorCheck的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_ConveyorCheckListDto>> GetPaged(GetLC_ConveyorChecksInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_ConveyorCheckListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_ConveyorCheckListDto>>();

			return new PagedResultDto<LC_ConveyorCheckListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_ConveyorCheckListDto信息
		/// </summary>
		 
		public async Task<LC_ConveyorCheckListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_ConveyorCheckListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_ConveyorCheck
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_ConveyorCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_ConveyorCheckForEditOutput();
LC_ConveyorCheckEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_ConveyorCheckEditDto>();

				//lC_ConveyorCheckEditDto = ObjectMapper.Map<List<lC_ConveyorCheckEditDto>>(entity);
			}
			else
			{
				editDto = new LC_ConveyorCheckEditDto();
			}

			output.LC_ConveyorCheck = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_ConveyorCheck的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_ConveyorCheckInput input)
		{

			if (input.LC_ConveyorCheck.Id.HasValue)
			{
				await Update(input.LC_ConveyorCheck);
			}
			else
			{
				await Create(input.LC_ConveyorCheck);
			}
		}


		/// <summary>
		/// 新增LC_ConveyorCheck
		/// </summary>
		
		protected virtual async Task<LC_ConveyorCheckEditDto> Create(LC_ConveyorCheckEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_ConveyorCheck>(input);
            var entity=input.MapTo<LC_ConveyorCheck>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_ConveyorCheckEditDto>();
		}

		/// <summary>
		/// 编辑LC_ConveyorCheck
		/// </summary>
		
		protected virtual async Task Update(LC_ConveyorCheckEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_ConveyorCheck信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_ConveyorCheck的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出LC_ConveyorCheck为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}


        [AbpAllowAnonymous]
        public async Task<APIResultDto> CreateConveyorCheckRecordAsync(LC_ConveyorCheckEditDto input)
        {
            var entity = input.MapTo<LC_ConveyorCheck>();
            
            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }
    }
}


