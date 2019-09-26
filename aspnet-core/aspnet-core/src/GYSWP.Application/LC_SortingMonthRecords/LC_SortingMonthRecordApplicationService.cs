
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


using GYSWP.LC_SortingMonthRecords;
using GYSWP.LC_SortingMonthRecords.Dtos;
using GYSWP.LC_SortingMonthRecords.DomainService;



namespace GYSWP.LC_SortingMonthRecords
{
    /// <summary>
    /// LC_SortingMonthRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_SortingMonthRecordAppService : GYSWPAppServiceBase, ILC_SortingMonthRecordAppService
    {
        private readonly IRepository<LC_SortingMonthRecord, Guid> _entityRepository;

        private readonly ILC_SortingMonthRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_SortingMonthRecordAppService(
        IRepository<LC_SortingMonthRecord, Guid> entityRepository
        ,ILC_SortingMonthRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_SortingMonthRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_SortingMonthRecordListDto>> GetPaged(GetLC_SortingMonthRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_SortingMonthRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_SortingMonthRecordListDto>>();

			return new PagedResultDto<LC_SortingMonthRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_SortingMonthRecordListDto信息
		/// </summary>
		 
		public async Task<LC_SortingMonthRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_SortingMonthRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_SortingMonthRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_SortingMonthRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_SortingMonthRecordForEditOutput();
LC_SortingMonthRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_SortingMonthRecordEditDto>();

				//lC_SortingMonthRecordEditDto = ObjectMapper.Map<List<lC_SortingMonthRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_SortingMonthRecordEditDto();
			}

			output.LC_SortingMonthRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_SortingMonthRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAllowAnonymous]
		public async Task CreateOrUpdate(CreateOrUpdateLC_SortingMonthRecordInput input)
		{

			if (input.LC_SortingMonthRecord.Id.HasValue)
			{
				await Update(input.LC_SortingMonthRecord);
			}
			else
			{
				await Create(input.LC_SortingMonthRecord);
			}
		}


		/// <summary>
		/// 新增LC_SortingMonthRecord
		/// </summary>
		
		protected virtual async Task<LC_SortingMonthRecordEditDto> Create(LC_SortingMonthRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_SortingMonthRecord>(input);
            var entity=input.MapTo<LC_SortingMonthRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_SortingMonthRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_SortingMonthRecord
		/// </summary>
		
		protected virtual async Task Update(LC_SortingMonthRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_SortingMonthRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_SortingMonthRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_SortingMonthRecord为excel表,等待开发。
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


