
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


using GYSWP.LC_SortingWeekRecords;
using GYSWP.LC_SortingWeekRecords.Dtos;
using GYSWP.LC_SortingWeekRecords.DomainService;



namespace GYSWP.LC_SortingWeekRecords
{
    /// <summary>
    /// LC_SortingWeekRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_SortingWeekRecordAppService : GYSWPAppServiceBase, ILC_SortingWeekRecordAppService
    {
        private readonly IRepository<LC_SortingWeekRecord, Guid> _entityRepository;

        private readonly ILC_SortingWeekRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_SortingWeekRecordAppService(
        IRepository<LC_SortingWeekRecord, Guid> entityRepository
        ,ILC_SortingWeekRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_SortingWeekRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_SortingWeekRecordListDto>> GetPaged(GetLC_SortingWeekRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_SortingWeekRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_SortingWeekRecordListDto>>();

			return new PagedResultDto<LC_SortingWeekRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_SortingWeekRecordListDto信息
		/// </summary>
		 
		public async Task<LC_SortingWeekRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_SortingWeekRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_SortingWeekRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_SortingWeekRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_SortingWeekRecordForEditOutput();
LC_SortingWeekRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_SortingWeekRecordEditDto>();

				//lC_SortingWeekRecordEditDto = ObjectMapper.Map<List<lC_SortingWeekRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_SortingWeekRecordEditDto();
			}

			output.LC_SortingWeekRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_SortingWeekRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_SortingWeekRecordInput input)
		{

			if (input.LC_SortingWeekRecord.Id.HasValue)
			{
				await Update(input.LC_SortingWeekRecord);
			}
			else
			{
				await Create(input.LC_SortingWeekRecord);
			}
		}


		/// <summary>
		/// 新增LC_SortingWeekRecord
		/// </summary>
		
		protected virtual async Task<LC_SortingWeekRecordEditDto> Create(LC_SortingWeekRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_SortingWeekRecord>(input);
            var entity=input.MapTo<LC_SortingWeekRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_SortingWeekRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_SortingWeekRecord
		/// </summary>
		
		protected virtual async Task Update(LC_SortingWeekRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_SortingWeekRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_SortingWeekRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_SortingWeekRecord为excel表,等待开发。
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


