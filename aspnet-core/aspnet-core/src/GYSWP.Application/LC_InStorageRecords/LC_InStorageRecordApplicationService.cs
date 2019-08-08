
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


using GYSWP.LC_InStorageRecords;
using GYSWP.LC_InStorageRecords.Dtos;
using GYSWP.LC_InStorageRecords.DomainService;



namespace GYSWP.LC_InStorageRecords
{
    /// <summary>
    /// LC_InStorageRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_InStorageRecordAppService : GYSWPAppServiceBase, ILC_InStorageRecordAppService
    {
        private readonly IRepository<LC_InStorageRecord, Guid> _entityRepository;

        private readonly ILC_InStorageRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_InStorageRecordAppService(
        IRepository<LC_InStorageRecord, Guid> entityRepository
        ,ILC_InStorageRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_InStorageRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_InStorageRecordListDto>> GetPaged(GetLC_InStorageRecordsInput input)
		{
            var query = _entityRepository.GetAll().WhereIf(input.BeginTime.HasValue, c => c.CreationTime >= input.BeginTime && c.CreationTime < input.EndTime.Value.ToDayEnd());
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_InStorageRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_InStorageRecordListDto>>();

			return new PagedResultDto<LC_InStorageRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_InStorageRecordListDto信息
		/// </summary>
		 
		public async Task<LC_InStorageRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_InStorageRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_InStorageRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_InStorageRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_InStorageRecordForEditOutput();
LC_InStorageRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_InStorageRecordEditDto>();

				//lC_InStorageRecordEditDto = ObjectMapper.Map<List<lC_InStorageRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_InStorageRecordEditDto();
			}

			output.LC_InStorageRecord = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改LC_InStorageRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task CreateOrUpdate(CreateOrUpdateLC_InStorageRecordInput input)
		{

			if (input.LC_InStorageRecord.Id.HasValue)
			{
				await Update(input.LC_InStorageRecord);
			}
			else
			{
				await Create(input.LC_InStorageRecord);
			}
		}


		/// <summary>
		/// 新增LC_InStorageRecord
		/// </summary>
		
		protected virtual async Task<LC_InStorageRecordEditDto> Create(LC_InStorageRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_InStorageRecord>(input);
            var entity=input.MapTo<LC_InStorageRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_InStorageRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_InStorageRecord
		/// </summary>
		
		protected virtual async Task Update(LC_InStorageRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_InStorageRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_InStorageRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_InStorageRecord为excel表,等待开发。
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


