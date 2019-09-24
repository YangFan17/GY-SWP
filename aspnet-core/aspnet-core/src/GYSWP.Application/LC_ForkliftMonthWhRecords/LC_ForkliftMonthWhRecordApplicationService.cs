
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


using GYSWP.LC_ForkliftMonthWhRecords;
using GYSWP.LC_ForkliftMonthWhRecords.Dtos;
using GYSWP.LC_ForkliftMonthWhRecords.DomainService;



namespace GYSWP.LC_ForkliftMonthWhRecords
{
    /// <summary>
    /// LC_ForkliftMonthWhRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_ForkliftMonthWhRecordAppService : GYSWPAppServiceBase, ILC_ForkliftMonthWhRecordAppService
    {
        private readonly IRepository<LC_ForkliftMonthWhRecord, Guid> _entityRepository;

        private readonly ILC_ForkliftMonthWhRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_ForkliftMonthWhRecordAppService(
        IRepository<LC_ForkliftMonthWhRecord, Guid> entityRepository
        ,ILC_ForkliftMonthWhRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_ForkliftMonthWhRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_ForkliftMonthWhRecordListDto>> GetPaged(GetLC_ForkliftMonthWhRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_ForkliftMonthWhRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_ForkliftMonthWhRecordListDto>>();

			return new PagedResultDto<LC_ForkliftMonthWhRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_ForkliftMonthWhRecordListDto信息
		/// </summary>
		 
		public async Task<LC_ForkliftMonthWhRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_ForkliftMonthWhRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_ForkliftMonthWhRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_ForkliftMonthWhRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_ForkliftMonthWhRecordForEditOutput();
LC_ForkliftMonthWhRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_ForkliftMonthWhRecordEditDto>();

				//lC_ForkliftMonthWhRecordEditDto = ObjectMapper.Map<List<lC_ForkliftMonthWhRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_ForkliftMonthWhRecordEditDto();
			}

			output.LC_ForkliftMonthWhRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_ForkliftMonthWhRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_ForkliftMonthWhRecordInput input)
		{

			if (input.LC_ForkliftMonthWhRecord.Id.HasValue)
			{
				await Update(input.LC_ForkliftMonthWhRecord);
			}
			else
			{
				await Create(input.LC_ForkliftMonthWhRecord);
			}
		}


		/// <summary>
		/// 新增LC_ForkliftMonthWhRecord
		/// </summary>
		
		protected virtual async Task<LC_ForkliftMonthWhRecordEditDto> Create(LC_ForkliftMonthWhRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_ForkliftMonthWhRecord>(input);
            var entity=input.MapTo<LC_ForkliftMonthWhRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_ForkliftMonthWhRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_ForkliftMonthWhRecord
		/// </summary>
		
		protected virtual async Task Update(LC_ForkliftMonthWhRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_ForkliftMonthWhRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_ForkliftMonthWhRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_ForkliftMonthWhRecord为excel表,等待开发。
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


