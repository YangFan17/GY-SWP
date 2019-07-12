
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


using GYSWP.LC_ScanRecords;
using GYSWP.LC_ScanRecords.Dtos;
using GYSWP.LC_ScanRecords.DomainService;



namespace GYSWP.LC_ScanRecords
{
    /// <summary>
    /// LC_ScanRecord应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class LC_ScanRecordAppService : GYSWPAppServiceBase, ILC_ScanRecordAppService
    {
        private readonly IRepository<LC_ScanRecord, Guid> _entityRepository;

        private readonly ILC_ScanRecordManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public LC_ScanRecordAppService(
        IRepository<LC_ScanRecord, Guid> entityRepository
        ,ILC_ScanRecordManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 获取LC_ScanRecord的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<LC_ScanRecordListDto>> GetPaged(GetLC_ScanRecordsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<LC_ScanRecordListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<LC_ScanRecordListDto>>();

			return new PagedResultDto<LC_ScanRecordListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取LC_ScanRecordListDto信息
		/// </summary>
		 
		public async Task<LC_ScanRecordListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<LC_ScanRecordListDto>();
		}

		/// <summary>
		/// 获取编辑 LC_ScanRecord
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetLC_ScanRecordForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetLC_ScanRecordForEditOutput();
LC_ScanRecordEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<LC_ScanRecordEditDto>();

				//lC_ScanRecordEditDto = ObjectMapper.Map<List<lC_ScanRecordEditDto>>(entity);
			}
			else
			{
				editDto = new LC_ScanRecordEditDto();
			}

			output.LC_ScanRecord = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改LC_ScanRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateLC_ScanRecordInput input)
		{

			if (input.LC_ScanRecord.Id.HasValue)
			{
				await Update(input.LC_ScanRecord);
			}
			else
			{
				await Create(input.LC_ScanRecord);
			}
		}


		/// <summary>
		/// 新增LC_ScanRecord
		/// </summary>
		
		protected virtual async Task<LC_ScanRecordEditDto> Create(LC_ScanRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <LC_ScanRecord>(input);
            var entity=input.MapTo<LC_ScanRecord>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<LC_ScanRecordEditDto>();
		}

		/// <summary>
		/// 编辑LC_ScanRecord
		/// </summary>
		
		protected virtual async Task Update(LC_ScanRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除LC_ScanRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除LC_ScanRecord的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出LC_ScanRecord为excel表,等待开发。
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


