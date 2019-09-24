
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


using GYSWP.LC_KyjFunctionRecords;
using GYSWP.LC_KyjFunctionRecords.Dtos;
using GYSWP.LC_KyjFunctionRecords.DomainService;



namespace GYSWP.LC_KyjFunctionRecords
{
    /// <summary>
    /// LC_KyjFunctionRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_KyjFunctionRecordAppService : GYSWPAppServiceBase, ILC_KyjFunctionRecordAppService
    {
        private readonly IRepository<LC_KyjFunctionRecord, Guid> _entityRepository;

        private readonly ILC_KyjFunctionRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_KyjFunctionRecordAppService(
        IRepository<LC_KyjFunctionRecord, Guid> entityRepository
        ,ILC_KyjFunctionRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_KyjFunctionRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_KyjFunctionRecordListDto>> GetPaged(GetLC_KyjFunctionRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_KyjFunctionRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_KyjFunctionRecordListDto>>();

			return new PagedResultDto<LC_KyjFunctionRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_KyjFunctionRecordListDto信息
		/// </summary>
		 
		public async Task<LC_KyjFunctionRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_KyjFunctionRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_KyjFunctionRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_KyjFunctionRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_KyjFunctionRecordForEditOutput();
LC_KyjFunctionRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_KyjFunctionRecordEditDto>();

				//lC_KyjFunctionRecordEditDto = ObjectMapper.Map<List<lC_KyjFunctionRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_KyjFunctionRecordEditDto();
			}

			output.LC_KyjFunctionRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_KyjFunctionRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_KyjFunctionRecordInput input)
		{

			if (input.LC_KyjFunctionRecord.Id.HasValue)
			{
				await Update(input.LC_KyjFunctionRecord);
			}
			else
			{
				await Create(input.LC_KyjFunctionRecord);
			}
		}


		/// <summary>
		/// 新增LC_KyjFunctionRecord
		/// </summary>
		
		protected virtual async Task<LC_KyjFunctionRecordEditDto> Create(LC_KyjFunctionRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_KyjFunctionRecord>(input);
            var entity=input.MapTo<LC_KyjFunctionRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_KyjFunctionRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_KyjFunctionRecord
		/// </summary>
		
		protected virtual async Task Update(LC_KyjFunctionRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_KyjFunctionRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_KyjFunctionRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_KyjFunctionRecord为excel表,等待开发。
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


