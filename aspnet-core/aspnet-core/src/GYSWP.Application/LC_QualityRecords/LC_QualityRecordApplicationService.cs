
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


using GYSWP.LC_QualityRecords;
using GYSWP.LC_QualityRecords.Dtos;
using GYSWP.LC_QualityRecords.DomainService;



namespace GYSWP.LC_QualityRecords
{
    /// <summary>
    /// LC_QualityRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_QualityRecordAppService : GYSWPAppServiceBase, ILC_QualityRecordAppService
    {
        private readonly IRepository<LC_QualityRecord, Guid> _entityRepository;

        private readonly ILC_QualityRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_QualityRecordAppService(
        IRepository<LC_QualityRecord, Guid> entityRepository
        ,ILC_QualityRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_QualityRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_QualityRecordListDto>> GetPaged(GetLC_QualityRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_QualityRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_QualityRecordListDto>>();

			return new PagedResultDto<LC_QualityRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_QualityRecordListDto信息
		/// </summary>
		 
		public async Task<LC_QualityRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_QualityRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_QualityRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_QualityRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_QualityRecordForEditOutput();
LC_QualityRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_QualityRecordEditDto>();

				//lC_QualityRecordEditDto = ObjectMapper.Map<List<lC_QualityRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_QualityRecordEditDto();
			}

			output.LC_QualityRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_QualityRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_QualityRecordInput input)
		{

			if (input.LC_QualityRecord.Id.HasValue)
			{
				await Update(input.LC_QualityRecord);
			}
			else
			{
				await Create(input.LC_QualityRecord);
			}
		}


		/// <summary>
		/// 新增LC_QualityRecord
		/// </summary>
		
		protected virtual async Task<LC_QualityRecordEditDto> Create(LC_QualityRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_QualityRecord>(input);
            var entity=input.MapTo<LC_QualityRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_QualityRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_QualityRecord
		/// </summary>
		
		protected virtual async Task Update(LC_QualityRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_QualityRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_QualityRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_QualityRecord为excel表,等待开发。
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


