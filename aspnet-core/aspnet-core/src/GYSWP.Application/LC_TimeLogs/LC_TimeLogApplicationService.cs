
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


using GYSWP.LC_TimeLogs;
using GYSWP.LC_TimeLogs.Dtos;
using GYSWP.LC_TimeLogs.DomainService;



namespace GYSWP.LC_TimeLogs
{
    /// <summary>
    /// LC_TimeLog应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_TimeLogAppService : GYSWPAppServiceBase, ILC_TimeLogAppService
    {
        private readonly IRepository<LC_TimeLog, Guid> _entityRepository;

        private readonly ILC_TimeLogManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_TimeLogAppService(
        IRepository<LC_TimeLog, Guid> entityRepository
        ,ILC_TimeLogManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_TimeLog的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_TimeLogListDto>> GetPaged(GetLC_TimeLogsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_TimeLogListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_TimeLogListDto>>();

			return new PagedResultDto<LC_TimeLogListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_TimeLogListDto信息
		/// </summary>
		 
		public async Task<LC_TimeLogListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_TimeLogListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_TimeLog
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_TimeLogForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_TimeLogForEditOutput();
LC_TimeLogEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_TimeLogEditDto>();

				//lC_TimeLogEditDto = ObjectMapper.Map<List<lC_TimeLogEditDto>>(entity);
			}
			else
			{
				editDto = new LC_TimeLogEditDto();
			}

			output.LC_TimeLog = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_TimeLog的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_TimeLogInput input)
		{

			if (input.LC_TimeLog.Id.HasValue)
			{
				await Update(input.LC_TimeLog);
			}
			else
			{
				await Create(input.LC_TimeLog);
			}
		}


		/// <summary>
		/// 新增LC_TimeLog
		/// </summary>
		
		protected virtual async Task<LC_TimeLogEditDto> Create(LC_TimeLogEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_TimeLog>(input);
            var entity=input.MapTo<LC_TimeLog>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_TimeLogEditDto>();
		}

		/// <summary>
		/// 编辑LC_TimeLog
		/// </summary>
		
		protected virtual async Task Update(LC_TimeLogEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_TimeLog信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_TimeLog的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_TimeLog为excel表,等待开发。
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

