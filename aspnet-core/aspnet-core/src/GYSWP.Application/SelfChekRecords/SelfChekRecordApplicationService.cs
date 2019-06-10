
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


using GYSWP.SelfChekRecords;
using GYSWP.SelfChekRecords.Dtos;
using GYSWP.SelfChekRecords.DomainService;



namespace GYSWP.SelfChekRecords
{
    /// <summary>
    /// SelfChekRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class SelfChekRecordAppService : GYSWPAppServiceBase, ISelfChekRecordAppService
    {
        private readonly IRepository<SelfChekRecord, Guid> _entityRepository;

        private readonly ISelfChekRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public SelfChekRecordAppService(
        IRepository<SelfChekRecord, Guid> entityRepository
        ,ISelfChekRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取SelfChekRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<SelfChekRecordListDto>> GetPaged(GetSelfChekRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<SelfChekRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<SelfChekRecordListDto>>();

			return new PagedResultDto<SelfChekRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取SelfChekRecordListDto信息
		/// </summary>
		 
		public async Task<SelfChekRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<SelfChekRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 SelfChekRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetSelfChekRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetSelfChekRecordForEditOutput();
SelfChekRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<SelfChekRecordEditDto>();

				//selfChekRecordEditDto = ObjectMapper.Map<List<selfChekRecordEditDto>>(entity);
			}
			else
			{
				editDto = new SelfChekRecordEditDto();
			}

			output.SelfChekRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改SelfChekRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateSelfChekRecordInput input)
		{

			if (input.SelfChekRecord.Id.HasValue)
			{
				await Update(input.SelfChekRecord);
			}
			else
			{
				await Create(input.SelfChekRecord);
			}
		}


		/// <summary>
		/// 新增SelfChekRecord
		/// </summary>
		
		protected virtual async Task<SelfChekRecordEditDto> Create(SelfChekRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <SelfChekRecord>(input);
            var entity=input.MapTo<SelfChekRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<SelfChekRecordEditDto>();
		}

		/// <summary>
		/// 编辑SelfChekRecord
		/// </summary>
		
		protected virtual async Task Update(SelfChekRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除SelfChekRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除SelfChekRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出SelfChekRecord为excel表,等待开发。
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


