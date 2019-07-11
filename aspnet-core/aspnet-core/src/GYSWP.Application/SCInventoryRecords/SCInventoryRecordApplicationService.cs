
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


using GYSWP.SCInventoryRecords;
using GYSWP.SCInventoryRecords.Dtos;
using GYSWP.SCInventoryRecords.DomainService;
using Abp.Auditing;

namespace GYSWP.SCInventoryRecords
{
    /// <summary>
    /// SCInventoryRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class SCInventoryRecordAppService : GYSWPAppServiceBase, ISCInventoryRecordAppService
    {
        private readonly IRepository<SCInventoryRecord, long> _entityRepository;

        private readonly ISCInventoryRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public SCInventoryRecordAppService(
        IRepository<SCInventoryRecord, long> entityRepository
        ,ISCInventoryRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取SCInventoryRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<SCInventoryRecordListDto>> GetPagedAsync(GetSCInventoryRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<SCInventoryRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<SCInventoryRecordListDto>>();

			return new PagedResultDto<SCInventoryRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取SCInventoryRecordListDto信息
		/// </summary>
		 
		public async Task<SCInventoryRecordListDto> GetByIdAsync(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<SCInventoryRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 SCInventoryRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetSCInventoryRecordForEditOutput> GetForEditAsync(NullableIdDto<long> input)
		{
			var output = new GetSCInventoryRecordForEditOutput();
SCInventoryRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<SCInventoryRecordEditDto>();

				//sCInventoryRecordEditDto = ObjectMapper.Map<List<sCInventoryRecordEditDto>>(entity);
			}
			else
			{
				editDto = new SCInventoryRecordEditDto();
			}

			output.SCInventoryRecord = editDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改SCInventoryRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        [Audited]
        public async Task CreateOrUpdateAsync(CreateOrUpdateSCInventoryRecordInput input)
		{

			if (input.SCInventoryRecord.Id.HasValue)
			{
				await Update(input.SCInventoryRecord);
			}
			else
			{
				await Create(input.SCInventoryRecord);
			}
		}


		/// <summary>
		/// 新增SCInventoryRecord
		/// </summary>
		
		protected virtual async Task<SCInventoryRecordEditDto> Create(SCInventoryRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <SCInventoryRecord>(input);
            var entity=input.MapTo<SCInventoryRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<SCInventoryRecordEditDto>();
		}

		/// <summary>
		/// 编辑SCInventoryRecord
		/// </summary>
		
		protected virtual async Task Update(SCInventoryRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除SCInventoryRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task DeleteAsync(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除SCInventoryRecord的方法
		/// </summary>
		
		public async Task BatchDeleteAsync(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出SCInventoryRecord为excel表,等待开发。
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


