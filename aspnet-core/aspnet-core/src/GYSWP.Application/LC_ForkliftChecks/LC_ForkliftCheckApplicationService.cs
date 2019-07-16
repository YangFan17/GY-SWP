
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


using GYSWP.LC_ForkliftChecks;
using GYSWP.LC_ForkliftChecks.Dtos;
using GYSWP.LC_ForkliftChecks.DomainService;
using GYSWP.Dtos;

namespace GYSWP.LC_ForkliftChecks
{
    /// <summary>
    /// LC_ForkliftCheck应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_ForkliftCheckAppService : GYSWPAppServiceBase, ILC_ForkliftCheckAppService
    {
        private readonly IRepository<LC_ForkliftCheck, Guid> _entityRepository;

        private readonly ILC_ForkliftCheckManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_ForkliftCheckAppService(
        IRepository<LC_ForkliftCheck, Guid> entityRepository
        ,ILC_ForkliftCheckManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_ForkliftCheck的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_ForkliftCheckListDto>> GetPaged(GetLC_ForkliftChecksInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_ForkliftCheckListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_ForkliftCheckListDto>>();

			return new PagedResultDto<LC_ForkliftCheckListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_ForkliftCheckListDto信息
		/// </summary>
		 
		public async Task<LC_ForkliftCheckListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_ForkliftCheckListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_ForkliftCheck
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_ForkliftCheckForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_ForkliftCheckForEditOutput();
LC_ForkliftCheckEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_ForkliftCheckEditDto>();

				//lC_ForkliftCheckEditDto = ObjectMapper.Map<List<lC_ForkliftCheckEditDto>>(entity);
			}
			else
			{
				editDto = new LC_ForkliftCheckEditDto();
			}

			output.LC_ForkliftCheck = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_ForkliftCheck的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_ForkliftCheckInput input)
		{

			if (input.LC_ForkliftCheck.Id.HasValue)
			{
				await Update(input.LC_ForkliftCheck);
			}
			else
			{
				await Create(input.LC_ForkliftCheck);
			}
		}


		/// <summary>
		/// 新增LC_ForkliftCheck
		/// </summary>
		
		protected virtual async Task<LC_ForkliftCheckEditDto> Create(LC_ForkliftCheckEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_ForkliftCheck>(input);
            var entity=input.MapTo<LC_ForkliftCheck>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_ForkliftCheckEditDto>();
		}

		/// <summary>
		/// 编辑LC_ForkliftCheck
		/// </summary>
		
		protected virtual async Task Update(LC_ForkliftCheckEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_ForkliftCheck信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_ForkliftCheck的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


        /// <summary>
        /// 导出LC_ForkliftCheck为excel表,等待开发。
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
        public async Task<APIResultDto> CreateForkliftCheckRecordAsync(LC_ForkliftCheckEditDto input)
        {
            var entity = input.MapTo<LC_ForkliftCheck>();

            entity = await _entityRepository.InsertAsync(entity);
            return new APIResultDto()
            {
                Code = 0,
                Data = entity
            };
        }
    }
}


